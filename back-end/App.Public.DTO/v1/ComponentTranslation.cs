using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Public.DTO.v1;

public class ComponentTranslation : DomainEntityId
{
    [Column(TypeName = "jsonb")]
    public LangStr Translation { get; set; } = new();
    public string ComponentName { get; set; } = default!;
} 