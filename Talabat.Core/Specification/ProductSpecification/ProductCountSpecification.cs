using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specification.ProductSpecification;

namespace Talabat.Core.Specification
{
    public class ProductCountSpecification : BaseSpecification<Product>
    {
        public ProductCountSpecification(ProductSpecParams Params) :
          base(p => (string.IsNullOrEmpty(Params.Search) || p.Name.ToLower().Contains(Params.Search.ToLower())) && (!Params.BrandId.HasValue || p.ProductBrandId == Params.BrandId) && (!Params.TypeId.HasValue || p.ProductTypeId == Params.TypeId))
        {

        }
    }
}
