using EcomRevisited.Models;

namespace EcomRevisited.Services.Interfaces
{
    public interface ICartService
    {
        Task<Cart> GetCartAsync(Guid cartId);
        Task CreateCartAsync(Cart cart);
        Task<Cart> GetOrCreateCartAsync(Guid cartId);
        Task<bool> AddProductToCartAsync(Guid cartId, Guid productId, int quantity);
        Task RemoveProductFromCartAsync(Guid cartId, Guid productId);
        Task UpdateProductQuantityAsync(Guid cartId, Guid productId, int newQuantity);
        Task<double> CalculateTotalPriceAsync(Cart cart);
        Task<bool> IncreaseProductQuantityAsync(Guid cartId, Guid productId);
        Task DecreaseProductQuantityAsync(Guid cartId, Guid productId);
    }

}
