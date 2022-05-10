using AutoMapper;

namespace App.BLL;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<App.BLL.DTO.Identity.AppUser, App.DTO.Identity.AppUser>().ReverseMap();
        CreateMap<App.BLL.DTO.Card, App.DTO.Card>().ReverseMap();
        CreateMap<App.BLL.DTO.Coordinate, App.DTO.Coordinate>().ReverseMap();
        CreateMap<App.BLL.DTO.ComponentTranslation, App.DTO.ComponentTranslation>().ReverseMap();
        CreateMap<App.BLL.DTO.CouponCategory, App.DTO.CouponCategory>().ReverseMap();
        CreateMap<App.BLL.DTO.ItemCategory, App.DTO.ItemCategory>().ReverseMap();
        CreateMap<App.BLL.DTO.ItemInOrder, App.DTO.ItemInOrder>().ReverseMap();
        CreateMap<App.BLL.DTO.MenuItem, App.DTO.MenuItem>().ReverseMap();
        CreateMap<App.BLL.DTO.Order, App.DTO.Order>().ReverseMap();
        CreateMap<App.BLL.DTO.Ticket, App.DTO.Ticket>().ReverseMap();
        CreateMap<App.BLL.DTO.TicketInOrder, App.DTO.TicketInOrder>().ReverseMap();
        CreateMap<App.BLL.DTO.UserCategory, App.DTO.UserCategory>().ReverseMap();
        CreateMap<App.BLL.DTO.UserCoupon, App.DTO.UserCoupon>().ReverseMap();
        CreateMap<App.BLL.DTO.UserInCategory, App.DTO.UserInCategory>().ReverseMap();
        CreateMap<App.BLL.DTO.UserLog, App.DTO.UserLog>().ReverseMap();
    }
}