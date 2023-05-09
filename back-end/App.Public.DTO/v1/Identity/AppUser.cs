using System.ComponentModel.DataAnnotations;
using Base.Domain.Identity;

namespace App.Public.DTO.v1.Identity;

public class AppUser : BaseUser
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
}