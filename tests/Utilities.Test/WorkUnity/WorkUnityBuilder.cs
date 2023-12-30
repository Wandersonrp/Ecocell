using Ecocell.Domain.Repository.WorkUnity;
using Moq;

namespace Utilities.Test.WorkUnity;

public class WorkUnityBuilder
{
    private static WorkUnityBuilder _instance;
    private readonly Mock<IWorkUnity> _workUnity;

    private WorkUnityBuilder()
    {
        if(_workUnity == null) 
        {
            _workUnity = new Mock<IWorkUnity>();
        }
    }

    public static WorkUnityBuilder Instance()
    {
        _instance = new WorkUnityBuilder();
        return _instance;
    }

    public IWorkUnity Builder()
    {
        return _workUnity.Object;
    }
}