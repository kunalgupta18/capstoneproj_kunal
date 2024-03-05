using CapstoneProj.Models;

namespace CapstoneProj.Repository.Interface
{
    public interface IClientRepo
    {
        IEnumerable<Client> GetDetails();
        Client GetClientById(int id);
        Client DeleteClient(int id);
    }
}
