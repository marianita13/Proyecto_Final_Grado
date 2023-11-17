using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
namespace Application.Repository;
public class PersonRepository : GenericRepository<Person>, IPerson
{
    private readonly GardeningContext _context;

    public PersonRepository(GardeningContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Person> GetByRefreshTokenAsync(string refreshToken)
    {
        return await _context.People
            .Include(u => u.PersonType)
            .Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == refreshToken));
    }

    public async Task<Person> GetByFirstNameAsync(string Personname)
    {
        return await _context.People
            .Include(u => u.PersonType)
            .Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.FirstName.ToLower() == Personname.ToLower());
    }
}