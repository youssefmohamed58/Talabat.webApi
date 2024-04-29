using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specification;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _dbcontext;

        public GenericRepository(StoreContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            //if(typeof(T) == typeof(Product))
            //{
            //    return (IReadOnlyList<T>) _dbcontext.Set<Product>() .Include(p => p.ProductBrand).Include(p => p.ProductType).ToList(); 
            
            //}
            return await _dbcontext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbcontext.Set<T>().FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
             await _dbcontext.AddAsync(entity); 
        }

        // Apply Specification
        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecifiction(spec).ToListAsync();
        }

        public async Task<T> GetByIdWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecifiction(spec).FirstOrDefaultAsync();
        }

        public IQueryable<T> ApplySpecifiction(ISpecification<T> spec)
        {
           return SpecificationEvaluator<T>.GetQuery(_dbcontext.Set<T>(),spec);
        }

        public async Task<int> GetCountwithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecifiction(spec).CountAsync();
        }

        public void Delete(T item)
        {
            _dbcontext.Set<T>().Remove(item);    
        }

        public void Update(T item)
        {
            _dbcontext.Set<T>().Update(item);    
        }
    }
}
