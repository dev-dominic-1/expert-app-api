using ExpertAppApi.Data;
using ExpertAppApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpertAppApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserPhotoUrlController(DataContext context) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await context.UserPhotoUrl.ToListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> AddUserPhotoUrl([FromBody] UserPhotoUrl data)
    {
        try
        {
            context.UserPhotoUrl.Add(data);
            return Ok(await context.SaveChangesAsync());
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
}