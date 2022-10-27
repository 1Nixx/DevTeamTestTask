using Web.ViewModels.Client;
using Web.ViewModels.Product;

namespace Web.ViewModels.Order
{
    public class OrderFullViewModel
    {
        public int OrderId { get; set; }
        public OrderTypeViewModel OrderType { get; set; }
        public ClientViewModel Client { get; set; }
        public List<ProductShortViewModel> Products { get; set; }
    }
}
