using CapstoneProj.Models;

namespace CapstoneProj.Services.Interface
{
    public interface ISalesService
    {
        Sales GetSalesById(int salesId);
        IEnumerable<Sales> GetAllSales();
        Sales AddSales(Sales sales);
        Sales UpdateSales(Sales sales);
        Sales DeleteSales(int salesId);
    }
}
