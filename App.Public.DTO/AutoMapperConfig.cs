using AutoMapper;

namespace App.Public.DTO;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<App.BLL.DTO.Identity.AppUser, App.Public.DTO.v1.Identity.AppUser>().ReverseMap();
        CreateMap<App.BLL.DTO.Card, App.Public.DTO.v1.Card>().ReverseMap();
        CreateMap<App.BLL.DTO.Coordinate, App.Public.DTO.v1.Coordinate>().ReverseMap();
        CreateMap<App.BLL.DTO.ComponentTranslation, App.Public.DTO.v1.ComponentTranslation>().ReverseMap();
        CreateMap<App.BLL.DTO.CouponCategory, App.Public.DTO.v1.CouponCategory>().ReverseMap();
        CreateMap<App.BLL.DTO.ItemCategory, App.Public.DTO.v1.ItemCategory>().ReverseMap();
        CreateMap<App.BLL.DTO.ItemInOrder, App.Public.DTO.v1.ItemInOrder>().ReverseMap();
        CreateMap<App.BLL.DTO.MenuItem, App.Public.DTO.v1.MenuItem>().ReverseMap();
        CreateMap<App.BLL.DTO.Order, App.Public.DTO.v1.Order>().ReverseMap();
        CreateMap<App.BLL.DTO.Ticket, App.Public.DTO.v1.Ticket>().ReverseMap();
        CreateMap<App.BLL.DTO.TicketInOrder, App.Public.DTO.v1.TicketInOrder>().ReverseMap();
        CreateMap<App.BLL.DTO.UserCategory, App.Public.DTO.v1.UserCategory>().ReverseMap();
        CreateMap<App.BLL.DTO.UserCoupon, App.Public.DTO.v1.UserCoupon>().ReverseMap();
        CreateMap<App.BLL.DTO.UserInCategory, App.Public.DTO.v1.UserInCategory>().ReverseMap();
        CreateMap<App.BLL.DTO.UserLog, App.Public.DTO.v1.UserLog>().ReverseMap();
    }
}