using BlogApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApi.Infrastructure.Configuration.PostConfiguration
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        //delete post it will also delete comment
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(s => s.Id);

            builder.HasMany(s => s.Comments)
                .WithOne(s=> s.Post)
                .HasForeignKey(s => s.PostId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
