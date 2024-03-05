using CapstoneProj.Models;

namespace CapstoneProj.Repository.Interface
{
    public interface ISalesRepo
    {
        Sales GetSalesById(int salesId);
        IEnumerable<Sales> GetAllSales();
        Sales AddSales(Sales sales);
        Sales UpdateSales(Sales sales);
        Sales DeleteSales(int salesId);
    }
}
