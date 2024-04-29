using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext context;
        private Hashtable _Repositories;

        public UnitOfWork(StoreContext Context)
        {
            context = Context;
            _Repositories = new Hashtable();
        }
        public async Task<int> CompleteAsync()
        {
          return await context.SaveChangesAsync();
        }

        public async  ValueTask DisposeAsync()
        {
            await context.DisposeAsync();
        }

        public IGenericRepository<TEntity> Repositories<TEntity>() where TEntity : BaseEntity
        {
            var type = typeof(TEntity).Name; 
            if(!_Repositories.ContainsKey(type))
            {
                var Repository = new GenericRepository<TEntity>(context);  
                _Repositories.Add(type, Repository);
            }
            return (IGenericRepository<TEntity>)_Repositories[type];
        }
    }
}
