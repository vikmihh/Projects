using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class MenuItemMapper : BaseMapper<App.BLL.DTO.MenuItem, App.DTO.MenuItem>
{
    public MenuItemMapper(IMapper mapper) : base(mapper)
    {
    }
}