using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Domain;

public class ComponentTranslation : DomainEntityMetaId
{
    //et-EE
    //et-RUS
    [Column(TypeName = "jsonb")]
    public string Translation { get; set; }  = default!;
    public string ComponentName { get; set; } = default!;
} 