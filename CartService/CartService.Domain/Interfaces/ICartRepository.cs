using CartService.Domain.Entities;

namespace CartService.Domain.Interfaces;

public interface ICartRepository
{
    Task<IEnumerable<Cart>> GetAllAsync();
    Task<Cart?> GetByUserIdAsync(Guid userId);
    Task AddOrUpdateItemAsync(Guid userId, CartItem item);
    Task RemoveItemAsync(Guid userId, Guid productId);
    Task ClearAsync(Guid userId);
    Task SaveChangesAsync();
}
