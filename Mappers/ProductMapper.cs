using AutoMapper;
using nettbutikk_api.Models.DTOs;
using nettbutikk_api.Models.Entities;

namespace nettbutikk_api.Mappers
{
    public class ProductMapper: Profile
    {
        public ProductMapper()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO, Product>();
        }
    }
}
