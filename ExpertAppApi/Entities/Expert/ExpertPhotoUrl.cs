using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpertAppApi.Entities;

public class ExpertPhotoUrl
{
    public int Id { get; init; }
    
    public int ExpertId { get; init; }

    [MaxLength(512)] public string Small { get; set; } = "";

    [MaxLength(512)] public string Medium { get; set; } = "";

    [MaxLength(512)] public string Large { get; set; } = "";
    
    [ForeignKey("FK__ExpertPhotoUrl_ExpertId__Expert_Id")]
    public virtual Expert? Expert { get; init; }
    
}