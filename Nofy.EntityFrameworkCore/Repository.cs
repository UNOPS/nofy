using System;
using System.Collections.Generic;
using System.Linq;
using Nofy.EntityFrameworkCore.DataAccess;
using Nofy.Core.Helper;
using Nofy.Core;
using Microsoft.EntityFrameworkCore;
using Nofy.Core.Model;

namespace Nofy.EntityFrameworkCore
{
	public class Repository : IRepository
	{
		internal readonly NotificationsDbContext DbContext;

		public Repository(string connectionString)
		{
			var optionsBuilder = new DbContextOptionsBuilder<NotificationsDbContext>();
			optionsBuilder.UseSqlServer(connectionString);
			DbContext = new NotificationsDbContext(optionsBuilder.Options);
		}

		public int AddRange(List<Notification> notifications)
		{
			DbContext.Notifications.AddRange(notifications.Select(n => new NotificationModel
			{
				Status = n.Status,
				DateCreated = n.CreatedOn,
				Description = n.Description,
				EntityId = n.EntityId,
				EntityType = n.EntityType,
				RecipientId = n.RecipientId,
				RecipientType = n.RecipientType,
				Summary = n.Summary,
				Category = n.Category,
				Actions = n.Actions.Select(a => new NotificationActionModel
				{
					Label = a.Label,
					ActionLink = a.Link
				}).ToList()
			}));
			return DbContext.SaveChanges();
		}


		public int Archive(int notificationId)
		{
			var notification = DbContext.Notifications.Find(notificationId);

			if (notification == null)
				throw new ArgumentNullException(nameof(notification));
			var ntf = Notification.Load(notification);
			if (ntf.Archive())
			{
				notification.Status = NotificationStatus.Archive;
				return DbContext.SaveChanges();
			}
			return -1;
		}

		public Notification GetNotification(int notificationId)
		{
			var notification = DbContext.Notifications.Find(notificationId);
			return notification == null ? null : Notification.Load(notification);
		}

		public PaginatedData<Notification> GetNotifications(IEnumerable<NotificationRecipient> recipients,
			int pageIndex, int pageSize, bool showArchived)
		{
			var query = DbContext.Notifications.AsQueryable();

			if (!showArchived)
				query = query.Where(t => t.Status != NotificationStatus.Archive);

			query = query.Where(t => recipients.Contains(new NotificationRecipient(t.RecipientType, t.RecipientId)));

			foreach (var recipient in recipients)
				query = query.Where(t => t.RecipientType == recipient.RecipientType && t.RecipientId == recipient.RecipientId);

			var data = query
				.Include(t => t.Actions)
				.OrderByDescending(n => n.Id)
				.Paginate(pageIndex, pageSize);

			return new PaginatedData<Notification>
			{
				Results = data.Results.Select(Notification.Load),
				TotalCount = data.TotalCount
			};
		}

		public int UnArchive(int notificationId)
		{
			var notification = DbContext.Notifications.Find(notificationId);

			if (notification == null)
				throw new ArgumentNullException(nameof(notification));
			var ntf = Notification.Load(notification);
			if (ntf.UnArchive())
			{
				notification.Status = NotificationStatus.Read;
				return DbContext.SaveChanges();
			}
			return -1;
		}
	}
}