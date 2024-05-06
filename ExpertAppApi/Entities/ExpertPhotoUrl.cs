using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ExpertAppApi.Entities;

[Table("expert_photo_url")]
public class ExpertPhotoUrl
{
    public int Id { get; init; }
    
    public int ExpertId { get; init; }

    [MaxLength(512)] public string Small { get; set; } = "";

    [MaxLength(512)] public string Medium { get; set; } = "";

    [MaxLength(512)] public string Large { get; set; } = "";
    
    [ForeignKey("ExpertId"), JsonIgnore]
    public virtual Expert? Expert { get; init; }
    
}