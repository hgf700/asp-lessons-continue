using aspapp.Data; // Upewnij si�, �e masz przestrze� nazw 'Data' tutaj
using aspapp.Data.Models;
using aspapp.Data.Repositories;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using aspapp.Data.Models.Validator;
using FluentValidation;
using AutoMapper;
using System.Reflection;
using aspapp.Data.Models.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Rejestracja DbContext z po��czeniem
builder.Services.AddDbContext<trip_context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Rejestracja repozytori�w i serwis�w
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

// Konfiguracja potoku przetwarzania ��da�
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
