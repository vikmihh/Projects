﻿using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class OrderMapper : BaseMapper<App.BLL.DTO.Order, App.DTO.Order>
{
    public OrderMapper(IMapper mapper) : base(mapper)
    {
    }
}