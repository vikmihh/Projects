using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Domain;

public class ComponentTranslation : DomainEntityId
{
    //et-EE
    //et-RUS
    [Column(TypeName = "jsonb")]
    public LangStr Translation { get; set; } = new();
    public string ComponentName { get; set; } = default!;
} 