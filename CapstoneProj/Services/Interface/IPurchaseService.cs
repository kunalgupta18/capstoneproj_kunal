using CapstoneProj.Models;
namespace CapstoneProj.Services.Interface
{
    public interface IPurchaseService
    {
        IEnumerable<Purchase> GetAllPurchases();
        Purchase GetPurchaseById(int purchaseId);
        Purchase AddPurchase(Purchase purchase);
        Purchase UpdatePurchase(Purchase purchase);
        Purchase DeletePurchase(int purchaseId);
    }
}
