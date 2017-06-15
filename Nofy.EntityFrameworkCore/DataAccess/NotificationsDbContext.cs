namespace Nofy.EntityFrameworkCore.DataAccess
{
	using Microsoft.EntityFrameworkCore;
	using Nofy.Core.Model;
	using Nofy.EntityFrameworkCore.Mappings;

	public class NotificationsDbContext : DbContext
	{
		public NotificationsDbContext(DbContextOptions options) : base(options)
		{
		}

		public virtual DbSet<Notification> Notifications { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.HasDefaultSchema("ntf");

			builder.AddConfiguration(new NotificationMap());
			builder.AddConfiguration(new NotificationActionsMap());
		}
	}
}