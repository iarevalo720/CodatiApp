using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using MimeKit;

namespace Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        public UserService(IUserRepository userRepository, IEmailService emailService)
        {
            _userRepository = userRepository;
            _emailService = emailService;
        }

        public async Task CrearCliente(string ci, string nombre, string correo, string telefono, string direccion)
        {
            string nuevoPassword = GenerarPassword(nombre);
            ApplicationUser nuevoUsuario = ArmarUsuario(ci, nombre, correo, telefono, direccion, nuevoPassword);

            var resultado = await _userRepository.CrearUsuario(nuevoUsuario, nuevoPassword);
            if (!resultado.Succeeded)
            {
                throw new Exception("Error: " + resultado.Errors.FirstOrDefault());
            }
            await _userRepository.AsignarRol(nuevoUsuario, "Cliente");

            var mensaje = new BodyBuilder
            {
                HtmlBody = $@"
                    <h1>¡Hola {nuevoUsuario.Name}!</h1>
                    <p>Tu cuenta ha sido creada con éxito.</p>
                    <p><strong>Contraseña temporal:</strong> {nuevoPassword}</p>
                    <p>De momento su cuenta se encuentra inactiva, para activarlo, por favor, cambie su contraseña desde la app CODATI, en el botón 'Activar cuenta por primera vez'.</p>
                "
            };

            var asunto = "Bienvenido a nuestro taller";

            try
            {
                await _emailService.EnviarEmail(correo, nombre, mensaje, asunto);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al enviar el correo de bienvenida: " + ex.Message);
                throw new Exception($"Error: {ex}");
            }
        }

        public async Task CrearFuncionario(string ci, string nombre, string correo, string telefono, string direccion, string rol)
        {
            string nuevoPassword = GenerarPassword(nombre);
            ApplicationUser nuevoUsuario = ArmarUsuario(ci, nombre, correo, telefono, direccion, nuevoPassword);

            var resultado = await _userRepository.CrearUsuario(nuevoUsuario, nuevoPassword);
            if (!resultado.Succeeded)
            {
                throw new Exception("Error: " + resultado.Errors.FirstOrDefault());
            }
            await _userRepository.AsignarRol(nuevoUsuario, rol);

            var mensaje = new BodyBuilder
            {
                HtmlBody = $@"
                    <h1>¡Hola {nuevoUsuario.Name}!</h1>
                    <p>Tu cuenta ha sido creada con éxito.</p>
                    <p><strong>Contraseña temporal:</strong> {nuevoPassword}</p>
                    <p>De momento su cuenta se encuentra inactiva, para activarlo, por favor, cambie su contraseña desde la app CODATI, en el botón 'Activar cuenta por primera vez'.</p>
                "
            };

            var asunto = "Bienvenido a nuestro taller";

            try
            {
                await _emailService.EnviarEmail(correo, nombre, mensaje, asunto);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al enviar el correo de bienvenida: " + ex.Message);
                throw new Exception($"Error: {ex}");
            }
        }

        public async Task GuardarCambiosUsuario(ApplicationUser user)
        {
            await _userRepository.GuardarCambiosUsuario(user);
        }

        public async Task<UserSession> Login(string username, string password)
        {
            var user = await _userRepository.ValidarEmail(username);
            if (user is null) throw new Exception("usuario_no_encontrado");

            bool esPasswordValido = await _userRepository.ValidarPassword(user, password);
            if (!esPasswordValido) throw new Exception("credenciales_invalidos");

            if (user.EsActivadoPrimeraVez?.ToLower() != "si") throw new Exception("usuario_no_activo");
            if (user.EsHabilitado?.ToLower() != "si") throw new Exception("usuario_inhabilitado");

            var rolesUsuario = await _userRepository.ObtenerRoles(user);
            var userSession = new UserSession(user.Id, user.Name, user.Email, rolesUsuario.First());

            try
            {
                return userSession;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public async Task<bool> ExisteEmail(string email)
        {
            return (await _userRepository.ValidarEmail(email) != null);
        }

        public async Task<ApplicationUser?> ObtenerUsuarioPorCi(string ci)
        {
            return await _userRepository.ObtenerUsuarioPorCI(ci);
        }

        public async Task<bool> ExisteCi(string ci)
        {
            return (await _userRepository.ObtenerUsuarioPorCI(ci) != null);
        }

        private ApplicationUser ArmarUsuario(string ci, string nombre, string correo, string telefono, string direccion, string contrasena)
        {
            return new ApplicationUser
            {
                Name = nombre,
                NroDocumento = ci,
                PhoneNumber = telefono,
                Direccion = direccion,
                Email = correo,
                PasswordHash = contrasena,
                UserName = correo,
                CreatedAt = DateTime.Now,
                EsHabilitado = "no",
                EsActivadoPrimeraVez = "no"
            };
        }

        private string GenerarPassword(string nombre)
        {
            Random random = new Random();
            int numeros = random.Next(100100, 999900);

            return nombre.Substring(0, 1).ToLower() + numeros.ToString();
        }

        public async Task ActivarCuenta(string codigoActivacion, string correo, string contrasena)
        {
            ApplicationUser? usuario = await _userRepository.ValidarEmail(correo);
            if (usuario == null)
            {
                throw new Exception("usuario_inexistente");
            }

            if (usuario.EsActivadoPrimeraVez?.ToLower() == "si")
            {
                throw new Exception("usuario_ya_activado");
            }

            bool esCodigoValido = await _userRepository.ValidarPassword(usuario, codigoActivacion);
            if (!esCodigoValido)
            {
                throw new Exception("codigo_invalido");
            }

            usuario.EsActivadoPrimeraVez = "si";
            usuario.EsHabilitado = "si";

            IdentityResult resultado = await _userRepository.ActualizarContrasena(usuario, codigoActivacion, contrasena);

            if (resultado.Succeeded)
            {
                await _userRepository.GuardarCambiosUsuario(usuario);
            }
            else
            {
                foreach (var error in resultado.Errors)
                {
                    throw new Exception($"{error.Description}");
                    Console.WriteLine($"Error al cambiar la contrasena de la cuenta: {error.Description}");
                }
            }
        }

        public async Task RestablecerContrasena(string ci)
        {
            try
            {
                ApplicationUser? usuario = await _userRepository.ObtenerUsuarioPorCI(ci);
                if (usuario is null)
                {
                    throw new Exception("usuario_inexistente");
                }

                string nuevaContrasena = GenerarPassword(usuario.Name);

                IdentityResult resultado = await _userRepository.RestablecerContrasena(usuario, nuevaContrasena);
                if (!resultado.Succeeded)
                {
                    throw new Exception("Error al restablecer la contrasena: " + resultado.Errors.FirstOrDefault());
                }

                usuario.EsActivadoPrimeraVez = "no";
                await _userRepository.GuardarCambiosUsuario(usuario);

                string asunto = "Restablecimiento de contraseña";
                var mensaje = new BodyBuilder
                {
                    HtmlBody = $@"
                        <h1>¡Hola {usuario.Name}!</h1>
                        <p>Tu contraseña ha sido restablecida exitosamente.</p>
                        <p><strong>Nueva contraseña:</strong> {nuevaContrasena}</p>
                        <p>Por favor, active la cuenta con su nueva contraseña para restablecer por una propia de usted.</p>
                    "
                };

                await _emailService.EnviarEmail(usuario.Email, usuario.Name, mensaje, asunto);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al restablecer la contrasena: " + ex.Message);
            }
        }

        public async Task<bool> EsRolFuncionarioValido(ApplicationUser user)
        {
            IList<string> rolesFuncionario = await _userRepository.ObtenerRoles(user);

            if (rolesFuncionario.FirstOrDefault() == "Mecanico" || rolesFuncionario.FirstOrDefault() == "Secretario") { return true; }

            return false;
        }

        public async Task<IList<string>> ObtenerRolesUsuario(ApplicationUser user)
        {
            return await _userRepository.ObtenerRoles(user);
        }

        public async Task CambiarRolUsuario(ApplicationUser user, string anteriorRol, string nuevoRol)
        {
            await _userRepository.EliminarRolUsuario(user, anteriorRol);
            await _userRepository.AsignarRol(user, nuevoRol);
        }
    }
}
