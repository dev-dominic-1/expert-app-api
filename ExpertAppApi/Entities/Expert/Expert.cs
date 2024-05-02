using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ExpertAppApi.Entities;

public class Expert
{
    public int Id { get; init; }

    [MaxLength(64)]
    public string Name { get; set; } = "";

    [MaxLength(64)]
    public string Bio { get; set; } = "";

    public ExpertPhotoUrl? PhotoUrl { get; set; }

    public ExpertFees? Fees { get; set; }
    
    [JsonIgnore]
    public IEnumerable<Call.Call>? Calls { get; set; }

}