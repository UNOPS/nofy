namespace Nofy.Entityframework.Test
{
	using System.ComponentModel.DataAnnotations;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using Nofy.Core;
	using Nofy.Core.Helper;
	using Nofy.Core.Model;
	using Nofy.EntityFramework;
	using TestStack.BDDfy;

	[TestClass]
	public class MagicTest
	{
		[TestMethod]
		public void PublishNotification()
		{
			new Scenario()
				.Given(a => a.CreateNotification())
				.BDDfy("Notification is created");
		}

		[TestMethod]
		public void ValidateNotification()
		{
			new Scenario()
				.Given(a => a.Validate())
				.BDDfy("The field EntityId must be a string or array type with a maximum length of '50'");
		}

		[TestMethod]
		public void LoadData()
		{
			new Scenario()
				.Given(a => a.LoaData())
				.BDDfy();
		}
	}

	internal class Scenario
	{
		private readonly INotificationRepository repository;

		public Scenario() : this(new DatabaseFixture())
		{
		}

		public Scenario(DatabaseFixture dbFixture)
		{
			this.repository = new NotificationRepository(dbFixture.CreateDataContext());
		}

		public void CreateNotification()
		{
			var service = new NotificationService(this.repository);
			var notification = new Notification("test", "entityType", "entityId", "role", "1", "test", null,
				new NotificationAction { ActionLink = "test", Label = "test" });

			service.Publish(notification);
		}

		public void Validate()
		{
			var service = new NotificationService(this.repository);

			Assert.ThrowsException<ValidationException>(() =>
			{
				var notification = new Notification(
					"test",
					"HASIqStELYkMrXDzIkyhAH4ODJ5AIG1zNWvG3RoJLGB9SKnA9aaCwwanHvmd",
					"HASIqStELYkMrXDzIkyhAH4ODJ5AIG1zNWvG3RoJLGB9SKnA9aaCwwanHvmd",
					"role",
					"1",
					"test",
					null,
					new NotificationAction { ActionLink = "test", Label = "test" });

				service.Publish(notification);
			});
		}

		public PaginatedData<Notification> LoaData()
		{
			var service = new NotificationService(this.repository);

			var recepients = new[]
			{
				new NotificationRecipient("role", "1")
			};

			return service.GetNotifications(recepients, 1, 10, true);
		}
	}
}