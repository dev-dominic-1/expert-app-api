using ExpertAppApi.Data;
using ExpertAppApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpertAppApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ExpertFeesController(DataContext context) : ControllerBase
{
    public async Task<IActionResult> Get()
    {
        return Ok(await context.ExpertFee.ToListAsync());
    }

    [HttpPost]
    public async Task<ActionResult<ExpertFees>> AddExpertFees([FromBody] ExpertFees data)
    {
        try
        {
            context.ExpertFee.Add(data);
            await context.SaveChangesAsync();
            return Ok(data);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
}