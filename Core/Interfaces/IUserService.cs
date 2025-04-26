using Core.DTOs;

namespace Core.Interfaces
{
    public interface IUserService
    {
        public Task<UserSession> Login(string username, string password);
    }
}
