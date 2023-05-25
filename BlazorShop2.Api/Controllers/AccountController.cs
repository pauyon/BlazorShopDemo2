using BlazorShopDemo2.Api.Helper;
using BlazorShopDemo2.Domain.Entities.Authentication;
using BlazorShopDemo2.Domain.Models.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlazorShopDemo2.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApiSettings _apiSettings;

        public AccountController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<ApiSettings> options)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _apiSettings = options.Value;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequestDto signUpRequestDto)
        {
            if (signUpRequestDto == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = new ApplicationUser
            {
                UserName = signUpRequestDto.Email,
                Email = signUpRequestDto.Email,
                Name = signUpRequestDto.Name,
                PhoneNumber = signUpRequestDto.PhoneNumber,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, signUpRequestDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new SignUpResponseDto()
                {
                    IsRegistrationSuccessful = false,
                    Errors = result.Errors.Select(x => x.Description)
                });
            }

            var roleResult = await _userManager.AddToRoleAsync(user, Common.Constants.RoleCustomer);

            if (!roleResult.Succeeded)
            {
                return BadRequest(new SignUpResponseDto()
                {
                    IsRegistrationSuccessful = false,
                    Errors = result.Errors.Select(x => x.Description)
                });
            }

            return StatusCode(201);
        }

        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] SignInRequestDto signInRequestDto)
        {
            if (signInRequestDto == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _signInManager.PasswordSignInAsync(signInRequestDto.UserName, signInRequestDto.Password, false, false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(signInRequestDto.UserName);

                if (user == null)
                {
                    return Unauthorized(new SignInResponseDto
                    {
                        IsAuthSuccessfull = false,
                        ErrorMessage = "Invalid Authentication"
                    });
                }

                //everything is valid and we need to login
                var signinCredentials = GetSigningCredentials();
                var claims = await GetClaims(user);

                var tokenOptions = new JwtSecurityToken(
                        issuer: _apiSettings.ValidIssuer,
                        audience: _apiSettings.ValidAudience,
                        claims: claims,
                        expires: DateTime.Now.AddDays(30),
                        signingCredentials: signinCredentials);

                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return Ok(new SignInResponseDto()
                {
                    IsAuthSuccessfull = true,
                    Token = token,
                    User = new UserDto()
                    {
                        Name = user.Name,
                        Id = user.Id,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber
                    }
                });
            }
            else
            {
                return Unauthorized(new SignInResponseDto
                {
                    IsAuthSuccessfull = false,
                    ErrorMessage = "Invalid Authentication"
                });
            }

            return StatusCode(201);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_apiSettings.SecretKey));

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("Id", user.Id),
            };

            var roles = await _userManager.GetRolesAsync(await _userManager.FindByEmailAsync(user.Email));

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }
    }
}