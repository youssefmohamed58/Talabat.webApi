using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order
{
    public class DelivaryMethod : BaseEntity
    {
        public DelivaryMethod()
        {
        }

        public DelivaryMethod(string shortName, string description, string delievaryTime, decimal cost)
        {
            ShortName = shortName;
            Description = description;
            DelievaryTime = delievaryTime;
            Cost = cost;
        }

        public string ShortName { get; set; } 
       public string Description { get; set; }
       public string DelievaryTime {  get; set; }

       public decimal Cost { get; set; }   






    }
}
