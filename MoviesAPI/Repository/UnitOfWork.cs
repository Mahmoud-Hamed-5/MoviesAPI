using MoviesAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext context;
        private IGenericRepository<Genre> _genres;

        public UnitOfWork(ApplicationDBContext context)
        {
            this.context = context;
        }

        public IGenericRepository<Genre> Genres { get => _genres ??= new GenericRepository<Genre>(context); }

        public void Dispose()
        {
            context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
    }
}
