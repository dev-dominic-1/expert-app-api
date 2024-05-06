using System.Text.Json.Serialization;
using ExpertAppApi.Data;
using ExpertAppApi.Utilities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// WHITELIST FOR CORS
var corsOrigins = "_CorsOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsOrigins, policy =>
    {
        policy.WithOrigins(
                "http://localhost:8081", 
                "https://web-host-expert-app.netlify.app"
            )
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Add services to the container.

builder.Services.AddMvc();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var sqlConnection = builder.Configuration["ConnectionStrings:ExpertApp:Sql"];
builder.Services.AddSqlServer<DataContext>(sqlConnection, options => options.EnableRetryOnFailure());

// ENCRYPTION PARAMETERS
var saltSize = builder.Configuration["EncryptionService:SaltSize"];
var keySize = builder.Configuration["EncryptionService:KeySize"];
var iterations = builder.Configuration["EncryptionService:Iterations"];
var delimiter = builder.Configuration["EncryptionService:Delimiter"];
string?[] encryptionParameters = new[] { saltSize, keySize, iterations, delimiter };

if (!(encryptionParameters.Contains(null) || encryptionParameters.Contains("")))
{
    builder.Services.AddSingleton(new EncryptionService(
        int.Parse(saltSize!), 
        int.Parse(keySize!), 
        int.Parse(iterations!), 
        delimiter![0]
    ));
}


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors(corsOrigins);

// app.UseAuthorization();

app.MapControllers();

app.Run();
