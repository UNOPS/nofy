namespace Nofy.EntityFrameworkCore.Tests
{
	using System.ComponentModel.DataAnnotations;
	using Nofy.Core;
	using Nofy.Core.Model;
	using Xunit;

	public class MagicTest
	{
		public MagicTest(DatabaseFixture dbFixture)
		{
			this.fixture = dbFixture;
		}

		private readonly DatabaseFixture fixture;

		[Fact]
		public void ManipulateDatabase()
		{
			var repository = this.fixture.CreateRepository();

			var service = new NotificationService(repository);
			var n = new Notification(
				"test",
				"entityType",
				"entityId",
				"role",
				"1",
				"test",
				null,
				new NotificationAction { ActionLink = "test", Label = "test" });

			service.Publish(n);
		}

		[Fact]
		public void Validate()
		{
			var rep = this.fixture.CreateRepository();
			var service = new NotificationService(rep);

			Assert.Throws<ValidationException>(() =>
			{
				var n = new Notification(
					"test",
					"HASIqStELYkMrXDzIkyhAH4ODJ5AIG1zNWvG3RoJLGB9SKnA9aaCwwanHvmd",
					"HASIqStELYkMrXDzIkyhAH4ODJ5AIG1zNWvG3RoJLGB9SKnA9aaCwwanHvmd",
					"role",
					"1",
					"test",
					null,
					new NotificationAction { ActionLink = "test", Label = "test" });

				service.Publish(n);
			});
		}
	}
}