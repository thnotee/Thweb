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

    options.Password.RequireDigit = true;  // 숫자를 필수로 요구
    options.Password.RequireLowercase = false;  // 소문자 사용을 요구하지 않음
    options.Password.RequireUppercase = false;  // 대문자 사용을 요구하지 않음
    options.Password.RequireNonAlphanumeric = false;  // 특수문자 사용을 요구하지 않음
    options.Password.RequiredLength = 4;  // 최소 길이
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
