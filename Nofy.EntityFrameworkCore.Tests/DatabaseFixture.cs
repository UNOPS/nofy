namespace Nofy.EntityFrameworkCore.Tests
{
	using System.IO;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Configuration;
	using Nofy.EntityFrameworkCore.DataAccess;

	public class DatabaseFixture
	{
		private readonly IConfigurationRoot config;
		private readonly DbContextOptions options;

		public DatabaseFixture()
		{
			this.config = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();

			this.options = new DbContextOptionsBuilder().UseSqlServer(this.ConnectionString).Options;
		}

		private string ConnectionString => this.config.GetConnectionString("tag");

		public NotificationsDbContext CreateDataContext()
		{
			return new NotificationsDbContext(this.options);
		}

		public NotificationRepository CreateRepository()
		{
			return new NotificationRepository(this.ConnectionString);
		}
	}
}