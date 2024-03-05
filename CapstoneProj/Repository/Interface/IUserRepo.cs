using CapstoneProj.Models;

namespace CapstoneProj.Repository.Interface
{
    public interface IUserRepo
    {
        Users GetUser(string username, string password);
        Users AddUser(Users user);
    }
}
