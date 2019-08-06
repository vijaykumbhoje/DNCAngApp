using System.Threading.Tasks;
using DNCAngApp.API.Models;

namespace DNCAngApp.API.Data
{
    public interface IAuthRepository
    {
         Task<User> Register(User user, string password);
         
         Task<User> Login(string username, string passsword);

         Task<bool> UserExists(string username);
    }
}