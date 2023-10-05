using DemoASPMVC_DAL.Models;

namespace DemoASPMVC_DAL.Interface
{
    public interface IUserService : IBaseRepository<User>
    {
        User Login(string email, string pwd);
        bool Register(string email, string pwd, string nickname);
    }
}