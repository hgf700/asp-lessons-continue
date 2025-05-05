using aspapp.Models;
using aspapp.Repositories;
using aspapp.Services;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using AutoMapper;
using System.Reflection;
using aspapp.Models;
using aspapp.Repositories;
using aspapp.Services;
using aspapp.Models.Mapper;
using aspapp.Models.Validator;
using aspapp.Models.VM;

var builder = WebApplication.CreateBuilder(args);

// Rejestracja DbContext z po³¹czeniem
builder.Services.AddDbContext<trip_context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Rejestracja repozytoriów i serwisów
builder.Services.AddScoped<ITravelerRepository, TravelerRepository>();
builder.Services.AddScoped<IGuideRepository, GuideRepository>();
builder.Services.AddScoped<ITripRepository, TripRepository>();

builder.Services.AddControllersWithViews();

builder.Services.AddValidatorsFromAssemblyContaining<TripViewModelValidator>();

//builder.Services.AddValidatorsFromAssemblyContaining<TravelerViewModelValidator>();

//builder.Services.AddValidatorsFromAssemblyContaining<GuideViewModelValidator>();

var configuration = new MapperConfiguration(cfg => cfg.AddMaps(typeof(TripMapper).Assembly));
var mapper = new Mapper(configuration);

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
