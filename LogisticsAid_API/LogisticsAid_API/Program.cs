using System.Text;
using System.Text.Json.Serialization;
using LogisticsAid_API.Context;
using LogisticsAid_API.Entities;
using LogisticsAid_API.Entities.Enums;
using LogisticsAid_API.Profiles;
using LogisticsAid_API.Repositories;
using LogisticsAid_API.Repositories.Interfaces;
using LogisticsAid_API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

builder.Services.AddScoped<PasswordService>();
builder.Services.AddScoped<LogisticianService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ContactInfoService>();
builder.Services.AddScoped<TripService>();
builder.Services.AddScoped<ContactInfoService>();
builder.Services.AddScoped<AddressService>();
builder.Services.AddScoped<RoutePointService>();
builder.Services.AddScoped<ICustomerCompanyRepository, CustomerCompanyRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<ICarrierCompanyRepository, CarrierCompanyRepository>();
builder.Services.AddScoped<IDriverRepository, DriverRepository>();
builder.Services.AddScoped<IRoutePointRepository, RoutePointRepository>();
builder.Services.AddScoped<ITransportRepository, TransportRepository>();
builder.Services.AddScoped<ITripRepository, TripRepository>();
builder.Services.AddScoped<ILogisticianRepository, LogisticianRepository>();
builder.Services.AddScoped<IContactInfoRepository, ContactInfoRepository>();
//
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("Policy1", policy =>
    {
        policy.WithOrigins("http://localhost:4200");
        policy.AllowCredentials();
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = config["JwtSettings:Issuer"],
        ValidAudience = config["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(config["JwtSettings:Key"]!)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };

    // Extract token from the cookie
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var token = context.Request.Cookies["auth_token"]; // Read JWT from cookies
            if (!string.IsNullOrEmpty(token))
            {
                context.Token = token;
            }
            return Task.CompletedTask;
        }
    };
}).AddCookie("Cookies", options =>
{
    options.Cookie.Name = "auth_token";
    options.Cookie.SameSite = SameSiteMode.None;
});

builder.Services.AddAuthorization();

builder.Services.AddDbContext<LogisticsAidDbContext>(options =>
{
    options
        .EnableSensitiveDataLogging()
        .UseNpgsql(builder.Configuration.GetConnectionString("DevConnection"),
            o => o
                .MapEnum<EGender>("gender")
                .MapEnum<ERoutePointType>("route_point_type")
                .MapEnum<ETransportType>("transport_type"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}


app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("Policy1");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();