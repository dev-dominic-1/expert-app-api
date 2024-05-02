using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpertAppApi.Entities.Call;

[Table("call")]
public class Call
{
    public int Id { get; init; }
    
    public int ExpertId { get; init; }

    [MaxLength(64)] public string Title { get; set; } = "";
    
    [MaxLength(512)] public string? AdImageUrl { get; set; }
    
    public CallDetails? CallDetails { get; set; }
    
    [ForeignKey("ExpertId")]
    public virtual Expert? Expert { get; set; }
}