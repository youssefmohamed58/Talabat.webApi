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
    internal class OrderItemsConfigrations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(OI => OI.price)
                    .HasColumnType("decimal(18,2)");

            builder.OwnsOne(OI => OI.product, product => product.WithOwner());

        }
    }
}
