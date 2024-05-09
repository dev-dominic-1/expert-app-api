using ExpertAppApi.Data;
using ExpertAppApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpertAppApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CallController(DataContext context) : ControllerBase
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

    [HttpGet, Route("GetByExpertId")]
    public async Task<IEnumerable<Call>?> GetByExpertId([FromQuery] int expertId)
    {
        var temp = PrimeGetRequestQuery(true, false);
        return await temp.Where(e => e.ExpertId == expertId).ToListAsync();
    }

    [HttpGet, Route("GetUpcomingCalls")]
    public async Task<IActionResult> GetUpcomingCalls([FromQuery] int? userId, [FromQuery] bool? showMore, [FromQuery] bool? getAll)
    {
        try
        {
            string now = DateTime.Now.ToString("yyyy`-`MM`-`dd` `HH`:`mm`:`ss");
            var initialData = context.Call
                .FromSql(
                    $"SELECT * FROM dbo.call WHERE EXISTS(SELECT 1 FROM dbo.call_details cd WHERE CONCAT_WS(' ', cd.Date, cd.Time) > {now})"
                ).Select(e => e.Id);
            if (userId is null || getAll != true)
            {
                initialData = initialData.Take(showMore == true ? 30 : 10);
            }
            List<int> results = initialData.ToList();
            
            // NO RESULTS CASE
            if (results.Count == 0) return Ok(new Call[] {});
            
            // STANDARD CASES
            var query = PrimeGetRequestQuery(true, true).Where(e => results.Contains(e.Id));
            if (userId is null) return Ok(await query.ToListAsync());
            return Ok(await query.Where(e => e.UserId == userId).ToListAsync());
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddCall([FromBody] Call data)
    {
        context.Call.Add(data);
        try
        {
            await context.SaveChangesAsync();
            return Ok(data);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
}