using System.ComponentModel.DataAnnotations;

namespace NewWebApi.Models.AuthModel
{
	public class UserDto : User
	{
		
		// [Required]
		// [StringLength(100, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 100 characters")]
		// public string Name { get; set; }
		public string Password { get; set; }
		
	}
}