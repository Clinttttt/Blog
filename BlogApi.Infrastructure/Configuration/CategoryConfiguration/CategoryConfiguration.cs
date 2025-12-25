using BlogApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApi.Infrastructure.Configuration.CategoryConfiguration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            // Each Post belongs to one Category
            // Each Category can have many Posts
            // Restrict delete: you cannot delete a Category if there are Posts assigned to it
            builder.HasOne(p => p.Category)
          .WithMany(c => c.Posts)
          .HasForeignKey(p => p.CategoryId)
          .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
