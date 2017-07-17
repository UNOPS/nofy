namespace Nofy.EntityFrameworkCore.Tests
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using Nofy.Core;
	using Nofy.Core.Model;
	using Xunit;

	public class MagicTest
	{

		[Fact]
		public void ManipulateDatabase()
		{
			var rep = new NotificationRepository(
				@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=notification;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

			var service = new NotificationService(rep);
			var n = new Notification("test", "entityType", "entityId", "role", "1", "test",null, new NotificationAction(){ActionLink = "test",Label = "test"});
			service.Publish(n);
		}

		[Fact]
		public void Validate()
		{
			var rep = new NotificationRepository(
				@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=notification;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
			var service = new NotificationService(rep);

			Assert.Throws<ValidationException>(() =>
			{
				var n = new Notification("test", "HASIqStELYkMrXDzIkyhAH4ODJ5AIG1zNWvG3RoJLGB9SKnA9aaCwwanHvmd", "HASIqStELYkMrXDzIkyhAH4ODJ5AIG1zNWvG3RoJLGB9SKnA9aaCwwanHvmd", "role", "1", "test", null, new NotificationAction() { ActionLink = "test", Label = "test" });
				service.Publish(n);
			});
		}
	}
}