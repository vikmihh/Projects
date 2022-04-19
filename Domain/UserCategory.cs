using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain;

public class UserCategory : BaseEntity
{
    [MinLength(1)]
    [MaxLength(128)]
    [DisplayName("Category name")]
    public string CategoryName { get; set; } = default!;
    
    public ICollection<UserInCategory>? UsersInCategories { get; set; }
}