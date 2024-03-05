using CapstoneProj.Models;
using CapstoneProj.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneProj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISalesService _salesservice;

        public SalesController(ISalesService service)
        {
            _salesservice = service;
        }

        [HttpGet("{id}")]
        public IActionResult GetSalesById(int id)
        {
            var sales = _salesservice.GetSalesById(id);
            if (sales == null)
            {
                return NotFound();
            }
            return Ok(sales);
        }

        [HttpGet]
        public IActionResult GetAllSales()
        {
            var salesList = _salesservice.GetAllSales();
            return Ok(salesList);
        }

        [HttpPost]
        public IActionResult AddSales([FromBody] Sales sales)
        {
            _salesservice.AddSales(sales);
            return CreatedAtAction(nameof(GetSalesById), new { id = sales.SalesId }, sales);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSales(int id, [FromBody] Sales sales)
        {
            if (id != sales.SalesId)
            {
                return BadRequest();
            }
            _salesservice.UpdateSales(sales);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSales(int id)
        {
            _salesservice.DeleteSales(id);
            return NoContent();
        }
    }
}
