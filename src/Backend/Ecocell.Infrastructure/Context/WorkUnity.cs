using Ecocell.Domain.Repository.WorkUnity;
namespace Ecocell.Infrastructure.Context;

public sealed class WorkUnity : IDisposable, IWorkUnity
{
    private readonly EcocellContext _context;
    private bool _disposed;

    public WorkUnity(EcocellContext context)
    {
        _context = context;
    }

    public async Task Commit()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose() 
    {
        Dispose(true);
    }

    private void Dispose(bool disposing)
    {
        if(!_disposed && disposing) _context.Dispose();

        _disposed = true;
    }

 }