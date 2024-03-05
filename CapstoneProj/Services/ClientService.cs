using CapstoneProj.Models;
using CapstoneProj.Repository.Interface;
using CapstoneProj.Services.Interface;

namespace CapstoneProj.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepo _clientRepo;

        public ClientService(IClientRepo clientRepo)
        {
            _clientRepo = clientRepo;
        }

        public IEnumerable<Client> GetDetails()
        {
            return _clientRepo.GetDetails();
        }
        public Client GetClientById(int id)
        {
            return _clientRepo.GetClientById(id);
        }
        public Client DeleteClient(int id)
        {
            return _clientRepo.DeleteClient(id);
        }
    }
}
