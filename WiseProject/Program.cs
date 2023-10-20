using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using WiseProject.Business.Abstract;
using WiseProject.Business.Concrete;
using WiseProject.DAL.Abstract;
using WiseProject.Data;
using WiseProject.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();


var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
optionsBuilder.UseSqlServer(connectionString);

ApplicationDbContext dbContext2 = new ApplicationDbContext(optionsBuilder.Options);

//ApplicationDbContext dbContext = new ApplicationDbContext(builder.options);


builder.Services.AddControllersWithViews();

builder.Services.AddDefaultIdentity<ApplicationUser>(
    options =>
    options.SignIn.RequireConfirmedAccount = false
    )
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();


//builder.Services.AddHttpContextAccessor();



builder.Services.AddSingleton<IEnrollmentDal, EnrollmentDal>();
builder.Services.AddSingleton<IEnrollmentService, EnrollmentManager>();

//builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddTransient<ServiceManager, ServiceManager>();
builder.Services.AddTransient<ICurrentUserService, CurrentUserManager>();
builder.Services.AddTransient<IAdminService, AdminManager>();

builder.Services.AddSingleton<IAssignmentDal, AssignmentDal>();
builder.Services.AddSingleton<IAssignmentService, AssignmentManager>();

builder.Services.AddSingleton<ICourseDal, CourseDal>();
builder.Services.AddSingleton<ICourseService, CourseManager>();




var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider;
    try
    {

        UserRoleInit.InitAsync(dbContext).Wait();
    }
    catch (Exception ex)
    {
        var logger = dbContext.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occured while attempting to seed the database");
    }

}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
