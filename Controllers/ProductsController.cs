using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using nettbutikk_api.Models.Entities;

namespace nettbutikk_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    /*
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
       var products = await _productService.GetProductsAsync();

       if (products == null)
       {
           return NotFound();
       }
       return Ok(products);
    }

    [HttpGet("{productId}", Name = "GetProductId")]
    public async Task<ActionResult<ProductDTO>> GetPostByIdAsync(int productId)
    {
        var rest = await _productService.GetPostByIdAsync(postId);
        return rest != null ? Ok(rest) : NotFound("Fant ikke product");
    }



    */
}
