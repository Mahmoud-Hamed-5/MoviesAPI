using MoviesAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        public IGenericRepository<Genre> Genres { get; }

        Task Save();
    }
}
