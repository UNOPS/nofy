namespace Nofy.EntityFrameworkCore.Tests
{
	using System.ComponentModel.DataAnnotations;
	using Nofy.Core;
	using Nofy.Core.Model;
	using Xunit;

	[Collection(nameof(DatabaseCollectionFixture))]
	public class MagicTest
	{
		public MagicTest(DatabaseFixture dbFixture)
		{
			this.repository = new NotificationRepository(dbFixture.CreateDataContext());
		}

		private readonly INotificationRepository repository;

		[Fact]
		public void CreateNotification()
		{
			var service = new NotificationService(this.repository);
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
			var service = new NotificationService(this.repository);

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