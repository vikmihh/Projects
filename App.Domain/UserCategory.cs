using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class UserCategory : DomainEntityMetaId
{
    [MinLength(1)]
    [MaxLength(128)]
    [DisplayName("Category name")]
    public string CategoryName { get; set; } = default!;
    
    public ICollection<UserInCategory>? UsersInCategories { get; set; }
}