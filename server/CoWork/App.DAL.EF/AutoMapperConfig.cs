using AutoMapper;

namespace App.DAL.EF;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<App.DTO.Identity.AppUser, App.Domain.Identity.AppUser>().ReverseMap();
        CreateMap<App.DTO.Card, App.Domain.Card>().ReverseMap();
        CreateMap<App.DTO.Coordinate, App.Domain.Coordinate>().ReverseMap();
        CreateMap<App.DTO.ComponentTranslation, App.Domain.ComponentTranslation>().ReverseMap();
        CreateMap<App.DTO.CouponCategory, App.Domain.CouponCategory>().ReverseMap();
        CreateMap<App.DTO.ItemCategory, App.Domain.ItemCategory>().ReverseMap();
        CreateMap<App.DTO.ItemInOrder, App.Domain.ItemInOrder>().ReverseMap();
        CreateMap<App.DTO.MenuItem, App.Domain.MenuItem>().ReverseMap();
        CreateMap<App.DTO.Order, App.Domain.Order>().ReverseMap();
        CreateMap<App.DTO.Ticket, App.Domain.Ticket>().ReverseMap();
        CreateMap<App.DTO.TicketInOrder, App.Domain.TicketInOrder>().ReverseMap();
        CreateMap<App.DTO.UserCategory, App.Domain.UserCategory>().ReverseMap();
        CreateMap<App.DTO.UserCoupon, App.Domain.UserCoupon>().ReverseMap();
        CreateMap<App.DTO.UserInCategory, App.Domain.UserInCategory>().ReverseMap();
        CreateMap<App.DTO.UserLog, App.Domain.UserLog>().ReverseMap();
        CreateMap<App.DTO.CoordinateLocation, App.Domain.CoordinateLocation>().ReverseMap();
    }
}