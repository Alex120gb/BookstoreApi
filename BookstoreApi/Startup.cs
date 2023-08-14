using AutoMapper;
using BookstoreApi.Common;
using BookstoreApi.MappingProfiles;
using BookstoreApi.Repositories;
using BookstoreApi.Repositories.Interface;
using BookstoreApi.Services;
using BookstoreApi.Services.Interfaces;
using BookstoreApi.TableDbContext;
using BookstoreApi.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog.Extensions.Logging;
using System.Text;

namespace BookstoreApi
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();
            services.AddHttpClient();
            services.AddControllers();
            services.AddHttpContextAccessor();
            services.AddEndpointsApiExplorer();

            services.AddSingleton<JwtTokenGenerator>();
            services.AddScoped<IJwtTokenGeneratorService, JwtTokenGeneratorService>();
            services.AddScoped<IBooksService, BooksService>();
            services.AddScoped<IUserService, UsersService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IBookRepositories, BookRepositories>();
            services.AddScoped<IUserRepositories, UserRepositories>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAuthUserRepositories, AuthUserRepositories>();

            // Use the below connection string when running the api locally without docker - BUT first do run the compose file to create a database for you which you can use
            // (MIGHT NEED TO CHANGE FROM 127.0.0.1 SERVER NAME TO localhost)
            // Server=127.0.0.1,1433;Database=Bookstore;User=sa;Password=B@@k2toR3S3rVer;TrustServerCertificate=true;
            var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            var dbName = Environment.GetEnvironmentVariable("DB_NAME");
            var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
            var connectionString = $"Server={dbHost},1433;Database={dbName};User=sa;Password={dbPassword};TrustServerCertificate=true;";
            services.AddDbContext<BookDbContext>(options => options.UseSqlServer(connectionString));
            services.AddDbContext<UserDbContext>(options => options.UseSqlServer(connectionString));

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            var jwtSecretKey = _configuration.GetSection("JwtSettings").Get<JwtSettingsModel>();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey.SecretKey));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSecretKey.Issuer, 
                        ValidAudience = jwtSecretKey.Audience, 
                        IssuerSigningKey = key
                    };
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookstoreApi", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });

                c.OperationFilter<AddJwtAuthorizationHeaderParameter>();
            });

            services.AddLogging(logging =>
            {
                logging.ClearProviders();
                logging.SetMinimumLevel(LogLevel.Trace); 
                logging.AddNLog(_configuration);
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication(); 
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}