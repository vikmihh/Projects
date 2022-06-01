﻿using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class UserCoupon : DomainEntityMetaId
{
    
    public string PromoCode { get; set; } = default!;
    
    public bool IsUsed { get; set; } = default!;
    
    
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
    public Guid CouponCategoryId { get; set; }
    public CouponCategory? CouponCategory { get; set; }
    
    public Guid? OrderId { get; set; }
    public Order? Order { get; set; }
}