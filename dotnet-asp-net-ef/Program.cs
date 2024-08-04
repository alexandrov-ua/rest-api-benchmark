using AuthorsDbRest.EF.Model;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();
builder.Configuration.GetConnectionString("Default");
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContextPool<MyDbContext>(
    optionsBuilder =>
    {
        optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
    }, poolSize: 100);
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();