using BusineessLogic.Repository;
using DataAccess.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Page.EmailServices;
using Page.ViewModel;
using Page.WorkerSevices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddTransient<IEmailService, EmailService>();
// Add database context
builder.Services.AddDbContext<FUMiniHotelManagementContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DB")));
// add mail setting
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

// Add SignalR
builder.Services.AddSignalR();
// add repo
builder.Services.AddScoped<IBookingRepository, BookingRepository>();

// Configure session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Adjust the session timeout as needed
});
builder.Services.AddHostedService<RoomStatusBackgroundService>();
// Add BookingRepository
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
// Configure cookie authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login"; // Set the login page path
        options.AccessDeniedPath = "/AccessDenied"; // Set the access denied page path
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

// Configure endpoints including SignalR Hub
app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
    endpoints.MapHub<BookingHub>("/bookingHub");
});

app.Run();