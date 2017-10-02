namespace Nofy.EntityFrameworkCore.Tests
{
	using System.IO;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Configuration;

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

			this.options = new DbContextOptionsBuilder().UseSqlServer(this.config.GetConnectionString("nofy")).Options;
		}

		public DataContext CreateDataContext()
		{
			return new DataContext(this.options);
		}
	}
}