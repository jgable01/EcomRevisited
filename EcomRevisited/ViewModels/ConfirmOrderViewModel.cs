using System;  
using EcomRevisited.ViewModels;

public class ConfirmOrderViewModel
{
    public Guid CartId { get; set; } 
    public List<OrderItemViewModel> OrderItems { get; set; }
    public double TotalPrice { get; set; }
    public double ConvertedPrice { get; set; }
    public double FinalPrice { get; set; }
    public string DestinationCountry { get; set; }
    public string Address { get; set; }
    public string MailingCode { get; set; }
}