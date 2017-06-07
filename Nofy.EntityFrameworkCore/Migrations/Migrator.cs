using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;

namespace Nofy.EntityFrameworkCore.Migrations
{
	public class Migrator
	{
		public static void Migrate(Repository repository)
		{
			var migrator = repository.DbContext.GetInfrastructure().GetRequiredService<IMigrator>();
			migrator.Migrate("init");
		}
	}
}