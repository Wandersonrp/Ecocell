using Ecocell.Domain.Repository;
using Moq;

namespace Utilities.Test.Repositories;

public class UserReadOnlyRepositoryBuilder
{
    private static UserReadOnlyRepositoryBuilder _instance;
    private readonly Mock<IUserReadOnlyRepository> _repository;

    private UserReadOnlyRepositoryBuilder()
    {
        if(_repository == null) 
        {
            _repository = new Mock<IUserReadOnlyRepository>();
        }
    }

    public static UserReadOnlyRepositoryBuilder Instance()
    {
        _instance = new UserReadOnlyRepositoryBuilder();
        return _instance;
    }

    public UserReadOnlyRepositoryBuilder UserExistWithSameEmail(string email)
    {
        if(!string.IsNullOrEmpty(email)) 
        {
            _repository.Setup(i => i.UserExistsWithTheSameEmail(email)).ReturnsAsync(true);
        }

        return this;
    }

    public IUserReadOnlyRepository Builder()
    {
        return _repository.Object;
    }
}