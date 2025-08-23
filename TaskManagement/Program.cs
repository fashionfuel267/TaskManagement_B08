using Microsoft.EntityFrameworkCore;
using TaskManagement.Data;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<TaskDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<IdentityUser,IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<TaskDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.MapRazorPages();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
