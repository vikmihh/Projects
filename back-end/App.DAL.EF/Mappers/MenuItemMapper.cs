using App.DTO;
using AutoMapper;
using Base.Contracts;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class MenuItemMapper : BaseMapper<App.DTO.MenuItem, Domain.MenuItem>
{
    public MenuItemMapper(IMapper mapper) : base(mapper)
    {
    }
}