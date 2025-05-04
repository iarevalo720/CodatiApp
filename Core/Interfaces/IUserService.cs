using Core.DTOs;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IUserService
    {
        public Task<UserSession> Login(string username, string password);
        public Task<ApplicationUser?> ObtenerUsuarioPorCi(string ci);
    }
}
