using FreeKassa.NET;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NewWebApi.Interface;
using NewWebApi.Models;

namespace NewWebApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class PayController : ControllerBase
	{
		private readonly IPayment _options;

		public PayController(IPayment options)
		{
			_options = options;
		}

		[HttpPost("SuccessNotification")]
		public async Task<IActionResult> SuccessNotification(PaymentRequest request)
		{
			await _options.SuccessOrderStatus(request.MERCHANT_ORDER_ID);
			await _options.AddPaymenInformation(request);
			return Ok();
		}
		[HttpGet]
		public async Task<IActionResult> GetPayLink(string orderId)
		{
			return Ok( _options.CreatePayRefers(orderId));
		}
		[HttpPost("GetOrder")]
		public async Task<IActionResult> GetOrder(int id)
		{
			return Ok(_options.CreateOrder(id));
		}
		[HttpPost("CheckStatus")]
		public async Task<IActionResult> CheckStatus(string orderId)
		{
			return Ok(await _options.CheckOrderStatus(orderId));
		}
		
	}
}