#pragma warning disable CS1591
using Cnab.Api.WebApi;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Environment);

startup.ConfigureServices(builder.Services);
builder.Host.UseSerilog();
var app = builder.Build();

startup.Configure(app, app.Environment);
app.Run();

#pragma warning restore CS1591