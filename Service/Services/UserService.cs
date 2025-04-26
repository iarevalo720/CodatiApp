using Core.DTOs;
using Core.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool EstaAutenticado(string jwt)
        {
            if (string.IsNullOrEmpty(jwt) || EsTokenExpirado(jwt)) return false;

            return true;
        }

        public async Task<string> Login(string username, string password)
        {
            var user = await _userRepository.ValidarEmail(username);
            if (user is null) throw new Exception("usuario_no_encontrado");

            bool esPasswordValido = await _userRepository.ValidarPassword(user, password);
            if (!esPasswordValido) throw new Exception("credenciales_invalidos");

            var rolesUsuario = await _userRepository.ObtenerRoles(user);
            var userSession = new UserSession(user.Id, user.Name, user.Email, rolesUsuario.First());

            try
            {
                string token = GenerateToken(userSession);
                return token;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private bool EsTokenExpirado(string jwt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var jwtSecurityToken = tokenHandler.ReadJwtToken(jwt);
                return jwtSecurityToken.ValidTo.ToLocalTime() < DateTime.Now;
            }
            catch (Exception)
            {
                return true;
            }
        }

        private string GenerateToken(UserSession user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("F83JjB01FlF94HO3Si7Fun7x8u4973b2a"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var token = new JwtSecurityToken(
                issuer: "https://localhost:7080",
                audience: "https://localhost:7080",
                claims: userClaims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
