using CapstoneProj.Repository.Interface;
using CapstoneProj.Services.Interface;
using CapstoneProj.Services;
using CapstoneProj.Models;
namespace CapstoneProj.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepo _purchaseRepo;

        public PurchaseService(IPurchaseRepo purchaseRepo)
        {
            _purchaseRepo = purchaseRepo;
        }

        public IEnumerable<Purchase> GetAllPurchases()
        {
            return _purchaseRepo.GetAllPurchases();
        }

        public Purchase GetPurchaseById(int purchaseId)
        {
            return _purchaseRepo.GetPurchaseById(purchaseId);
        }

        public Purchase AddPurchase(Purchase purchase)
        {
            return _purchaseRepo.AddPurchase(purchase);
        }

        public Purchase UpdatePurchase(Purchase purchase)
        {
            return _purchaseRepo.UpdatePurchase(purchase);
        }

        public Purchase DeletePurchase(int purchaseId)
        {
            return _purchaseRepo.DeletePurchase(purchaseId);
        }
    }
}
