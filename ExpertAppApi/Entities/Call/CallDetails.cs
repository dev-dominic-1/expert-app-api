using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ExpertAppApi.Entities.Call;

[Table("call_details")]
public class CallDetails
{
    public int Id { get; init; }
    
    public int CallId { get; init; }
    
    public DateOnly Date { get; set; }
    
    public TimeOnly Time { get; set; }
    
    [MaxLength(64)] public string? QuestionTitle { get; set; }
    
    [MaxLength(512)] public string? Question { get; set; }
    
    public int? Rating { get; set; }
    
    [MaxLength(512)] public string? Review { get; set; }
    
    [MaxLength(512)] public string? Comment { get; set; }

    [ForeignKey("CallId"), JsonIgnore]
    public virtual Call? Call { get; init; }
}