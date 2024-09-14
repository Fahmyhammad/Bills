using bill_DataAccess.Data;
using bill_DataAccess.Implementation;
using bill_Entities.Repoistory;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using bill_Entities.Models;
using bill_DataAccess.Seed;
using bill_Entities.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnBills")));

builder.Services.AddIdentity<AppUser,IdentityRole>().AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultUI().AddDefaultTokenProviders();


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICompanyRepoistory, CompanyRepoistory>();
builder.Services.AddScoped<ITypeRepoistory, TypeRepoistory>();
builder.Services.AddScoped<IItemRepoistory, ItemRepoistory>();
builder.Services.AddScoped<IClientRepoistory, ClientRepoistory>();
builder.Services.AddScoped<ISalesInvoiceRepoistory, SalesInvoiceRepoistroy>();
builder.Services.AddScoped<IUnitssRepoistory, UnitssRepoistory>();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAutoMapper(typeof(MappingProfile));


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
