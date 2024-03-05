using CapstoneProj.Models;

namespace CapstoneProj.Services.Interface
{
    public interface IClientService
    {
        IEnumerable<Client> GetDetails();
        Client GetClientById(int id);
        Client DeleteClient(int id);
    }
}
