using System.Runtime.InteropServices.Marshalling;
using Ecocell.Domain.Entities.UserApp;

namespace Ecocell.Domain.Repository.UserApp;

public interface IUserWriteOnlyRepository
{
    Task Add(User user);    
}