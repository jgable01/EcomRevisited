using System;

namespace EcomRevisited.ViewModels
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }
        public int NumberOfItems { get; set; }
        public string DestinationCountry { get; set; }
        public double TotalPrice { get; set; }
        public string Address { get; set; }
        public string MailingCode { get; set; }

    }
}
