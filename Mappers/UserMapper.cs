using AutoMapper;
using nettbutikk_api.Models.DTOs;
using nettbutikk_api.Models.Entities;

namespace nettbutikk_api.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
        }
    }
}
