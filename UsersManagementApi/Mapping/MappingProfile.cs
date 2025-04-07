using Domain.Entities;
using System.Net;
using UsersManagementApi.DTOs;

namespace UsersManagementApi.Mapping
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<UserAddress, AddressDTO>().ReverseMap();
            CreateMap<UserGiftCard, GiftCardDTO>().ReverseMap();
        }
    }
}