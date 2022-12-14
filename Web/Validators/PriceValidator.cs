using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Web.Validators
{
	public class PriceValidator : ValidationAttribute
	{
		private const string PriceRegex = "^\\d+([,.]\\d{1,2})?$";
		public override bool IsValid(object? value)
		{
			if (value is null)
				return false;

			return new RegularExpressionAttribute(PriceRegex).IsValid(value);
		}
	}
}
