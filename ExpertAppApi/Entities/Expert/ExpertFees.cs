namespace ExpertAppApi.Entities;

public class ExpertFees
{
    public int Id { get; init; }
    
    public int ExpertId { get; init; }
    
    public float FollowUpQuestion { get; set; }

    public virtual Expert? Expert { get; set; }
}