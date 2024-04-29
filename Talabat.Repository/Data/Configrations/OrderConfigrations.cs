using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.Core.Entities.Order;

namespace Talabat.Repository.Data.Configrations
{
    public class OrderConfigrations : IEntityTypeConfiguration<Orders>
    {
        public void Configure(EntityTypeBuilder<Orders> builder)
        {
            builder.Property(O => O.Status)
                   .HasConversion(Ostatus => Ostatus.ToString(), Ostatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), Ostatus));

            builder.OwnsOne(O => O.ShippingAddress, ShippingAddress => ShippingAddress.WithOwner());

            builder.Property(o => o.SubTotal)
                .HasColumnType("decimal(18,2)");

            builder.HasOne(O => O.DelivaryMethod)
                   .WithMany()
                   .OnDelete(DeleteBehavior.NoAction);



        }
    }
}
