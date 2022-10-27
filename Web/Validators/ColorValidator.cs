using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Web.Validators
{
	public class ColorValidator : ValidationAttribute
	{
		private const string ColorRegex = "#[0-9A-Fa-f]{6}$";
		public override bool IsValid(object? value)
		{
			if (value is null)
				return false;

			return new RegularExpressionAttribute(ColorRegex).IsValid(value);
		}
	}
}
