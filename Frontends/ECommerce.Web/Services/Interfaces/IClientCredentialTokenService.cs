using System.Threading.Tasks;

namespace ECommerce.Web.Services.Interfaces
{
    public interface IClientCredentialTokenService
    {
        Task<string> GetToken();
    }
}
