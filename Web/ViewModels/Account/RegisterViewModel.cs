using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

        [Required]
        public UserType UserRole { get; set; }
    }

    public enum UserType
    {
        [Display(Name = "Админ")]
        Admin = 0,

		[Display(Name = "Курьер")]
		Courier = 1
	}
}
