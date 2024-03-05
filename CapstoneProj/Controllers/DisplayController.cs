using CapstoneProj.Models;
using CapstoneProj.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CapstoneProj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisplayController : ControllerBase
    {
        private readonly IDisplayService _displayService;

        public DisplayController(IDisplayService displayService)
        {
            _displayService = displayService;
        }

        [HttpGet]
        public ActionResult<SalesAndPurchaseSummary> GetSummary()
        {
            SalesAndPurchaseSummary summary = _displayService.GetSummary();
            if (summary == null)
            {
                return NotFound();
            }

            return Ok(summary);
        }
    }
}