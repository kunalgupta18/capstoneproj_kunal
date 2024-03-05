using CapstoneProj.Models;

namespace CapstoneProj.Services.Interface
{
    public interface ISupplierService
    {
        IEnumerable<Supplier> GetAllSuppliers();
        Supplier GetSupplierById(int id);
        Supplier AddSupplier(Supplier supplier);
        Supplier UpdateSupplier(Supplier supplier);
        Supplier DeleteSupplier(int id);
    }
}
