using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Configuration.NotifConfiguration
{
    public class NotifConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasOne(s => s.User)
                   .WithMany(s => s.Notifications)
                   .HasForeignKey(s => s.ActorUserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(s => s.Post)
                .WithMany(s => s.Notifications)
                .HasForeignKey(s => s.PostId)
                .OnDelete(deleteBehavior: DeleteBehavior.NoAction)
                 .IsRequired(false);
        }
    }
}
