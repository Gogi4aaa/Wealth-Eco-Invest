using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Wealth_Eco_Invest.Data;
using Wealth_Eco_Invest.Data.Models;
using Wealth_Eco_Invest.Services.Data.Interfaces;
using Wealth_Eco_Invest.Services.Messaging;
using Wealth_Eco_Invest.Web.Infrastructure.Extensions;
using Wealth_Eco_Invest.Web.Infrastructure.ModelBinders;
using static Wealth_Eco_Invest.Common.GeneralApplicationConstants;
var builder = WebApplication.CreateBuilder(args);

StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
	options.SignIn.RequireConfirmedAccount = true;
})
	.AddRoles<IdentityRole<Guid>>()
.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddApplicationServices(typeof(IAnnounceService));
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddControllersWithViews()
	.AddMvcOptions(options =>
	{
		options.ModelBinderProviders.Insert(0,new DecimalModelBinderProvider());
		options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
	});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
	app.UseDeveloperExceptionPage();
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

if (app.Environment.IsDevelopment())
{
    app.SeedAdministrator(DevelopmentAdminEmail);
}

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
