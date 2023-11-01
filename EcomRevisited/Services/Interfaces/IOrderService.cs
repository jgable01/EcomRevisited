using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EcomRevisited.Models;

namespace EcomRevisited.Services.Interfaces
{
    public interface IOrderService
        {
            Task<Order> GetOrderAsync(Guid orderId);
            Task<Guid> CreateOrderAsync(Guid cartId, string destinationCountry, string mailingCode, string address);
            Task<IEnumerable<Order>> GetAllOrdersAsync();
            Task UpdateTotalPriceAsync(Guid orderId);
            Task ApplyTaxAndConversionAsync(Guid orderId, string destinationCountry);
            Task<bool> ConfirmOrderAsync(ConfirmOrderViewModel model);
        }
}
