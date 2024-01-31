    using log4net;
    using log4net.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
//using Log4NetSample.LogUtility;

    namespace CourseApi
    {
        public class Program
        {
            public static void Main(string[] args)
            {
                var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.

            // JWT Authentication

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
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



            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();
            var app = builder.Build();
            //var _log4net = log4net.LogManager.GetLogger(typeof(Program));
            //_log4net.Info("Hello Logging World");

            // Configure the HTTP request pipeline.
            app.UseAuthentication();
                app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI();
            app.MapControllers();

                app.Run();
            }
        }
    }