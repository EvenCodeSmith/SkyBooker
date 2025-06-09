using FlightService.Models;
using FlightService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Serilog;


namespace FlightService
{
    public class Program
    {
        public static void Main(string[] args)
        {

            Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.UseSerilog();


            builder.Services.Configure<FlightDatabaseSettings>(
                builder.Configuration.GetSection("FlightDatabaseSettings"));

            builder.Services.AddSingleton<FlightDataService>();

            // Add JWT Authentication
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "SkyBookerAuth",
                        ValidAudience = "SkyBookerUsers",
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.ASCII.GetBytes("LeslieSucksAtEldenRingNightReign69420SuperSigma"))
                    };
                });

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
