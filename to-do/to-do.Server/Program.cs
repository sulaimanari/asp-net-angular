using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using to_do.Server.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<to_doServerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("to_doServerContext") ?? throw new InvalidOperationException("Connection string 'to_doServerContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
