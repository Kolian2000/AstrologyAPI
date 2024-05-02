using NewWebApi.Models;

namespace NewWebApi.Interface
{
	public interface IPayment
	{
		public Task SuccessOrderStatus(string orderId);

		public void  FailOrderStatus(string orderId);
		public  Task<Result> CheckOrderStatus(string orderId);
		public string CreateOrder(int id);
		public string CreatePayRefers(string orderId);
		public Task<bool> AddPaymenInformation(PaymentRequest paymentRequest);
		
	}
}