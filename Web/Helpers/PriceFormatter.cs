using System.Globalization;

namespace Web.Helpers
{
	public static class PriceFormatter
	{
		public static string Format(decimal price)
		{
			return string.Format("{0:0.00}", price);
		}

		public static decimal ToDecimal(string price)
		{
			return Convert.ToDecimal(price.Replace('.', ','));
		}
	}
}
