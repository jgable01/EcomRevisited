using EcomRevisited.Data;
using EcomRevisited.Models;

namespace EcomRevisited.Services
{
    public class OrderService
    {
        private readonly IRepository<Order> _orderRepository;

        public OrderService(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> GetOrderAsync(Guid orderId)
        {
            return await _orderRepository.GetByIdAsync(orderId);
        }

    }
}
