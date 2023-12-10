using Ecocell.Domain.Entities.UserApp;
using Ecocell.Domain.Repository;
using Ecocell.Domain.Repository.UserApp;
using Ecocell.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Ecocell.Infrastructure.Repository.UserApp;

public class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository
{
    private readonly EcocellContext _context;

    public UserRepository(EcocellContext context)
    {
        _context = context;
    }

    public async Task Add(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task<bool> UserExistsWithTheSameEmail(string email)
    {
        var userExistsWithTheSameEmail = await _context.Users.AnyAsync(user => user.Email.Equals(email));

        return userExistsWithTheSameEmail;
    }
}