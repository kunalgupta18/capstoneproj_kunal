using CapstoneProj.Models;
using CapstoneProj.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CapstoneProj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Supplier>> GetAllSuppliers()
        {
            var suppliers = _supplierService.GetAllSuppliers();
            return Ok(suppliers);
        }

        [HttpGet("{id}")]
        public ActionResult<Supplier> GetSupplierById(int id)
        {
            var supplier = _supplierService.GetSupplierById(id);
            if (supplier == null)
            {
                return NotFound();
            }
            return Ok(supplier);
        }

        [HttpPost]
        public ActionResult AddSupplier(Supplier supplier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _supplierService.AddSupplier(supplier);
            return CreatedAtAction(nameof(GetSupplierById), new { id = supplier.SupplierId }, supplier);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateSupplier(int id, Supplier supplier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var existingSupplier = _supplierService.GetSupplierById(id);
                if (existingSupplier == null)
                {
                    return NotFound();
                }

                supplier.SupplierId = id;
                _supplierService.UpdateSupplier(supplier);
                return Ok(supplier);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteSupplier(int id)
        {
            var existingSupplier = _supplierService.GetSupplierById(id);
            if (existingSupplier == null)
            {
                return NotFound();
            }

            _supplierService.DeleteSupplier(id);
            return NoContent();
        }
    }
}