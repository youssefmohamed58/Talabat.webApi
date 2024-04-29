using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using Talabat.Core.Entities.Order;

namespace Talabat.Core.Specification.OrderSpecification
{
    public class OrderSpecifications : BaseSpecification<Orders>
    {
        // GetAll Orders
        public OrderSpecifications(string buyeremail) : base(O => O.BuyerEmail == buyeremail)
        {
            Includes.Add(o => o.DelivaryMethod);
            Includes.Add(o => o.Items);
            AddOrderByDesc(o => o.OrderDate);
        }
        // Get Specific Order
        public OrderSpecifications(string buyeremail , int id) : base(O => O.BuyerEmail == buyeremail && O.Id == id)
        {
            Includes.Add(o => o.DelivaryMethod);
            Includes.Add(o => o.Items);
        }
    }
}
