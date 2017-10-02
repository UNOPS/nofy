namespace Nofy.EntityFrameworkCore
{
	using Microsoft.EntityFrameworkCore;
	using Nofy.EntityFrameworkCore.DataAccess;

	/// <summary>
	/// Represents a single unit of work.
	/// </summary>
	public class DataContext
	{
		/// <summary>
		/// Instantiates a new instance of the DataContext class.
		/// </summary>
		public DataContext(DbContextOptions options, string schema = "ntf")
		{
			this.DbContext = new NotificationsDbContext(options, schema);
		}

		// ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
		internal NotificationsDbContext DbContext { get; private set; }

		/// <summary>
		/// Runs <see cref="RelationalDatabaseFacadeExtensions.Migrate"/> underlying <see cref="Microsoft.EntityFrameworkCore.DbContext"/>,
		/// to make sure database exists and all migrations are run.
		/// </summary>
		public void MigrateDatabase()
		{
			this.DbContext.Database.Migrate();
		}
	}
}