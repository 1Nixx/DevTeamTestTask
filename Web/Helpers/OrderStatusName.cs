using Web.ViewModels.Order;

namespace Web.Helpers
{
	public class OrderStatusName
	{
		public readonly static Dictionary<int, string> StatusNames = new()
		{
			{(int) OrderTypeViewModel.Active, "Активные"},
			{(int) OrderTypeViewModel.Completed, "Выполненные"}
		};
	}
}
