using Ambev.DeveloperEvaluation.Domain.Carts;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class CartRepository(DefaultContext context) : ICartRepository
{
    public async Task<Cart?> GetByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken = default)
    {
        return await context.Carts
            .Include(c => c.Items)
                .ThenInclude(ci => ci.Product)
            .Include(c => c.User)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId, cancellationToken);
    }

    public async Task<Cart> CreateAsync(Cart cart, CancellationToken cancellationToken = default)
    {
        await context.Carts.AddAsync(cart, cancellationToken);
        await context.SaveChangesAsync(cancellationToken); // TODO: colocar na unit of work
        return cart;
    }
}
