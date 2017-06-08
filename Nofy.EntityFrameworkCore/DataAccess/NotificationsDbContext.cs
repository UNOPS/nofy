using Microsoft.EntityFrameworkCore;
using Nofy.Core.Model;
using Nofy.EntityFrameworkCore.Mappings;

namespace Nofy.EntityFrameworkCore.DataAccess
{
	public class NotificationsDbContext : DbContext
	{
		public NotificationsDbContext(DbContextOptions options) : base(options)
		{
		}

		public virtual DbSet<NotificationModel> Notifications { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.HasDefaultSchema("ntf");

			builder.AddConfiguration(new NotificationMap());
			builder.AddConfiguration(new NotificationActionsMap());
		}
	}
}