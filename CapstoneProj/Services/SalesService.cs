using CapstoneProj.Models;
using CapstoneProj.Repository.Interface;
using CapstoneProj.Services.Interface;

namespace CapstoneProj.Services
{
    public class SalesService : ISalesService
    {
        private readonly ISalesRepo _salesrepo;

        public SalesService(ISalesRepo repo)
        {
            _salesrepo = repo;
        }

        public Sales GetSalesById(int salesId)
        {
            return _salesrepo.GetSalesById(salesId);
        }

        public IEnumerable<Sales> GetAllSales()
        {
            return _salesrepo.GetAllSales();
        }

        public Sales AddSales(Sales sales)
        {
            return _salesrepo.AddSales(sales);
        }

        public Sales UpdateSales(Sales sales)
        {
            return _salesrepo.UpdateSales(sales);
        }

        public Sales DeleteSales(int salesId)
        {
            return _salesrepo.DeleteSales(salesId);
        }
    }
}
