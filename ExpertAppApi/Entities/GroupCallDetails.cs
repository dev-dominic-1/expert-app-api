using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ExpertAppApi.Entities;

[Table(("group_call_details"))]
public class GroupCallDetails
{
    public int Id { get; init; }
    
    public int GroupCallId { get; init; }

    [MaxLength(512)] public string Description { get; set; } = "";
    
    public DateOnly? Date { get; set; }
    
    public TimeOnly? Time { get; set; }

    public bool Canceled { get; set; } = false;

    public bool Postponed { get; set; } = false;
    
    [ForeignKey("GroupCallId"), JsonIgnore]
    public virtual GroupCall? GroupCall { get; set; }
}