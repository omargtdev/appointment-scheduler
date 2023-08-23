using System.Text;
using AppointmentScheduler.Database;
using AppointmentScheduler.Repository.Users;
using AppointmentScheduler.Service.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppointmentSchedulerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppointmentScheduler"))
);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true, // Validates the issuer (transmitter)
            ValidateAudience = true, // Validates where is coming from
            ValidateLifetime = true, // Validates the token's expiration time
            ValidateIssuerSigningKey = true, // Validates the token's signing key

            ValidIssuer = builder.Configuration["Jwt:Issuer"], // Issuer (transmitter)
            ValidAudience = builder.Configuration["Jwt:Audience"], // Audience (receiving)
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SigningKey"]))
        };
    });


// TODO: Create an external file for inject repositories
builder.Services.AddTransient<IUserRepository, UserRepository>();

// TODO: Create an external file for inject services
builder.Services.AddTransient<IUserService, UserService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // Using Jwt service authentication

app.UseAuthorization();

app.MapControllers();

app.Run();
