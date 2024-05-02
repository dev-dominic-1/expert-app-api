using ExpertAppApi.Data;
using ExpertAppApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpertAppApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ExpertController(ILogger<ExpertController> logger, DataContext context)
{
    private IQueryable<Expert> PrimeGetRequestQuery(bool includePhotoUrl, bool includeFees)
    {
        var temp = context.Expert.AsQueryable();
        if (includePhotoUrl) temp = temp.Include(e => e.PhotoUrl);
        if (includeFees) temp = temp.Include(e => e.Fees);
        return temp;
    }
    
    [HttpGet]
    public async Task<IEnumerable<Expert>?> Get([FromQuery] bool includePhotoUrl, [FromQuery] bool includeFees)
    {
        var temp = PrimeGetRequestQuery(includePhotoUrl, includeFees);
        return await temp.ToListAsync();
    }

    [HttpGet, Route("GetById")]
    public async Task<Expert?> GetById([FromQuery] int id, [FromQuery] bool includePhotoUrl, [FromQuery] bool includeFees)
    {
        var temp = PrimeGetRequestQuery(includePhotoUrl, includeFees);
        return await temp.Where(e => e.Id == id).FirstAsync();
    }
}