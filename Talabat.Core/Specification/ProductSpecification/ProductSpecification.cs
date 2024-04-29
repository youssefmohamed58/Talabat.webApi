using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specification.ProductSpecification
{
    public class ProductSpecification : BaseSpecification<Product>
    {
        //GetAll
        public ProductSpecification(ProductSpecParams Params) :
            base(p => (string.IsNullOrEmpty(Params.Search) || p.Name.ToLower().Contains(Params.Search.ToLower())) && (!Params.BrandId.HasValue || p.ProductBrandId == Params.BrandId) && (!Params.TypeId.HasValue || p.ProductTypeId == Params.TypeId))
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductType);
            if (!string.IsNullOrEmpty(Params.Sort))
            {
                switch (Params.Sort)
                {
                    case "PriceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "PriceDesc":
                        AddOrderByDesc(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
            /*
             products = 100
             PageSize = 10
             PageIndex = 5
             skip(40)
             take(10)   
             */
            ApplyPagination(Params.PageSize * (Params.PageIndex - 1), Params.PageSize);
        }
        //Get By Id
        public ProductSpecification(int id) : base(P => P.Id == id)
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductType);
        }
    }
}
