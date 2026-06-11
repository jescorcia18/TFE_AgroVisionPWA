using CoffeePestDetection.Domain.Entities;


namespace CoffeePestDetection.Infrastructure.Persistence.Repositories.Interfaces;

public interface IProfileRepository
{
    Task<Profile?> GetByEmailAsync(string email);

    Task AddAsync(Profile profile);

    Task<bool> ExistsByEmailAsync(string email);
}
