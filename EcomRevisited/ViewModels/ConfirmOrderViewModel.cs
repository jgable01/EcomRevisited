using System.Collections.Generic;

namespace EcomRevisited.ViewModels
{
    public class ConfirmOrderViewModel
    {
        public List<OrderItemViewModel> OrderItems { get; set; }
        public double TotalPrice { get; set; }
        public double ConvertedPrice { get; set; }
        public double FinalPrice { get; set; }
        public string DestinationCountry { get; set; }
    }
}
