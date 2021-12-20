using ECommerce.Service.FakePayment.Models;
using ECommerce.Shared.ControllerBases;
using ECommerce.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Service.FakePayment.Controllers
{
    public class FakePaymentController : CustomBaseController
    {
        [HttpPost]
        public IActionResult ReceivePayments(PaymentDto dto)
        {
            return CreateActionResultInstance<NoContent>(Response<NoContent>.Success(200));
        }
    }
}
