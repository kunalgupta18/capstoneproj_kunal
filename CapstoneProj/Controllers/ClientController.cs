using CapstoneProj.Models;
using CapstoneProj.Services;
using CapstoneProj.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneProj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public IActionResult GetDetails()
        {
            var details = _clientService.GetDetails();
            return Ok(details);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteClient(int id)
        {
            var existingProduct = _clientService.GetClientById(id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            _clientService.DeleteClient(id);
            return NoContent();
        }
    }
}
