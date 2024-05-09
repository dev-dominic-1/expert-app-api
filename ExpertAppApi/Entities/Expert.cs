using System.Collections;
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

    /// <summary>
    /// Here for 1-way binding purposes. Query this data from <i>CallController.GetByExpertId</i>
    /// </summary>
    [JsonIgnore]
    public IEnumerable<Call>? Calls { get; } = null;

    [JsonIgnore] public IEnumerable<GroupCall>? GroupCalls { get; } = null;

}