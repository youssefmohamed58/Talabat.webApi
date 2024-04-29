using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specification
{
    public interface ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; }
        public Expression<Func<T, object>> OrderByAsec { get; set; }
        public Expression<Func<T, object>> OrderByDesc { get; set; }

        public int skip { get; set; }

        public int take { get; set; }

        public bool IsPagination { get; set; }




    }
}
