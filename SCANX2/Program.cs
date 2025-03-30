using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSession(); // ✅ Enable session storage
builder.Services.AddHttpContextAccessor(); // ✅ Allow accessing session data

// Add services to the container
builder.Services.AddDbContext<ScanXDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews();

var app = builder.Build();

// ✅ Enable serving static files (this must be BEFORE routing)
app.UseStaticFiles();
app.UseSession(); // ✅ Enable session usage in the app

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});


app.Run();
