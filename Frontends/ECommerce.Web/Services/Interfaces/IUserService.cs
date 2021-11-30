using ECommerce.Web.Models;
using System.Threading.Tasks;

namespace ECommerce.Web.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel> GetUser();
    }
}
