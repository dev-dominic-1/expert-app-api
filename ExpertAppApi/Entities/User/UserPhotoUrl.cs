using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ExpertAppApi.Entities.User;

[Table("app_user_photo_url")]
public class UserPhotoUrl
{
    public int Id { get; init; }
    
    public int UserId { get; init; }

    [MaxLength(512)] public string Small { get; set; } = "";
    
    [MaxLength(512)] public string Medium { get; set; } = "";
    
    [MaxLength(512)] public string Large { get; set; } = "";
    
    [ForeignKey("UserId"), JsonIgnore]
    public virtual User? User { get; init; }
}