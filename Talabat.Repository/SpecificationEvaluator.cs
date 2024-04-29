using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Specification;

namespace Talabat.Repository
{
    public static class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static  IQueryable<T> GetQuery (IQueryable<T> query , ISpecification<T> spec)
        {
            var Query = query;

            if(spec.criteria != null)
            {
               Query = Query.Where (spec.criteria);
            }

            if(spec.OrderByAsec != null)
            { 
            Query = Query.OrderBy(spec.OrderByAsec);
            }

            if (spec.OrderByDesc != null)
            {
                Query = Query.OrderByDescending(spec.OrderByDesc);
            }

            if(spec.IsPagination == true)
            {
                Query = Query.Skip(spec.skip).Take(spec.take);
            }
            Query = spec.Includes.Aggregate(Query, (QueryExpression, IncludeExpression) => QueryExpression.Include(IncludeExpression));

            return Query;

        }
    }
}
