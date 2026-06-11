
namespace CoffeePestDetection.Infrastructure.Persistence;

public interface IApplicationDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
