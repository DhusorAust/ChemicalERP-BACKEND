using Microsoft.EntityFrameworkCore;
using ChemicalERP.Controllers;
using ChemicalERP.Models;

var builder = WebApplication.CreateBuilder(args);

// MVC + JSON casing
builder.Services.AddControllers().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.PropertyNamingPolicy = null;
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers()
  .AddJsonOptions(o =>
  {
      o.JsonSerializerOptions.PropertyNamingPolicy = null;
      o.JsonSerializerOptions.DictionaryKeyPolicy = null;
  });

// DI
builder.Services.AddScoped(typeof(DapperContext<>), typeof(DapperContext<>));
// builder.Services.AddDbContext<ApplicationDBContext>(db =>
//     db.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

const string DevCors = "DevCors";

// CORS: when using credentials you MUST specify exact origins (no wildcard)
builder.Services.AddCors(options =>
{
    options.AddPolicy(DevCors, p => p
        .WithOrigins("http://localhost:4200")  // add more origins if you need
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials()
    );
});

ServiceList.RegisterServiceList(builder.Services);

var app = builder.Build();

// Swagger
app.UseSwagger();
app.UseSwaggerUI();

// If you don’t terminate TLS locally, you can comment HTTPS redirection out:
// app.UseHttpsRedirection();

// IMPORTANT: CORS BEFORE auth/endpoints
app.UseCors(DevCors);

app.UseAuthorization();

app.MapControllers();

app.Run();
