using AutoMapper;
using nettbutikk_api.Models.DTOs;
using nettbutikk_api.Models.Entities;

namespace nettbutikk_api.Mappers
{
    public class UserRegMapper: Profile
    {
        public UserRegMapper()
        {
            CreateMap<UserRegDTO, User>();
        }
    }
}
