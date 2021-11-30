using ECommerce.Web.Models;
using ECommerce.Web.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Threading.Tasks;

namespace ECommerce.Web.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Task<UserViewModel> GetUser()
        {
            throw new System.NotImplementedException();
        }
    }
}
