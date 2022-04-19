using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain;

public class ItemCategory : BaseEntity
{
    [MinLength(1)]
    [MaxLength(128)]
    [DisplayName("Name")]
    public string Name { get; set; } = default!;
    
    public ICollection<MenuItem>? MenuItems { get; set; }
}