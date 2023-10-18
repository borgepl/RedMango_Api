using DataAccess.Data.Domain;
using AutoMapper;
using Models.DTO;

namespace Repositories.Config
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<MenuItemDTO, MenuItem>().ReverseMap();
            
            //CreateMap<RoomOrderDetails, RoomOrderDetailsDTO>()
            //    .ForMember(x=> x.HotelRoomDTO, opt => opt.MapFrom(c => c.HotelRoom));
            
        }
    }
}
