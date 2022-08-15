using Microsoft.EntityFrameworkCore;
using Petify.WebApi.Data_Model;
using Petify.WebApi;
using Petify.WebApi.Model;
using Microsoft.AspNetCore.Identity;
using Petify.Abstraction;
using Petify.Facade;
using Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Petify.Data.DBModels;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

//Database Connection String Default
//builder.Services.AddDbContext<PetifyDbContext>(options => options.UseSqlServer(
//    builder.Configuration["ConnectionStrings:DefaultConnection"]));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<PetifyDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDbContext<PetifyLiveContext>(options =>
{
    options.UseSqlServer(connectionString, sqlServerOptions =>
    {
        sqlServerOptions.MigrationsAssembly("Petify.Data");
    });
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 5;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
}).AddEntityFrameworkStores<PetifyDbContext>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => options.TokenValidationParameters
    = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"])),
    });

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddScoped<IPetProfile, PetProfile>();
builder.Services.AddScoped<IServiceItem, ServiceItem>();
builder.Services.AddScoped<IAllergiesItem, AllergiesItem>();
builder.Services.AddScoped<IFeedBackItem, FeedbackItem>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<ICheckingService, CheckingService>();
builder.Services.AddScoped<IBillingService, BillingService>();
builder.Services.AddTransient<IEmailService, EmailService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseCors();
    //app.UseHttpLogging();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
