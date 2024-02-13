using System.ComponentModel.DataAnnotations;

namespace NewWebApi.Models.AuthModel
{
	public class User
	{
		public int? Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string PasswordHash { get; set; }
		
		public string? Email { get; set; }
		
		
	}
}