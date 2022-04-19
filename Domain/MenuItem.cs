﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain;

public class MenuItem : BaseEntity
{
    [MinLength(1)]
    [MaxLength(128)]
    [DisplayName("Item name")]
    public string ItemName { get; set; } = default!;
    
    [MaxLength(256)]
    [DisplayName("Description")]
    public string Description { get; set; } = default!;
    
    public decimal Price { get; set; } = default!;
    
    public ICollection<ItemInOrder>? ItemsInOrder { get; set; }
}