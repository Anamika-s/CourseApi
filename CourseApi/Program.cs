using CourseApi.Autehntication;
using CourseApi.Models;
using GlobalExceptionHandling.Utility;
//using log4net;
//    using log4net.Config;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text;
using System.Text.Json;
//using Log4NetSample.LogUtility;

    namespace CourseApi
    {
        public class Program
        {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var logger = LogManager.Setup().LoadConfigurationFromAppSettings()
                    .GetCurrentClassLogger();
            logger.Info("Insie Main");
            builder.Services.AddControllers().AddNewtonsoftJson();
            // Add services to the container.
            try
            { 
                builder.Logging.ClearProviders();
                builder.Host.UseNLog();
                // JWT Authentication
                
                builder.Services.AddDbContext<DBContext>();
                builder.Services.AddScoped<JwtAuthenticationManager>();
                builder.Services.AddControllers(options => options.ReturnHttpNotAcceptable = true);

                builder.Services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                }).
    AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

                builder.Services.AddSwaggerGen(options =>
                {
                    options.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme()
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = "Sec"
                    });
                    options.AddSecurityRequirement(securityRequirement: new OpenApiSecurityRequirement
                    {
               {
                   new OpenApiSecurityScheme
                   {
                       Reference=new OpenApiReference{
                       Type=ReferenceType.SecurityScheme,
                       Id="Bearer"

                       }
                   },
                   new String[]{ }
                   }

                    });
                });

                //var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
                //XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
                builder.Services.AddControllers();
                builder.Services.AddSwaggerGen();
                builder.Services.AddControllers(options =>
                {
                    options.InputFormatters.Insert(0, new VcardInputFormatter());
                    options.OutputFormatters.Insert(0, new VcardOutputFormatter());
                });
                //builder.Services.AddControllers()
                //.AddXmlSerializerFormatters();
                //        builder.Services.Configure<JsonOptions>(options =>
                //        {
                //            options.SerializerOptions.IncludeFields = false;
                //        });
                //        var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);

                var app = builder.Build();
                //var _log4net = log4net.LogManager.GetLogger(typeof(Program));
                //_log4net.Info("Hello Logging World");

                // Configure the HTTP request pipeline.
                app.UseAuthentication();
                app.UseAuthorization();
                //app.UseMiddleware(typeof(GlobalErrorHandlingMiddleware));

                app.UseSwagger();
                app.UseSwaggerUI();
                app.MapControllers();

                app.Run();
            }
            
            catch(Exception ex)
            {
                logger.Error(ex.Message);
            }
            finally
            {
                LogManager.Shutdown();            }
            }
        }
    }