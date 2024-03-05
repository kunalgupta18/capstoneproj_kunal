using CapstoneProj.Models;
using CapstoneProj.Repository.Interface;
using CapstoneProj.Services.Interface;

namespace CapstoneProj.Services
{
    public class DisplayService : IDisplayService
    {
        private readonly IDisplayRepo _displayRepo;

        public DisplayService(IDisplayRepo displayRepo)
        {
            _displayRepo = displayRepo;
        }

        public SalesAndPurchaseSummary GetSummary()
        {
            return _displayRepo.GetSummary();
        }
    }
}
