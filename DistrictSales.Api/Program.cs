using DistrictSales.Api.Middleware;
using DistrictSales.Api.SqlServer;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog();

builder.Services.AddSqlServer();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddDateOnlyTimeOnlyStringConverters();
builder.Services
    .AddControllers()
    .AddJsonOptions(options => options.AllowInputFormatterExceptionMessages = false);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.UseDateOnlyTimeOnlyStringConverters());

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
