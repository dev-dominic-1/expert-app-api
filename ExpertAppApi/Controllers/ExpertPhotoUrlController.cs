using ExpertAppApi.Data;
using ExpertAppApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpertAppApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ExpertPhotoUrlController(DataContext context) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await context.ExpertPhotoUrl.ToListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> AddExpertPhotoUrl([FromBody] ExpertPhotoUrl data)
    {
        context.ExpertPhotoUrl.Add(data);
        return Ok(await context.SaveChangesAsync());
    }
}