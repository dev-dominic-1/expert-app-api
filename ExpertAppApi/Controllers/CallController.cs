using ExpertAppApi.Data;
using ExpertAppApi.Entities.Call;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpertAppApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CallController(DataContext context)
{
    private IQueryable<Call> PrimeGetRequestQuery(bool includeCallDetails, bool includeExpert)
    {
        var temp = context.Call.AsQueryable();
        if (includeCallDetails) temp = temp.Include(e => e.CallDetails);
        if (includeExpert)
        {
            temp = temp.Include(e => e.Expert).ThenInclude(e => e!.PhotoUrl);
            temp = temp.Include(e => e.Expert).ThenInclude(e => e!.Fees);
        }
        return temp;
    }
    
    [HttpGet]
    public async Task<IEnumerable<Call>> Get([FromQuery] bool includeCallDetails, [FromQuery] bool includeExpert)
    {
        return await PrimeGetRequestQuery(includeCallDetails, includeExpert).ToListAsync();
    }

    [HttpGet, Route("GetById")]
    public async Task<ActionResult<Call?>> GetById([FromQuery] int id, [FromQuery] bool includeCallDetails, [FromQuery] bool includeExpert)
    {
        var temp = PrimeGetRequestQuery(includeCallDetails, includeExpert);
        return await temp.Where(e => e.Id == id).FirstAsync();
    }
}