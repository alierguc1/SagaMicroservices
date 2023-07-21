using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Order.DataAccess.DataMapper
{
    public class OrdersTypeConfiguration : IEntityTypeConfiguration<Orders>
    {
        public void Configure(EntityTypeBuilder<Orders> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(x => x.Id);
            builder.Property(b => b.Id).HasDefaultValueSql("NEWID()");
            builder.Property(b => b.FailMessage).IsRequired().HasMaxLength(75);
            builder.Property(b => b.IsActive).HasDefaultValue(true);
     
        }
    }
}
