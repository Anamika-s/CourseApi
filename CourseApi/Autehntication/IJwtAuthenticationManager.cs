using CourseApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CourseApi.Autehntication
{
    public interface IJwtAuthenticationManager
    {
        public string Authenticate(User user);
    }
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        DBContext _dbContext;
        IConfiguration _config;
        public JwtAuthenticationManager(DBContext context ,IConfiguration configuration)
        {
            _config = configuration;
            _dbContext = context;
        }
        public string Authenticate(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            //Claim[] claimsData = new Claim[]
            //{
            //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

            //};

            string RoleName = GetRoleName(user.RoleId);
            IEnumerable<Claim> claims = new Claim[] {
                    new Claim(JwtRegisteredClaimNames.Jti, new Guid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Sid, user.UserID.ToString()),
                    //new Claim("Id", user.Email),
                        new Claim(ClaimTypes.Name, user.UserID.ToString()),
                        new Claim(ClaimTypes.Email, user.Email),
                        //new Claim(ClaimTypes.Role, user.RoleId.ToString()),
                        new Claim(ClaimTypes.Role, RoleName),
                        new Claim(type:"Date", DateTime.Now.ToString()),
                        new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(1).ToString("MMM ddd dd yyyy HH:mm:ss tt"))
                };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
             _config["Jwt:Audience"],
             claims,
             expires: DateTime.Now.AddMinutes(120),
             signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        private string GetRoleName(int roleId)
        {
            string roleName = (from x in _dbContext.Roles
                               where x.RoleId == roleId
                               select x.RoleName).FirstOrDefault();
            return roleName;
        }

        public User FindUser(User user)
        {
            var user1 = _dbContext.Users.FirstOrDefault(x => x.Email == user.Email && x.Password == user.Password);

            return user1;


        }


    }
}
