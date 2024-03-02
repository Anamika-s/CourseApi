using CourseApi.Autehntication;
using CourseApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CourseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        IConfiguration _config;
        DBContext _dbContext;
        JwtAuthenticationManager _jwt;
        public LoginController(IConfiguration config, DBContext context, JwtAuthenticationManager jwt )
        {
            _config = config;
            _dbContext = context;
            _jwt = jwt;
        }
        [HttpPost]
        public IActionResult Login(User User)
        {
            IActionResult response = Unauthorized();
            var user = _jwt.FindUser(User);
            if (user != null)
            {
                var tokenString = _jwt.Authenticate(user);
                response = Ok(new { token = tokenString });
            }
            return response;
        }

        //private string GetToleName(int roleId)
        //{
        //    string roleNAme = (from x in _dbContext.Roles
        //                       where x.RoleId == roleId
        //                       select x.RoleName).FirstOrDefault();
        //    return roleNAme;
        //}

        //private string GenerateJSONWebToken(User user)
        //{
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        //    //Claim[] claimsData = new Claim[]
        //    //{
        //    //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

        //    //};

        //    string RoleNAme = GetToleName(user.RoleId);
        //    IEnumerable<Claim> claims = new Claim[] {
        //        new Claim(JwtRegisteredClaimNames.Jti, new Guid().ToString()),
        //        new Claim(JwtRegisteredClaimNames.Sid, user.UserID.ToString()),
        //        //new Claim("Id", user.Email),
        //            new Claim(ClaimTypes.Name, user.UserID.ToString()),
        //            new Claim(ClaimTypes.Email, user.Email),
        //            new Claim(ClaimTypes.Role, user.RoleId.ToString()),
        //            //new Claim("role", RoleNAme),
        //            new Claim(type:"Date", DateTime.Now.ToString()),
        //            new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(1).ToString("MMM ddd dd yyyy HH:mm:ss tt"))
        //    };

        //    var token = new JwtSecurityToken(_config["Jwt:Issuer"], 
        //     _config["Jwt:Audience"],
        //     claims,
        //     expires: DateTime.Now.AddMinutes(120),
        //     signingCredentials: credentials);


        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}

        //private string GenerateJSONWebToken(User user)
        //{
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


        //    string RoleNAme = GetToleName(user.RoleId);
        //    IEnumerable<Claim> claims = new Claim[] {
        //        new Claim(JwtRegisteredClaimNames.Jti, new Guid().ToString()),
        //        new Claim(JwtRegisteredClaimNames.Sid, user.UserID.ToString()),
        //        //new Claim("Id", user.Email),
        //            new Claim(ClaimTypes.Name, user.UserID.ToString()),
        //            new Claim(ClaimTypes.Email, user.Email),
        //            new Claim(ClaimTypes.Role, RoleNAme),
        //            //new Claim("role", RoleNAme),
        //            new Claim(type:"Date", DateTime.Now.ToString()),
        //            new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(1).ToString("MMM ddd dd yyyy HH:mm:ss tt"))
        //    };

        //    var token = new JwtSecurityToken(_config["Jwt:Issuer"],
        //     _config["Jwt:Audience"],
        //     claims,
        //     expires: DateTime.Now.AddMinutes(120),
        //     signingCredentials: credentials);


        //    return( new JwtSecurityTokenHandler().WriteToken(token));

        //}


        //private TokenModel GenerateJSONWebToken(User user)
        //{
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


        //    string RoleNAme = GetToleName(user.RoleId);
        //    IEnumerable<Claim> claims = new Claim[] {
        //        new Claim(JwtRegisteredClaimNames.Jti, new Guid().ToString()),
        //        new Claim(JwtRegisteredClaimNames.Sid, user.UserID.ToString()),
        //        //new Claim("Id", user.Email),
        //            new Claim(ClaimTypes.Name, user.UserID.ToString()),
        //            new Claim(ClaimTypes.Email, user.Email),
        //            new Claim(ClaimTypes.Role, RoleNAme),
        //            //new Claim("role", RoleNAme),
        //            new Claim(type:"Date", DateTime.Now.ToString()),
        //            new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(1).ToString("MMM ddd dd yyyy HH:mm:ss tt"))
        //    };

        //    var token = new JwtSecurityToken(_config["Jwt:Issuer"],
        //     _config["Jwt:Audience"],
        //     claims,
        //     expires: DateTime.Now.AddMinutes(120),
        //     signingCredentials: credentials);


        //    string tok = new JwtSecurityTokenHandler().WriteToken(token);
        //    string refreshToken =
        //       return new TokenModel() { Token = tok, RefreshToken = refreshToken };
        //    ;

        //}



        





    }


}

