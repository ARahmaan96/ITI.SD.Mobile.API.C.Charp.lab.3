
using Lab3.Context;
using Lab3.Models;
using Lab3.Services.AccountServices;
using Lab3.Services.CourseServies;
using Lab3.Services.DepartmentService;
using Lab3.Services.StudentService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Lab3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Register Company Context
            builder.Services.AddDbContext<CompanyContext>(option =>
            {
                option.UseSqlServer(builder.Configuration["ConnectionStrings:conn"]);
            });

            // Register Models Servises
            builder.Services.AddScoped<IAccountServices, AccountServices>();
            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<ICourseServies, CourseServies>();

            // Register User Manager
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<CompanyContext>();

            // Ririster CORS Policy
            builder.Services.AddCors(CorsPolicy =>
            {
                CorsPolicy.AddPolicy("Development", (CorsOption) =>
                {
                    CorsOption.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });

                CorsPolicy.AddPolicy("Production", (CorsOption) =>
                {
                    CorsOption.WithOrigins("192.25.10.4").AllowAnyHeader().AllowAnyMethod();
                });
            });

            // Configer UseAuthentication Middelware For accepting token from header
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:ValidateIssure"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:ValidateAud"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecurityKey"]!))
                };
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();

            // Add CORS MW
            app.UseCors("Development");

            app.MapControllers();

            app.Run();
        }
    }
}
