using ECommerce.Shared.Dtos;
using ECommerce.Web.Models;
using IdentityModel.Client;
using System.Threading.Tasks;

namespace ECommerce.Web.Services.Interfaces
{
    public  interface IIdentityService
    {
        Task<Response<bool>> SignIn(SignInInput signInInput);
        Task<TokenResponse> GetAccessTokenByRefreshToken();
        Task RevokeRefreshToken();
    }
}
