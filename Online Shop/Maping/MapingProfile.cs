using AutoMapper;
using Online_Shop.DTO;
using Online_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Shop.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserRegistrationDTO>().ReverseMap(); //Kazemo mu da mapira User na UserRegistrationDTO i obrnuto
            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<UserLoginDTO, User>().ReverseMap();
            CreateMap<AddProductDTO, Product>();
            CreateMap<Product,ProductDTO>();
            CreateMap<ItemDTO, Item>().ReverseMap();
            CreateMap<AddItemDTO, Item>();
            CreateMap<AddOrderDTO, Order>();
            CreateMap<Order, OrderDTO>();
            CreateMap<Order, OrderDTO>().ReverseMap();








        }
    }
}
