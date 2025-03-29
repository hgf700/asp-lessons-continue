using aspapp.Data; // Upewnij siê, ¿e masz przestrzeñ nazw 'Data' tutaj
using aspapp.Data.Models;
using aspapp.Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Rejestracja DbContext z po³¹czeniem
builder.Services.AddDbContext<trip_context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Rejestracja repozytoriów i serwisów
builder.Services.AddScoped<ITravelerRepository, TravelerRepository>();
builder.Services.AddScoped<IGuideRepository, GuideRepository>();
builder.Services.AddScoped<ITripRepository, TripRepository>();

builder.Services.AddControllersWithViews();

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
