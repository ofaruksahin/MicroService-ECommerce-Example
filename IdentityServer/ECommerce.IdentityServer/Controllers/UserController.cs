using ECommerce.IdentityServer.Dtos;
using ECommerce.IdentityServer.Models;
using ECommerce.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace ECommerce.IdentityServer.Controllers
{
    [Authorize(LocalApi.PolicyName)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignupDto dto)
        {
            var applicationUser = new ApplicationUser()
            {
                UserName = dto.UserName,
                Email = dto.Email,
                City = dto.City
            };
           var result =  await _userManager.CreateAsync(applicationUser, dto.Password);
            if (!result.Succeeded)
                return BadRequest(Response<NoContent>.Fail(result.Errors.Select(f => f.Description).ToList(), 400));
            else
                return NoContent();
        }
    }
}
