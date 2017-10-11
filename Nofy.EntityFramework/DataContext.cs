namespace Nofy.EntityFramework6
{
	using System.Data.Entity.Infrastructure;
	using System.Data.Entity.Migrations;
	using System.Data.Entity.Migrations.Infrastructure;
	using System.Linq;
	using Nofy.EntityFramework6.DataAccess;

	/// <summary>
	/// Represents a single unit of work.
	/// </summary>
	public class DataContext
	{
		/// <summary>
		/// Instantiates a new instance of the DataContext class.
		/// </summary>
		public DataContext(string connectionString, string schema = "ntf")
		{
			this.DbContext = new NotificationsDbContext(connectionString, schema);
		}

		// ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
		internal NotificationsDbContext DbContext { get; private set; }

		/// <summary>
		/// Runs migrations underlying <see cref="System.Data.Entity.DbContext"/>,
		/// to make sure database exists and all migrations are run.
		/// </summary>
		public void MigrateDatabase()
		{
			if (this.DbContext.Database.Exists() && this.DbContext.Database.CompatibleWithModel(false))
			{
				return;
			}

			var configuration = new DbMigrationsConfiguration();
			var migrator = new DbMigrator(configuration);
			migrator.Configuration.TargetDatabase = new DbConnectionInfo(this.DbContext.Database.Connection.ConnectionString, "System.Data.SqlClient");
			var migrations = migrator.GetPendingMigrations().ToArray();

			if (!migrations.Any())
			{
				return;
			}

			var scriptor = new MigratorScriptingDecorator(migrator);
			var script = scriptor.ScriptUpdate(null, migrations.Last());

			if (!string.IsNullOrEmpty(script))
			{
				this.DbContext.Database.ExecuteSqlCommand(script);
			}
		}
	}
}