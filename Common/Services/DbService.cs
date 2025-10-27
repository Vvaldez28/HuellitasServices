using Microsoft.EntityFrameworkCore;

namespace Common.Services
{
    public abstract class DbService<Tcontext> : IDisposable
        where Tcontext : DbContext
    {
        protected Tcontext Context { get; }

        protected DbService(Tcontext context)
        {
            Context = context;
        }
        public virtual void Dispose() { }
    }
}
