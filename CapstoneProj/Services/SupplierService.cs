using CapstoneProj.Models;
using CapstoneProj.Services.Interface;
using CapstoneProj.Repository.Interface;
namespace CapstoneProj.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepo _supplierRepository;

        public SupplierService(ISupplierRepo supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public IEnumerable<Supplier> GetAllSuppliers()
        {
            return _supplierRepository.GetAllSuppliers();
        }

        public Supplier GetSupplierById(int id)
        {
            return _supplierRepository.GetSupplierById(id);
        }

        public Supplier AddSupplier(Supplier supplier)
        {
            return _supplierRepository.AddSupplier(supplier);
        }

        public Supplier UpdateSupplier(Supplier supplier)
        {
            return _supplierRepository.UpdateSupplier(supplier);
        }

        public Supplier DeleteSupplier(int id)
        {
            return _supplierRepository.DeleteSupplier(id);
        }
    }
}
