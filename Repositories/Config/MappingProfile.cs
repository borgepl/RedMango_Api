using DataAccess.Data.Domain;
using AutoMapper;
using Models.DTO;
using Models.DTO.Order;
using Models.DTO.Login;
using DataAccess.Data.Identity;

namespace Repositories.Config
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<MenuItemDTO, MenuItem>().ReverseMap();
            CreateMap<CartItemDTO, CartItem>().ReverseMap();
            CreateMap<ShoppingCartDTO, ShoppingCart>().ReverseMap();
            CreateMap<OrderHeaderDTO, OrderHeader>();
            CreateMap<OrderHeader, OrderHeaderDTO>()
                .ForMember(x => x.OrderDetailsDTO, opt => opt.MapFrom(c => c.OrderDetails));
            CreateMap<OrderHeaderCreateDTO, OrderHeader>();
            CreateMap<OrderDetailsDTO, OrderDetail>().ReverseMap();
            CreateMap<ApplicationUser, UserDTO>()
                .ForMember(x => x.PhoneNo, opt => opt.MapFrom(c => c.PhoneNumber));

            //CreateMap<RoomOrderDetails, RoomOrderDetailsDTO>()
            //    .ForMember(x=> x.HotelRoomDTO, opt => opt.MapFrom(c => c.HotelRoom));

        }
    }
}
