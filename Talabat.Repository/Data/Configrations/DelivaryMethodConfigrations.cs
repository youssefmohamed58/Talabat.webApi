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
    internal class DelivaryMethodConfigrations : IEntityTypeConfiguration<DelivaryMethod>
    {
        public void Configure(EntityTypeBuilder<DelivaryMethod> builder)
        {
            builder.Property(DelivaryMethod => DelivaryMethod.Cost)
                .HasColumnType("decimal(18,2)");

        }
    }
}
