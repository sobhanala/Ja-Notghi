using API.Converters;
using Application.Services;
using DotNetEnv;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using API.Extensions;
using API.TokenService;
using Application.interfaces.ApplicationServices;
using Application.interfaces.ExternalServices;
using Application.interfaces.Repositories;
using Infrastructure.EmailService;
using API.Controllers.Service;

var builder = WebApplication.CreateBuilder(args);

Env.Load();
builder.Configuration.AddEnvironmentVariables();

//configs 
builder.Services.ConfigureSmtpSettings(builder.Configuration);
builder.Services.ConfigureJwtSettings(builder.Configuration);

//services
builder.Services.AddCustomCors();
builder.Services.AddCustomIdentity();
builder.Services.AddCustomAuthentication();
builder.Services.AddCustomDbContext();
builder.Services.AddCustomSwagger();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
});
builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromHours(1);
});


//Dependency Inversions
builder.Services.AddTransient<IEmailService, SmtpEmailService>();
builder.Services.AddScoped<UserControllerService>();
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<TokenGenerator>();
builder.Services.AddScoped<IFollowRepository, FollowRepository>();
builder.Services.AddScoped<IFollowService, FollowService>();
builder.Services.AddScoped<FollowControllerService>();


//app configs
var app = builder.Build();

app.UseCustomMiddlewares();

app.MapControllers();

app.Run();