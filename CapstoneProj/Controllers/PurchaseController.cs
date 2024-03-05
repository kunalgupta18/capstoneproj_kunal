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
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;

        public PurchaseController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Purchase>> GetAllPurchases()
        {
            var purchases = _purchaseService.GetAllPurchases();
            return Ok(purchases);
        }

        [HttpGet("{id}")]
        public ActionResult<Purchase> GetPurchaseById(int id)
        {
            var purchase = _purchaseService.GetPurchaseById(id);
            if (purchase == null)
            {
                return NotFound();
            }
            return Ok(purchase);
        }

        [HttpPost]
        public ActionResult AddPurchase(Purchase purchase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _purchaseService.AddPurchase(purchase);
            return CreatedAtAction(nameof(GetPurchaseById), new { id = purchase.PurchaseId }, purchase);
        }

        [HttpPut("{id}")]
        public ActionResult UpdatePurchase(int id, Purchase purchase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var existingPurchase = _purchaseService.GetPurchaseById(id);
                if (existingPurchase == null)
                {
                    return NotFound();
                }

                purchase.PurchaseId = id;
                _purchaseService.UpdatePurchase(purchase);
                return Ok(purchase);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeletePurchase(int id)
        {
            var existingPurchase = _purchaseService.GetPurchaseById(id);
            if (existingPurchase == null)
            {
                return NotFound();
            }

            _purchaseService.DeletePurchase(id);
            return NoContent();
        }
    }
}