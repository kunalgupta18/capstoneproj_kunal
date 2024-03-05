using CapstoneProj.Models;

namespace CapstoneProj.Repository.Interface
{
    public interface IPurchaseRepo
    {
        IEnumerable<Purchase> GetAllPurchases();
        Purchase GetPurchaseById(int purchaseId);
        Purchase AddPurchase(Purchase purchase);
        Purchase UpdatePurchase(Purchase purchase);
        Purchase DeletePurchase(int purchaseId);
    }
}
