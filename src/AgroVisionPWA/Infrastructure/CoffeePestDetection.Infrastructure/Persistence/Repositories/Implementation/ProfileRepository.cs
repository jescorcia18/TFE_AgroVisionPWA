using CoffeePestDetection.Domain.Entities;
using CoffeePestDetection.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeePestDetection.Infrastructure.Persistence.Repositories.Implementation;

public class ProfileRepository: IProfileRepository
{

    private readonly ApplicationDbContext _context;

    public ProfileRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Profile?>GetByEmailAsync(string email)
    {
        return await _context
            .Profiles
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x =>
                x.Email ==
                email);
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _context.Profiles
            .AnyAsync(x => x.Email == email);
    }

    public async Task AddAsync(Profile profile)
    {
        await _context.Profiles.AddAsync( profile);
    }
}
