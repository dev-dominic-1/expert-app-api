using System.Net;
using ExpertAppApi.Data;
using ExpertAppApi.Utilities;
using ExpertAppApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ExpertAppApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(DataContext context, EncryptionService encrypt) : ControllerBase
{
    private IQueryable<User> PrimeGetRequestQuery(bool includePhotoUrl)
    {
        var temp = context.User.AsQueryable();
        if (includePhotoUrl) temp = temp.Include(e => e.PhotoUrl);
        return temp;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>?>> Get([FromQuery] bool includePhotoUrl)
    {
        var temp = PrimeGetRequestQuery(includePhotoUrl);
        return Ok(await temp.ToListAsync());
    }

    [HttpGet, Route("GetById")]
    public async Task<IActionResult> GetById([FromQuery] int id, [FromQuery] bool includePhotoUrl)
    {
        var temp = PrimeGetRequestQuery(includePhotoUrl);
        var result = await temp.Where(e => e.Id == id).FirstOrDefaultAsync();
        if (result == null) return NotFound("No User entry found with Id=" + id);
        return Ok(result);
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

    [HttpPatch, Route("SetPassword")]
    public async Task<IActionResult> SetPassword([FromBody] SetPassword data)
    {
        var task = context.User.FindAsync(data.Id);
        if (data.Password.IsNullOrEmpty() || data.Password.Length < 8)
            return BadRequest("Password must be at least 8 characters");
        User? user = await task;
        if (user == null) return NotFound("No User entry found with Id=" + data.Id);
        user.Password = encrypt.Encrypt(data.Password);
        await context.SaveChangesAsync();
        return Ok(user);
    }

    [HttpPatch, Route("SetName")]
    public async Task<IActionResult> SetName([FromBody] SetName data)
    {
        var task = context.User.FindAsync(data.Id);
        if (data.Name.IsNullOrEmpty()) return BadRequest("Name field must be included in request");
        User? user = await task;
        if (user == null) return NotFound("No User entry found with Id=" + data.Id);
        user.Name = data.Name;
        await context.SaveChangesAsync();
        return Ok(user);
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