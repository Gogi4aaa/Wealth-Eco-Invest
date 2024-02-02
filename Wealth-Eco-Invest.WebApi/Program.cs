using Microsoft.EntityFrameworkCore;
using Stripe;
using Wealth_Eco_Invest.Data;

var builder = WebApplication.CreateBuilder(args);

StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(setup =>
{
	setup.AddPolicy("Wealth-Eco-Invest", policyBuilder =>
	{
		policyBuilder
			.WithOrigins("https://localhost:7222")
			.AllowAnyHeader()
			.AllowAnyMethod();
	});
});

builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("Wealth-Eco-Invest");

app.UseRouting();

app.MapControllers();

app.Run();