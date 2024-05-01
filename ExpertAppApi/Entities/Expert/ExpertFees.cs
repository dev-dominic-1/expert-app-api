using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ExpertAppApi.Entities;

[Table("expert_fees")]
public class ExpertFees
{
    public int Id { get; init; }
    
    public int ExpertId { get; init; }
    
    public double FollowUpQuestion { get; set; }

    [ForeignKey("ExpertId"), JsonIgnore]
    public virtual Expert? Expert { get; init; }
}