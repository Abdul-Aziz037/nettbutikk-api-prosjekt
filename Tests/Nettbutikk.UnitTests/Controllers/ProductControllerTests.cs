using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using nettbutikk_api.Controllers;
using nettbutikk_api.Models.DTOs;
using nettbutikk_api.Services;
using nettbutikk_api.Services.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Nettbutikk.UnitTests.Controllers;

public class ProductControllerTests
{
    private readonly ProductsController _productController;
    private readonly Mock<IProductService> _productServiceMock = new Mock<IProductService>();
    private readonly Mock<IUserService> _userServiceMock = new Mock<IUserService>();

    public ProductControllerTests()
    {
        _productController = new ProductsController(_productServiceMock.Object, _userServiceMock.Object);
    }

    [Fact]
    public async Task GetProductsAsync_ShouldReturn_ProductDTO()
    {
        //Arrange 
        var products = new List<ProductDTO>
    {
        new ProductDTO { Id = 1, Name = "bok", Price = 10 },
        new ProductDTO { Id = 2, Name = "PC", Price = 20 },
        new ProductDTO { Id = 3, Name = "bord", Price = 30 }
    };

        _productServiceMock.Setup(service => service.GetProductsAsync()).ReturnsAsync(products);

        //Act
        var resultat = await _productController.GetProducts();

        // Assert
        var ActionResultat = Assert.IsType<ActionResult<IEnumerable<ProductDTO>>>(resultat);
        var returnValue = Assert.IsType<OkObjectResult>(ActionResultat.Result);
        var dto_resultat = Assert.IsType<List<ProductDTO>>(returnValue.Value);

        var dto = products.FirstOrDefault();
        Assert.Equal("bok", dto?.Name);
    }

    [Fact]
    public async Task AddProductAsync_ShouldAddProduct()
    {
        // Arrange
        var productToAdd = new ProductDTO
        {
            Name = "Ny bok",
            Description = "En spennede bok",
            Price = 99
        };

        // Oppsett for HttpContext
        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.Name, "ahmed"),
        }, "mock"));

        var controllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        _productController.ControllerContext = controllerContext;

        _userServiceMock.Setup(service =>
        service.GetUserByNameAsync(It.IsAny<string>())).ReturnsAsync(new UserDTO { UserId = 1});

        _productServiceMock.Setup(service => 
        service.AddProductAsync(It.IsAny<ProductDTO>())).ReturnsAsync(productToAdd);

        // Act
        var result = await _productController.AddProductAsync(productToAdd);


        //Assert 
        var createdResult = Assert.IsType<OkObjectResult>(result.Result);
        var createdProduct = Assert.IsType<ProductDTO>(createdResult.Value);
        Assert.Equal(productToAdd.Name, createdProduct.Name);
        Assert.Equal(productToAdd.Description, createdProduct.Description);
        Assert.Equal(productToAdd.Price, createdProduct.Price);
    }
}
