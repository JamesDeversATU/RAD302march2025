using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Rad302feWebAPI2025.DataLayer;
using System.Text;
using Tracker.WebAPIClient;

namespace Rad302feWebAPI2025
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ActivityAPIClient.Track(StudentID: "S00236260", StudentName: "James Mccafferty Devers", activityName: "Rad302 fe March 2025", Task: "Creating Secure Web Api");
            //confure the program class to use the applicationdb context and add this missiing lines
            // For CORS on localhost
            string LocalAllowSpecificOrigins = "_localAllowSpecificOrigins";

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddCookie().AddJwtBearer(cfg =>
            {
                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["Tokens:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["Tokens:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Tokens:Key"]))
                };
            });
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Rad302 Final Exam API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization Header using Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    }, new String[] {}
                }
                });
            });
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: LocalAllowSpecificOrigins,
                                  builder =>
                                  {
                                      // current Blazor localhost endpoints See launchSettings.json in Blazor app
                                      builder.WithOrigins("https://localhost:7141")
                                                            .AllowAnyHeader()
                                                            .AllowAnyMethod();
                                  });
            });
            var app = builder.Build();

            

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseRouting();
            app.UseCors(LocalAllowSpecificOrigins);
            app.UseAuthorization();
            app.MapControllers();

            
            app.Run();
        }
    }
}
// cant do it wont work this is how to do it Add-Migration Initial-Schema
