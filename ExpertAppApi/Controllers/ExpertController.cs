﻿using ExpertAppApi.Data;
using ExpertAppApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpertAppApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ExpertController(ILogger<ExpertController> logger, DataContext context)
{
    [HttpGet(Name = "GetAllExperts")]
    public async Task<IEnumerable<Expert>?> Get([FromQuery] bool includePhotoUrl, [FromQuery] bool includeFees)
    {
        var temp = context.Expert.AsQueryable();
        if (includePhotoUrl) temp = temp.Include(e => e.PhotoUrl);
        if (includeFees) temp = temp.Include(e => e.Fees);
        return await temp.ToListAsync();
    }
}