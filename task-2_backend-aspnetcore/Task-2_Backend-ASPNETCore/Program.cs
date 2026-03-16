using GVZ.Task2BackendASPNETCore;
using Mapster;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddMapster();
builder.Services.AddDbContext<MessagesContext>(opt => opt.UseInMemoryDatabase("Messages"));
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

var app = builder.Build();
app.MapOpenApi();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", "v1");
});
app.MapControllers();
app.UseRewriter(new RewriteOptions().AddRedirect("^$", "swagger"));
app.Run();

public partial class Program { }
