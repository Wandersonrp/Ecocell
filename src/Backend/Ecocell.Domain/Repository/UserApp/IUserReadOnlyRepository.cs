namespace Ecocell.Domain.Repository;

public interface IUserReadOnlyRepository
{
    Task<bool> UserExistsWithTheSameEmail(string email);
    Task<bool> UserExistsWithTheSameDocument(string document);
}