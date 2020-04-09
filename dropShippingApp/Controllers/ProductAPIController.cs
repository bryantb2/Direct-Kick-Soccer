using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using dropShippingApp.Models;
using dropShippingApp.Data.Repositories;
//using dropShippingApp.APIModels;

namespace dropShippingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        private ICustomProductRepo customProductRepo;

        [HttpGet]
        [Route("CustomProductData/{productId}")]
        public async Task<IActionResult> GetProductData([FromRoute] int productId)
        {
            var foundProduct = await customProductRepo.GetCustomProductById(productId);
            if (foundProduct != null)
                return Ok(foundProduct);
            return NotFound("Custom product with Id of " + productId.ToString() + " does not exist");
        }

        /*[HttpPost]
        [Route("CalculateCartTotal")]
        public async Task<IActionResult> CalculateCartTotal([FromBody] CartItem[] cartItems)
        {
            // waiting on Raza to determine if custom products can have their own pricing
            decimal runningTotal = 0m;
            for(var i = 0; i < cartItems.Length; i++)
            {
                var currentItem = cartItems[i];
                var productData = await customProductRepo.GetCustomProductById(currentItem.CustomProductId);
                runningTotal += (productData.)
            }
        }*/
    }
}