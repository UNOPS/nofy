namespace Nofy.EntityFrameworkCore.DataAccess
{
	using Microsoft.EntityFrameworkCore;
	using Nofy.Core.Model;
	using Nofy.EntityFrameworkCore.Mappings;

	internal class NotificationsDbContext : DbContext
	{
		private const string DefaultConnectionString =
			"Server=(localdb)\\mssqllocaldb;Database=corenofy;Trusted_Connection=True;MultipleActiveResultSets=true";

		private readonly string schema;

		public NotificationsDbContext()
			: this(new DbContextOptionsBuilder().UseSqlServer(DefaultConnectionString).Options, "ntf")
		{
		}

		public NotificationsDbContext(DbContextOptions options, string schema) : base(options)
		{
			this.schema = schema;
		}

		public virtual DbSet<Notification> Notifications { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.HasDefaultSchema(this.schema);

			builder.AddConfiguration(new NotificationMap(this.schema));
			builder.AddConfiguration(new NotificationActionsMap(this.schema));
		}
	}
}