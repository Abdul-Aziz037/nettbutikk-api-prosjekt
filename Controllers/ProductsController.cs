using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using nettbutikk_api.Models.DTOs;
using nettbutikk_api.Models.Entities;
using nettbutikk_api.Services;

namespace nettbutikk_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }
    [HttpGet(Name ="GetProducts")]
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
    public async Task<ActionResult<ProductDTO>> GetProductByIdAsync(int productId)
    {
        var rest = await _productService.GetProductByIdAsync(productId);
        return rest != null ? Ok(rest) : NotFound("Fant ikke product");
    }

    [HttpPost(Name = "AddProduct")]
    public async Task<ActionResult<ProductDTO>> AddProductAsync(ProductDTO productDTO)
    {
        var rest = await _productService.AddProductAsync(productDTO);
        if (rest != null)
        {
            return Ok(rest);
        }
        return BadRequest("Klarte ikke å legge til nytt product.");
    }

    [HttpPut("{productId}", Name = "UpdateProduct")]
    public async Task<ActionResult<ProductDTO>> UpdateProductAsync(int productId, ProductDTO productDTO)
    {
        var res = await _productService.UpdateProductAsync(productId, productDTO);
        return res != null ? Ok(res) : NotFound("fikk ikke oppdatert post");
    }

    [HttpDelete("{productId}", Name = "DeleteProduct")]
    public async Task<ActionResult<ProductDTO>> DeleteProductAsync(int productId)
    {
        var res = await _productService.DeleteProductAsync(productId);
        return res != null ? Ok(res) : BadRequest("fikk ikke slettet innlegget.");
    }
}
