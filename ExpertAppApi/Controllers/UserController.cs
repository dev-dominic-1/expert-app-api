using System.Net;
using ExpertAppApi.Data;
using ExpertAppApi.Entities.User;
using ExpertAppApi.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ExpertAppApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(DataContext context, EncryptionService encrypt) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>?>> Get()
    {
        var temp = await context.User.ToListAsync();
        return Ok(temp);
    }

    [HttpGet, Route("GetById")]
    public async Task<IActionResult> GetById([FromQuery] int id)
    {
        var temp = await context.User.Where(e => e.Id == id).FirstOrDefaultAsync();
        if (temp == null) return NotFound("No User entry found with Id=" + id);
        return Ok(temp);
    }

    [HttpPost, Route("Login")]
    public async Task<IActionResult> Login([FromBody] UserCredentials credentials)
    {
        if (credentials.Username.IsNullOrEmpty()) return BadRequest("Username field must be included in request");
        if (credentials.Password.IsNullOrEmpty()) return BadRequest("Password field must be included in request");
        User? user = await context.User.Where(e => e.Username! == credentials.Username).FirstOrDefaultAsync();
        if (user == null) return NotFound("No User entry found with Username=" + credentials.Username);
        
        Console.WriteLine("User: " + user.Password);

        if (encrypt.Verify(user.Password!, credentials.Password!)) return Ok();
        
        return BadRequest("Credentials do not match");
    }

    [HttpPost, Route("Register")]
    public async Task<IActionResult> AddUser([FromBody] User user)
    {
        var newUser = new User()
        {
            Name = user.Name,
            Username = user.Username,
            Password = encrypt.Encrypt(user.Password)
        };
        try
        {
            context.User.Add(newUser);
            await context.SaveChangesAsync();
            return Ok(newUser);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveUser([FromQuery] int id)
    {
        var entry = await context.User.FindAsync(id);
        if (entry == null) return NotFound("No User entry found matching Id=" + id);
        context.User.Remove(entry);
        try
        {
            await context.SaveChangesAsync();
            return Ok(entry);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
}