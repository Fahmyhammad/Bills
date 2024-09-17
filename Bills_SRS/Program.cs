using bill_DataAccess.Data;
using bill_DataAccess.Implementation;
using bill_Entities.Repoistory;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using bill_Entities.Models;
using bill_DataAccess.Seed;
using bill_Entities.Mapper;
using Microsoft.AspNetCore.Localization;
using bill_Entities.ServicesRegistrations;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Globalization;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnBills")));

builder.Services.AddIdentity<AppUser,IdentityRole>().AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultUI().AddDefaultTokenProviders();

// My Services 
builder.Services.AddServicesRegistrations();




builder.Services.AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
.AddDataAnnotationsLocalization();

builder.Services.AddLocalization(opt => opt.ResourcesPath = "");


var supportedCultures = new[] {
      new CultureInfo("ar-EG"),
      new CultureInfo("en-US"),
};









var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en-US"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures,
    RequestCultureProviders = new List<IRequestCultureProvider>
                {
                new QueryStringRequestCultureProvider(),
                new CookieRequestCultureProvider()
                }
});



app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Admin}/{controller=Home}/{action=Index}/{id?}");


app.MapControllerRoute(
  name: "default",
  pattern: "{controller=Home}/{action=Index}/{id?}");


using var Scope = app.Services.CreateScope();

var services = Scope.ServiceProvider;
var loggerFactory = services.GetRequiredService<ILoggerFactory>();
var logger = loggerFactory.CreateLogger("app");


try
{
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    await DefaultRoles.SeedRolesAsync(roleManager);
    await DefaultUsers.SeedAdminAsync(userManager);
    await DefaultUsers.SeedEditorAsync(userManager);
    await DefaultUsers.SeedUserAsync(userManager);

    logger.LogInformation("Data Seeded");
    logger.LogInformation("App Started");
}
catch (Exception ex)
{

    logger.LogWarning(ex, "An error occurred while seeding data");
	
}


app.Run();
