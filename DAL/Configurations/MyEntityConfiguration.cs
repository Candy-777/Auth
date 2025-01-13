using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations
{
    public class MyEntityConfiguration : IEntityTypeConfiguration<MyEntity>
    {
        public void Configure(EntityTypeBuilder<MyEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(250);           
            builder.HasIndex(x => new { x.UserId, x.Name }).IsUnique(); // Композитный уникальный индекс

            builder.HasOne(x=> x.User)
                .WithMany(u=> u.MyEntities)
                .HasForeignKey(x=> x.UserId)
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
