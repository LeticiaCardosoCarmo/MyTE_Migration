using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyTE_Migration.Areas.Admin.Controllers;
using MyTE_Migration.Areas.Admin.Service;
using MyTE_Migration.Context;
using MyTE_Migration.Models;
using MyTE_Migration.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddDbContext<AppDbContext>((options) => options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));

builder.Services.AddDbContext<LoginDbContext>((options) => options.UseSqlServer(builder.Configuration["ConnectionStrings:AnotherConnection"]));

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddRoles<IdentityRole>().AddEntityFrameworkStores<LoginDbContext>().AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Account/Login";
    options.LogoutPath = $"/Account/Logout";
    options.AccessDeniedPath = $"/Account/AccessDenied";
});

builder.Services.AddScoped<IHourRepository, HourRepository>();
builder.Services.AddScoped<IWBSRepository, WBSRepository>();

// Add services to the container.
builder.Services.AddControllersWithViews();


// definindo a complexidade da senha

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 10;
    options.Password.RequiredUniqueChars = 3;
    options.Password.RequireNonAlphanumeric = false;
});

// registrando o serviço da criação do perfil e do user

builder.Services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();

builder.Services.AddAuthorization(options =>
{

    options.AddPolicy("RequiredUserAdminGerenteRole",
       policy => policy.RequireRole("User", "Admin", "Gerente"));

    options.AddPolicy("RequiredAdminGerenteRole",
        policy => policy.RequireRole("Admin", "Gerente"));

    options.AddPolicy("RequiredAdminRole",
       policy => policy.RequireRole("Admin"));
});

var app = builder.Build();


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

app.UseAuthentication();

app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
