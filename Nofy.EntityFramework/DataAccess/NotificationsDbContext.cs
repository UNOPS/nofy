namespace Nofy.EntityFramework6.DataAccess
{
	using System.Data.Entity;
	using Nofy.Core.Model;
	using Nofy.EntityFramework6.Mappings;

	internal class NotificationsDbContext : DbContext
	{
		private const string DefaultConnectionString =
			"Server=(localdb)\\mssqllocaldb;Database=nofy;Trusted_Connection=True;MultipleActiveResultSets=true";

		private readonly string schema;

		public NotificationsDbContext()
			: this(DefaultConnectionString, "dbo")
		{
		}

		public NotificationsDbContext(string connectionString, string schema) : base(connectionString)
		{
			this.schema = schema;
		}

		public virtual DbSet<Notification> Notifications { get; set; }

		protected override void OnModelCreating(DbModelBuilder builder)
		{

			base.OnModelCreating(builder);

			builder.HasDefaultSchema(this.schema);

			builder.Configurations.Add(new NotificationMap(this.schema));
			builder.Configurations.Add(new NotificationActionsMap(this.schema));
		}
	}
}