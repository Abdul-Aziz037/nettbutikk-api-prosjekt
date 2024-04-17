using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using nettbutikk_api.Models.Entities;

namespace nettbutikk_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    //[HttpGet]
    //public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    //{
    //    var products = await _productService.GetProductsAsync();

    //    if (products == null)
    //    {
    //        return NotFound(); // Returnerer 404 Not Found hvis produkter ikke ble funnet
    //    }

    //    return Ok(products);
    //}
}
