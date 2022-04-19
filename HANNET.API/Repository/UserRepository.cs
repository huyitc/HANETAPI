using HANNET.API.Contracts;
using HANNET.API.ViewModel.User;
using HANNET.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HANNET.API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<UserRole> _roleManager;
        private readonly IConfiguration _config;
       public UserRepository(UserManager<User> userManager, SignInManager<User> signInManager,
           RoleManager<UserRole> roleManager,IConfiguration config )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _config = config;
        }
        public async Task<string> Authenticate(LoginModels models)
        {
            var user= await _userManager.FindByNameAsync(models.UserName);
            if(user == null)
            {
                throw new Exception("Can not find username.");
            }
            var result = await _signInManager.PasswordSignInAsync(user, models.Password, models.RememberMe, true);
            if (!result.Succeeded)
            {
                return null;
            }
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.FirstName),
                new Claim(ClaimTypes.Role, string.Join(";",roles)),
                new Claim(ClaimTypes.Name, models.UserName),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token) ;
        }

        public async Task<bool> Register(RegisterModels models)
        {
            var user = await _userManager.FindByNameAsync(models.UserName);
            if (user != null)
            {
                throw new Exception("Tài khoản đã tồn tại");
            }
            if (await _userManager.FindByEmailAsync(models.Email) != null)
            {
                throw new Exception("Email đã tồn tại");
            }
            user = new User()
            {
                FirstName = models.FirstName,
                LastName = models.LastName,
                PlaceId = models.PlaceId,
                UserName = models.UserName,
                Email = models.Email,
                PhoneNumber = models.PhoneNumber,
                DateOfBirth = models.DateOfBirth,
            };
            var result = await _userManager.CreateAsync(user, models.Password);
            if (result.Succeeded)
            {
                return true; 
            }
            return false;
        }
    }
 }

