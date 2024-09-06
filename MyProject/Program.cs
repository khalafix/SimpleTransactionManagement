using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyProject.DataModel;
using MyProject.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationContext>(
    options => options.UseSqlServer(connectionString)
    );
builder.Services.AddIdentity<User, Role>(
    options =>
    {
        // عدم نیاز به تایید ایمیل
        options.SignIn.RequireConfirmedEmail = false;
        // عدم نیاز به تایید حساب کاربری
        options.SignIn.RequireConfirmedAccount = false;
        // عدم نیاز به تایید شماره تلفن
        options.SignIn.RequireConfirmedPhoneNumber = false;
        // عدم نیاز به ایمیل یکتا
        options.User.RequireUniqueEmail = false;

        // تعداد حروف یکتای مورد نیاز برای رمز
        options.Password.RequiredUniqueChars = 0;
        // تعداد حروف بزرگ مورد نیاز برای رمز
        options.Password.RequireUppercase = false;
        // تعداد حروف نیاز برای رمز
        options.Password.RequiredLength = 4;
        // تعداد حروف کوچک مورد نیاز برای رمز
        options.Password.RequireLowercase = false;
        // تعداد حروف غیرالفبایی مورد نیاز برای رمز
        options.Password.RequireNonAlphanumeric = false;

    })
    //.AddUserManager<User>()
    //.AddRoleStore<Role>()
    .AddRoleManager<RoleManager<Role>>()
    .AddSignInManager<SignInManager<User>>()
.AddEntityFrameworkStores<ApplicationContext>()
.AddDefaultTokenProviders();
builder.Services.AddScoped<IUserClaimsPrincipalFactory<User>, UserClaimsPrincipalFactory<User, Role>>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
