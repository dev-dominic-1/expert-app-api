using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpertAppApi.Entities;

[Table("group_call")]
public class GroupCall
{
    public int Id { get; init; }
    
    public int ExpertId { get; init; }

    [MaxLength(64)] public string Title { get; set; } = "";

    [MaxLength(512)] public string AdImageUrl { get; set; } = "";
    
    public GroupCallDetails? GroupCallDetails { get; set; }
    
    public IEnumerable<GroupCallRegistration>? GroupCallRegistrations { get; set; }
    
    [ForeignKey("ExpertId")]
    public virtual Expert? Expert { get; set; }
}