using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order;

namespace Talabat.Core.Specification.OrderSpecification
{
    public class OrderWithPaymentIntentSpec : BaseSpecification<Orders>
    {
        public OrderWithPaymentIntentSpec(string PaymentIntentid):base(O => O.PaymentIntentId == PaymentIntentid)
        {
            
        }
    }
}
