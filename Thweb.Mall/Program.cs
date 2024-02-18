using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Thweb.Data.DbContext;
using Thweb.Data.Repository;
using Thweb.Data.Repository.IRepository;
using Thweb.Mall.Areas.Identity.Pages.Account.Manage;
using Thweb.Model.Model;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DbContextConnection") ?? throw new InvalidOperationException("Connection string 'DbContextConnection' not found.");
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<ThwebDbContext>(options => options.UseSqlServer(connectionString));

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<DbContext>();
builder.Services.AddIdentity<ThwebUser, IdentityRole>()
    .AddEntityFrameworkStores<ThwebDbContext>().AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequireDigit = true;  // ���ڸ� �ʼ��� �䱸
    options.Password.RequireLowercase = false;  // �ҹ��� ����� �䱸���� ����
    options.Password.RequireUppercase = false;  // �빮�� ����� �䱸���� ����
    options.Password.RequireNonAlphanumeric = false;  // Ư������ ����� �䱸���� ����
    options.Password.RequiredLength = 4;  // �ּ� ����
    options.Password.RequiredUniqueChars = 0;
    //options.SignIn.RequireConfirmedEmail = true;
    //options => options.SignIn.RequireConfirmedAccount = true
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddDistributedMemoryCache();

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
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "node_modules")),
    RequestPath = "/node_modules"
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
