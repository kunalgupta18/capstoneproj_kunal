using CapstoneProj.Models;

namespace CapstoneProj.Repository.Interface
{
    public interface ISupplierRepo
    {
        IEnumerable<Supplier> GetAllSuppliers();
        Supplier GetSupplierById(int id);
        Supplier AddSupplier(Supplier supplier);
        Supplier UpdateSupplier(Supplier supplier);
        Supplier DeleteSupplier(int id);
    }
}
