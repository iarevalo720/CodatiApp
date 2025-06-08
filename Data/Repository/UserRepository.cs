using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _context;

        public UserRepository(UserManager<ApplicationUser> userManager, AppDbContext appDbContext)
        {
            _userManager = userManager;
            _context = appDbContext;
        }

        public async Task<ApplicationUser?> ValidarEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<bool> ValidarPassword(ApplicationUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IList<string>> ObtenerRoles(ApplicationUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<ApplicationUser?> ObtenerUsuarioPorCI(string ci)
        {
            return await _context.Users.Where(u => u.NroDocumento == ci).FirstOrDefaultAsync();
        }

        public async Task GuardarCambiosUsuario(ApplicationUser user)
        {
            await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> CrearCliente(ApplicationUser applicationUser, string password)
        {
            return await _userManager.CreateAsync(applicationUser, password);
        }

        public async Task AsignarRol(ApplicationUser user, string roleName)
        {
            await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task<IdentityResult> ActualizarContrasena(ApplicationUser user, string contrasenaActual, string nuevaContrasena)
        {
            return await _userManager.ChangePasswordAsync(user, contrasenaActual, nuevaContrasena);
        }

        public async Task<IdentityResult> RestablecerContrasena(ApplicationUser user, string nuevaContrasena)
        {
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return await _userManager.ResetPasswordAsync(user, token, nuevaContrasena);
        }
    }
}
