using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using LogiDriverPortal.Data;
using LogiDriverPortal.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

// Configure MySQL Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Configure Identity
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Configure authentication
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromHours(8);
});

builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline
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
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Dashboard}/{id?}");

// Initialize database with seed data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    var userManager = services.GetRequiredService<UserManager<User>>();
    var logger = services.GetRequiredService<ILogger<Program>>();

    try
    {
        // Ensure database is created
        context.Database.EnsureCreated();

        // Seed existing data
        DbInitializer.Initialize(context);

        // Seed default supervisor user
        var supervisorEmail = "supervisor@g.com";
        var existingUser = await userManager.FindByEmailAsync(supervisorEmail);

        if (existingUser == null)
        {
            var supervisorUser = new User
            {
                UserName = supervisorEmail,
                Email = supervisorEmail,
                FullName = "System Supervisor",
                Role = "Supervisor",
                Status = "Active",
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(supervisorUser, "Pass123");

            if (result.Succeeded)
                logger.LogInformation("✓ Supervisor user created successfully!");
            else
            {
                logger.LogWarning("✗ Failed to create supervisor user:");
                foreach (var error in result.Errors)
                {
                    logger.LogWarning($"  - {error.Description}");
                }
            }
        }
        else
        {
            logger.LogInformation("✓ Supervisor user already exists");
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while initializing the database.");
    }
}

app.Run();
