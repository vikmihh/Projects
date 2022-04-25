﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class Order : DomainEntityMetaId
{
    public DateTime DeletedAt { get; set; } = default!;
    
    public decimal Price { get; set; } = default!;
    
    public decimal Discount { get; set; } = default!;
    
    public decimal FinalPrice { get; set; } = default!;
    
    public int OrderNr { get; set; } = default!;
    
    [MaxLength(256)]
    [DisplayName("Description")]
    public string Description { get; set; } = default!;
    
    public ICollection<TicketInOrder>? TicketsInOrder { get; set; }
    public ICollection<ItemInOrder>? ItemsInOrder { get; set; }
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}