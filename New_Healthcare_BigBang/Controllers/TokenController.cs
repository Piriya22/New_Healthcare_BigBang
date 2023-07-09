using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using New_Healthcare_BigBang.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace New_Healthcare_BigBang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly HealthcareContext _context;

        private const string DoctorsRole = "Doctors";
        private const string PatientsRole = "Patients";
        private const string AdminRole = "Admin";
        public TokenController(IConfiguration config, HealthcareContext context)
        {
            _configuration = config;
            _context = context;
        }

        [HttpPost("Doctors")]
        public async Task<IActionResult> Post(Doctors _userData)
        {
            if (_userData != null && _userData.Doc_name != null && _userData.Doc_password != null)
            {
                var user = await GetUser(_userData.Doc_name, _userData.Doc_password);

                if (user != null)
                {
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Doctor_Id", user.Doctor_Id.ToString()),
                        new Claim("Doctor_Name", user.Doc_name),
                        new Claim("Password",user.Doc_password),
                        new Claim(ClaimTypes.Role, DoctorsRole)

                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:ValidIssuer"],
                        _configuration["Jwt:ValidAudience"],
                        claims,
                        expires: DateTime.UtcNow.AddDays(1),
                        signingCredentials: signIn);

                    var response = new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        doctor = user.Doctor_Id
                    };

                    return Ok(response);
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private async Task<Doctors> GetUser(string name, string password)
        {
            return await _context.Doctors.FirstOrDefaultAsync(x => x.Doctor_Name == name && x.Doc_password == password);

        }

        [HttpPost("Patients")]
        public async Task<IActionResult> Post(Patients _userData)
        {
            if (_userData != null && _userData.Patient_Name != null && _userData.Password != null)
            {
                var user = await GetUsers(_userData.Patient_Name, _userData.Password);

                if (user != null)
                {
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Patient_Id", user.Patient_Id.ToString()),
                        new Claim("Patient_Name", user.Patient_Name),
                        new Claim("Password",user.Password),
                        new Claim(ClaimTypes.Role, PatientsRole)

                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:ValidIssuer"],
                        _configuration["Jwt:ValidAudience"],
                        claims,
                        expires: DateTime.UtcNow.AddDays(1),
                        signingCredentials: signIn);

                    var response = new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        patient = user.Patient_Id
                    };

                    return Ok(response);
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private async Task<Patients> GetUsers(string name, string password)
        {
            return await _context.Patients.FirstOrDefaultAsync(x => x.Patient_Name == name && x.Password == password);

        }
        [HttpPost("Admin")]
        public async Task<IActionResult> PostStaff(Admin staffData)
        {
            if (staffData != null && !string.IsNullOrEmpty(staffData.Admin_Name) && !string.IsNullOrEmpty(staffData.Admin_Password))
            {
                if (staffData.Admin_Name == "Piriya" && staffData.Admin_Password == "Piriya@123")
                {
                    var claims = new[]
                    {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("AdminId", "1"), // Set the admin ID accordingly
                new Claim("Admin_Name", staffData.Admin_Name),
                new Claim("Admin_Password", staffData.Admin_Password),
                new Claim(ClaimTypes.Role, AdminRole)
            };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:ValidIssuer"],
                        _configuration["Jwt:ValidAudience"],
                        claims,
                        expires: DateTime.UtcNow.AddDays(1),
                        signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }


        private async Task<Admin> GetStaff(string adminName, string adminPassword)
        {
            return await _context.Admins.FirstOrDefaultAsync(s => s.Admin_Name == adminName && s.Admin_Password == adminPassword);
        }
    }

}
