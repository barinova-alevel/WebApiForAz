using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SFMB.BL.Services;
using SFMB.BL.Services.Interfaces;
using SFMB.DAL;
using SFMB.DAL.Repositories;
using SFMB.DAL.Repositories.Interfaces;
using WebApiForAz.Middleware;

//IConfigurationRoot configurationBuilder = new ConfigurationBuilder()
//    .AddEnvironmentVariables("DefaultConnection")
//                //.AddJsonFile("appsettings.json")
//                .Build();

//var builder = WebApplication.CreateBuilder(args);

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables(); // Load ALL environment variables

builder.Services.AddControllers();
builder.Services.AddOpenApi();

//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var connectionString = builder.Configuration.GetValue<string>("DATABASE_URL");

builder.Services.AddDbContext<SfmbDbContext>(options =>
options.UseNpgsql(connectionString));

builder.Services.AddScoped<IOperationTypeRepository, OperationTypeRepository>();
builder.Services.AddScoped<IOperationRepository, OperationRepository>();
builder.Services.AddScoped<IDailyReportRepository, DailyReportRepository>();
builder.Services.AddScoped<IPeriodReportRepository, PeriodReportRepository>();
builder.Services.AddScoped<IOperationService, OperationService>();
builder.Services.AddScoped<IOperationTypeService, OperationTypeService>();
builder.Services.AddScoped<IDailyReportService, DailyReportService>();
builder.Services.AddScoped<IPeriodReportService, PeriodReportService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "SFM API",
        Version = "v1"
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

//if (app.Environment.IsDevelopment())
//{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();