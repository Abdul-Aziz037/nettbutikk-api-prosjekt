using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using nettbutikk_api.Models.DTOs;
using nettbutikk_api.Models.Entities;
using nettbutikk_api.Services;
using nettbutikk_api.Services.Interfaces;

namespace nettbutikk_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    
    private readonly IProductService _productService;
    private readonly IUserService _userService;

    public ProductsController(IProductService productService, IUserService userService)
    {
        _productService = productService;
        _userService=userService;
    }
    [HttpGet(Name ="GetProducts")]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
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
    [Authorize(Roles = "Admin, User")]
    public async Task<ActionResult<ProductDTO>> AddProductAsync(ProductDTO productDTO)
    {
        // henter brukeren id fra token
        var userName = HttpContext.User.Identity?.Name!;
        var user = await _userService.GetUserByNameAsync(userName);

        if (user != null)
        {
            productDTO.UserId = user.UserId;
        }

        var product = await _productService.AddProductAsync(productDTO);
        if (product != null)
        {
            return Ok(product);
        }
        return BadRequest("Klarte ikke å legge til nytt product.");
    }

    [HttpPut("{productId}", Name = "UpdateProduct")]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<ProductDTO>> UpdateProductAsync(int productId, ProductDTO productDTO)
    {
        var userName = HttpContext.User.Identity?.Name!;
        var user = await _userService.GetUserByNameAsync(userName);


        var product = await _productService.GetProductByIdAsync(productId);
        if (product == null)
        {
            return NotFound("Produktet ble ikke funnet.");
        }

        if (product.UserId != user?.UserId && !User.IsInRole("Admin"))
        {
            return Forbid();
        }


        var res = await _productService.UpdateProductAsync(productId, productDTO);
        return res != null ? Ok(res) : NotFound("fikk ikke oppdatert produktet");
    }

    [HttpDelete("{productId}", Name = "DeleteProduct")]
    [Authorize(Roles = "Admin, User")]
    public async Task<ActionResult<ProductDTO>> DeleteProductAsync(int productId)
    {
        var userName = HttpContext.User.Identity?.Name!;
        var user = await _userService.GetUserByNameAsync(userName);

        var product = await _productService.GetProductByIdAsync(productId);
        if (product == null)
        {
            return NotFound("produktet ble ikke funnet.");
        }

        if (product.UserId != user?.UserId && !User.IsInRole("Admin"))
        {
            return Forbid();
        }
        var res = await _productService.DeleteProductAsync(productId);
        return res != null ? Ok(res) : BadRequest("Fikk ikke slettet produktet");
    }
}
