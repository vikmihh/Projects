﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Public.DTO.v1;

public class ItemCategory : DomainEntityId
{
    [MinLength(1)]
    [MaxLength(128)]
    [DisplayName("Name")]
    public string Name { get; set; } = default!;
    
    public ICollection<MenuItem>? MenuItems { get; set; }
}