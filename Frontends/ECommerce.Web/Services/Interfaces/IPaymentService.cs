using ECommerce.Web.Models.FakePayment;
using System.Threading.Tasks;

namespace ECommerce.Web.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<bool> ReceivePayment(PaymentInfoInput paymentInfoInput);
    }
}
