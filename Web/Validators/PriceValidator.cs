using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Web.Validators
{
	public class PriceValidator : ValidationAttribute
	{
		private const string PriceRegex = "^\\d+(,\\d{1,2})?$";
		public override bool IsValid(object? value)
		{
			if (value is null)
				return false;

			var regex = new Regex(PriceRegex);
			var match = regex.Match(((string)value).Trim());
			return match.Success;
		}
	}
}
