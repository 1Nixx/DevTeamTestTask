using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Account
{
    public class RegisterViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
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
