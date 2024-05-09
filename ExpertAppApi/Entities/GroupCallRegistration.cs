using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ExpertAppApi.Entities;

[Table("group_call_registration")]
public class GroupCallRegistration
{
    public int Id { get; init; }
    
    public int GroupCallId { get; init; }
    
    public int UserId { get; init; }
    
    public DateOnly? RegistrationDate { get; set; }
    
    public TimeOnly? RegistrationTime { get; set; }

    public bool Canceled { get; set; } = false;
    
    [ForeignKey("GroupCallId"), JsonIgnore]
    public virtual GroupCall? GroupCall { get; set; }
    
    [ForeignKey("UserId")]
    public virtual User? User { get; set; }
}