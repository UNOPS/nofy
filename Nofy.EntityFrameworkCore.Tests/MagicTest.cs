namespace Nofy.EntityFrameworkCore.Tests
{
	using Nofy.Core;
	using Nofy.Core.Model;
	using Xunit;

	public class MagicTest
	{
		[Fact]
		public void Works()
		{
			Assert.Equal(1, 1);
		}



		[Fact]
		public void Works2()
		{
			var rep = new NotificationRepository(
				@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=notification;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

			var service = new NotificationService(rep);
			service.Config.BatchLimit = 0;
			var n = new Notification("test", "entityType", "entityId", "role", "1", "test",null, new NotificationAction(){ActionLink = "test",Label = "test"});
			service.Publish(n);
			//var n = rep.GetNotification(0);
			//Assert.True(n.Id == 1);
		}
	}
}