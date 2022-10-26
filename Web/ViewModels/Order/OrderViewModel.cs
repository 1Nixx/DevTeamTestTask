using Web.ViewModels.Product;

namespace Web.ViewModels.Order
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public TypeOrderViewModel OrderType { get; set; }
        public UserViewModel User { get; set; }
        public List<ProductShortViewModel> Products { get; set; }
    }
}
