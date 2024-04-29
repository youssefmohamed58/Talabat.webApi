using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specification
{
    public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get ; set ; } = new List<Expression<Func<T, object>>> ();
        public Expression<Func<T, object>> OrderByAsec { get ; set; }
        public Expression<Func<T, object>> OrderByDesc { get; set ; }
        public int skip { get ; set ; }
        public int take { get ; set ; }
        public bool IsPagination { get ; set; }

        public BaseSpecification() // Get Includes
        {
            
        }

        public BaseSpecification(Expression<Func<T, bool>> CriteriaExpression) // GetById
        {
            criteria = CriteriaExpression;
            
        }

        public Expression<Func<T, object>>  AddOrderBy (Expression<Func<T, object>> OrderAscExpression)
        {
            return OrderByAsec = OrderAscExpression;
        }
        public Expression<Func<T, object>> AddOrderByDesc(Expression<Func<T, object>> OrderDescExpression)
        {
            return OrderByDesc = OrderDescExpression;
        }

        public void ApplyPagination(int Skip , int Take )
        {
            IsPagination = true;
            skip = Skip;
            take = Take;

        }

    }
}
