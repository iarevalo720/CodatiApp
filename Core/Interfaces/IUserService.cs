namespace Core.Interfaces
{
    public interface IUserService
    {
        public bool EstaAutenticado(string jwt);
        public Task<string> Login(string username, string password);
    }
}
