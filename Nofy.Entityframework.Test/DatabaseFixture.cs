namespace Nofy.Entityframework.Test
{
	using System.Configuration;
	using Nofy.EntityFramework;

	public class DatabaseFixture
	{
		private readonly string connetionString;

		public DatabaseFixture()
		{
			this.connetionString = ConfigurationManager.ConnectionStrings["nofy"].ConnectionString;
		}

		public DataContext CreateDataContext()
		{
			return new DataContext(this.connetionString, "dbo");
		}
	}
}