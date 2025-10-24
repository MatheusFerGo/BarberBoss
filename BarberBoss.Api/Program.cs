using BarberBoss.Application;
using BarberBoss.Application.Interfaces;
using BarberBoss.Application.Services.Billings;
using BarberBoss.Application.Services.Reports;
using BarberBoss.Domain;
using BarberBoss.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

var serverVersion = new MySqlServerVersion(ServerVersion.AutoDetect(connectionString));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, serverVersion)
);

IServiceCollection serviceCollection = builder.Services.AddScoped<IBillingService, BillingService>();

builder.Services.AddScoped<IBillingRepository, BillingRepository>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IExcelService, ExcelService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();