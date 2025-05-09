using aspapp.Models;
using aspapp.Repositories;
using aspapp.Services;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
//using aspapp.Models.Validator;
using Serilog;
using AutoMapper;


var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// Rejestracja DbContext z po³¹czeniem
builder.Services.AddDbContext<trip_context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Rejestracja repozytoriów i serwisów
builder.Services.AddScoped<ITravelerRepository, TravelerRepository>();
builder.Services.AddScoped<IGuideRepository, GuideRepository>();
builder.Services.AddScoped<ITripRepository, TripRepository>();

builder.Services.AddScoped<ITravelerService, TravelerService>();
builder.Services.AddScoped<ITripService, TripService>();
builder.Services.AddScoped<IGuideService, GuideService>();

builder.Services.AddControllersWithViews();

//builder.Services.AddValidatorsFromAssemblyContaining<TripViewModelValidator>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Konfiguracja potoku przetwarzania ¿¹dañ
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
