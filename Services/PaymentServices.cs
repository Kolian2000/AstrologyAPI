using FreeKassa.NET;
using NewWebApi.Interface;
using NewWebApi.Models;
using NewWebApi.Models.Enum;
using Npgsql;

namespace NewWebApi.Services
{
	public class PaymentServices : IPayment
	{
		private readonly IFreeKassaService _freeKassaService;
		private readonly IReposi _repository;

		public PaymentServices(IFreeKassaService freeKassaService, IReposi repository)
		{
			_freeKassaService = freeKassaService;
			_repository = repository;
		}
		public string CreatePayRefers(string orderId)
		{	
			return _freeKassaService.GetPayLink(orderId, 10,"RUB");
		}
		public string CreateOrder(int Id)
		{
			var orderID = Guid.NewGuid().ToString();
			using (var command = new NpgsqlCommand("INSERT INTO orders(orderId, status, userId,timeCreate ) VALUES (@orderId, @status, @userId, @timeCreate);"))
			{
				command.Parameters.AddWithValue("@orderId", orderID);
				command.Parameters.AddWithValue("@status", "pending");
				command.Parameters.AddWithValue("@userId", Id);
				command.Parameters.AddWithValue("@timeCreate", DateTime.Now);
				var result = _repository.Request(command, TypeOfComand.Insert);
				
			}
			return orderID;
		}
		public async Task SuccessOrderStatus(string orderId)
		{
			using (var command = new NpgsqlCommand("UPDATE orders SET status = @status WHERE orderId = @orderId;"))
			{
				command.Parameters.AddWithValue("@orderId", orderId);
				command.Parameters.AddWithValue("@status", "success");
				await _repository.Request(command, TypeOfComand.Insert);
			}
		}
		
		public async Task<Result> CheckOrderStatus(string orderId)
		{
			using (var command = new NpgsqlCommand("SELECT status FROM orders WHERE orderId = @orderId;"))
			{
				command.Parameters.AddWithValue("@orderId", orderId);
				var reuslt = await _repository.Request(command, TypeOfComand.Get);
				return reuslt;
			}
		}
		public void FailOrderStatus(string orderId)
		{
			using (var command = new NpgsqlCommand("UPDATE orders SET status = @status WHERE orderId = @orderId;"))
			{
				command.Parameters.AddWithValue("@orderId", orderId);
				command.Parameters.AddWithValue("@status", "fail");
				var result = _repository.Request(command, TypeOfComand.Insert);
			}
		}
		public async Task<bool> AddPaymenInformation(PaymentRequest paymentRequest)
		{
			
			using (var command = new NpgsqlCommand("UPDATE orders SET (amount, intid) VALUES(@amount, @intid) WHERE orderId = @orderId;"))
			{
				
				command.Parameters.AddWithValue("@amount", paymentRequest.AMOUNT);
				command.Parameters.AddWithValue("@intid", paymentRequest.intid);
				var result = await _repository.Request(command, TypeOfComand.Insert);
				return result.IsSuccess;
			}
			
			
		}
		
	}
}