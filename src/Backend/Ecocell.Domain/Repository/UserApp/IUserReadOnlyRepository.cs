namespace Ecocell.Domain.Repository;

public interface IUserReadOnlyRepository
{
    Task<bool> UserExistsWithTheSameEmail(string email);
}