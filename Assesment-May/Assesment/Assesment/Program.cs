////using Microsoft.AspNetCore.Identity;
////using Assesment.Models;
////using Microsoft.AspNetCore.Identity;
////using Microsoft.EntityFrameworkCore;
////using Microsoft.Extensions.Configuration;
////using Microsoft.AspNetCore.Authentication.JwtBearer;
////using Microsoft.IdentityModel.Tokens;
////using System.Text;
////using Microsoft.Extensions.DependencyInjection;

////var builder = WebApplication.CreateBuilder(args);

////// Add services to the container.
////builder.Services.AddControllersWithViews();
////builder.Services.AddDbContext<TaskManagerContext>(
////    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
////// Add services to the container.
//////builder.Services.AddDatabaseDeveloperPageExceptionFilter();
////builder.Services.AddDefaultIdentity<UserDetails>(options => options.SignIn.RequireConfirmedAccount = true)
////    .AddEntityFrameworkStores<TaskManagerContext>();
//////builder.Services.AddDefaultIdentity<UserDetails, IdentityRole>()
//////    .AddEntityFrameworkStores<TaskManagerContext>()
//////    .AddDefaultTokenProviders();
////builder.Services.AddControllersWithViews();
////var app = builder.Build();

////// Configure the HTTP request pipeline.
////if (!app.Environment.IsDevelopment())
////{
////    app.UseExceptionHandler("/Home/Error");
////    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
////    app.UseHsts();
////}

////app.UseHttpsRedirection();

////app.UseHttpsRedirection();
////app.UseStaticFiles();

////app.UseRouting();

////app.UseAuthentication();
////app.UseAuthorization();


////app.MapControllerRoute(
////    name: "default",
////    pattern: "{controller=Home}/{action=Index}/{id?}");

////app.Run();
////builder.Services.AddIdentityCore<UserDetails>(options => options.SignIn.RequireConfirmedAccount = true)
////        .AddEntityFrameworkStores<TaskManagerContext>();
////builder.Services.AddControllersWithViews();
////builder.Services.AddRazorPages();
//using Assesment.Models;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddControllersWithViews();
//builder.Services.AddDbContext<TaskManagerContext>(
//    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddIdentity<UserDetails, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<TaskManagerContext>();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthentication();
//app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.Run();
//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
//    var userManager = services.GetRequiredService<UserManager<UserDetails>>();

//    await CreateRolesAndAdminUser(roleManager, userManager);
//}

//static async Task CreateRolesAndAdminUser(RoleManager<IdentityRole> roleManager, UserManager<UserDetails> userManager)
//{
//    string[] roleNames = { "Admin", "User" };
//    IdentityResult roleResult;

//    foreach (var roleName in roleNames)
//    {
//        var roleExist = await roleManager.RoleExistsAsync(roleName);
//        if (!roleExist)
//        {
//            roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
//        }
//    }

//    var adminUser = new UserDetails
//    {
//        UserName = "admin@admin.com",
//        Email = "admin@admin.com"
//    };

//    string adminPassword = "Admin@123";
//    var user = await userManager.FindByEmailAsync(adminUser.Email);

//    if (user == null)
//    {
//        var createAdminUser = await userManager.CreateAsync(adminUser, adminPassword);
//        if (createAdminUser.Succeeded)
//        {
//            await userManager.AddToRoleAsync(adminUser, "Admin");
//        }
//    }
//}
using Assesment.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<TaskManagerContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<UserDetails, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
    .AddEntityFrameworkStores<TaskManagerContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<UserDetails>>();
    await CreateRolesAndAdminUser(roleManager, userManager);
}

async Task CreateRolesAndAdminUser(RoleManager<IdentityRole> roleManager, UserManager<UserDetails> userManager)
{
    string[] roleNames = { "Admin", "User" };
    IdentityResult roleResult;

    foreach (var roleName in roleNames)
    {
        var roleExist = await roleManager.RoleExistsAsync(roleName);
        if (!roleExist)
        {
            roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    var adminUser = new UserDetails
    {
        UserName = "admin@admin.com",
        Email = "admin@admin.com"
    };

    string adminPassword = "Admin@123";
    var user = await userManager.FindByEmailAsync(adminUser.Email);

    if (user == null)
    {
        var createAdminUser = await userManager.CreateAsync(adminUser, adminPassword);
        if (createAdminUser.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}

app.Run();
