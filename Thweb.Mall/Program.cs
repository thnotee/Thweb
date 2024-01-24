using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Thweb.Data.DbContext;
using Thweb.Model.Model;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DbContextConnection") ?? throw new InvalidOperationException("Connection string 'DbContextConnection' not found.");
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<ThwebDbContext>(options => options.UseSqlServer(connectionString));

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<DbContext>();
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ThwebDbContext>().AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options =>
{

    options.Password.RequireDigit = true;  // ���ڸ� �ʼ��� �䱸
    options.Password.RequireLowercase = false;  // �ҹ��� ����� �䱸���� ����
    options.Password.RequireUppercase = false;  // �빮�� ����� �䱸���� ����
    options.Password.RequireNonAlphanumeric = false;  // Ư������ ����� �䱸���� ����
    options.Password.RequiredLength = 4;  // �ּ� ����
    options.Password.RequiredUniqueChars = 0;
    //options.SignIn.RequireConfirmedEmail = true;
    //options => options.SignIn.RequireConfirmedAccount = true
});
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

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
