using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.DTO;

public class ComponentTranslation : DomainEntityMetaId
{
    [Column(TypeName = "jsonb")]
    public LangStr Translation { get; set; } = new();
    public string ComponentName { get; set; } = default!;
}