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
        switch(user) 
        {
            case LegalPerson legalPerson:
                AddLegalPerson(legalPerson);
                break;
            case NaturalPerson naturalPerson:
                AddNaturalPerson(naturalPerson);
                break;
            default:
                throw new ArgumentException("Invalid user type");
        }        
    }

    private async Task AddNaturalPerson(NaturalPerson naturalPerson)
    {
        await _context.NaturalPerson.AddAsync(naturalPerson);
    }

    private async Task AddLegalPerson(LegalPerson legalPerson)
    {
        await _context.LegalPerson.AddAsync(legalPerson);
    }

    public async Task<bool> UserExistsWithTheSameEmail(string email)
    {
        var userExistsWithTheSameEmail = await _context.Users.AnyAsync(user => user.Email.Equals(email));

        return userExistsWithTheSameEmail;
    }   
}