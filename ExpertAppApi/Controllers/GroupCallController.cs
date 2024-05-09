using ExpertAppApi.Data;
using ExpertAppApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpertAppApi.Controllers;

[ApiController]
[Route("[controller]")]
public class GroupCallController(DataContext context) : ControllerBase
{
    private IQueryable<GroupCall> PrimeGetRequestQuery(
        bool? includeExpert, 
        bool? includeCallDetails, 
        bool? includeRegistrations, 
        bool? includeRegistrationUserData
    )
    {
        var temp = context.GroupCall.AsQueryable();
        if (includeExpert == true)
        {
            temp = temp.Include(e => e.Expert).ThenInclude(e => e!.PhotoUrl);
            temp = temp.Include(e => e.Expert).ThenInclude(e => e!.Fees);
        }
        if (includeCallDetails == true) temp = temp.Include(e => e.GroupCallDetails);
        if (includeRegistrations == true) temp = temp.Include(e => e.GroupCallRegistrations);
        if (includeRegistrationUserData == true) temp = temp.Include(e => e.GroupCallRegistrations)!.ThenInclude(e => e.User)!.ThenInclude(e => e!.PhotoUrl);
        return temp;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] bool includeExpert, 
        [FromQuery] bool includeCallDetails, 
        [FromQuery] bool includeRegistrations, 
        [FromQuery] bool includeRegistrationUserData
    )
    {
        var query = PrimeGetRequestQuery(
            includeExpert, 
            includeCallDetails, 
            includeRegistrations, 
            includeRegistrationUserData
        );
        return Ok(await query.ToListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> AddGroupCall(GroupCall data)
    {
        try
        {
            context.GroupCall.Add(data);
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
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteGroupCall([FromQuery] int id)
    {
        var call = await context.GroupCall.FindAsync(id);
        if (call is null) return NotFound("No GroupCall entry found with Id=" + id);
        context.GroupCall.Remove(call);
        try
        {
            await context.SaveChangesAsync();
            return Ok(call);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
}