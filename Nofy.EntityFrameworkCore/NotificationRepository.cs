namespace Nofy.EntityFrameworkCore
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Microsoft.EntityFrameworkCore;
	using Nofy.Core;
	using Nofy.Core.Helper;
	using Nofy.Core.Model;
	using Nofy.EntityFrameworkCore.DataAccess;

	/// <summary>
	/// Represent notification repository to manipulate notification data.
	/// </summary>
	public class NotificationRepository : INotificationRepository
	{
		internal readonly NotificationsDbContext DbContext;

		/// <summary>
		/// Initialize new instance of repository
		/// </summary>
		/// <param name="context"></param>
		public NotificationRepository(DataContext context)
		{
			this.DbContext = context.DbContext;
		}

		/// <summary>
		/// Add List of notifications
		/// </summary>
		/// <param name="notifications"></param>
		/// <returns></returns>
		public int AddRange(List<Notification> notifications)
		{
			this.DbContext.Notifications.AddRange(notifications.Select(n => new Notification(n.Description, n.EntityType, n.EntityId, n.RecipientType,
				n.RecipientId, n.Summary, n.Category, n.Actions.Select(a => a).ToArray())));
			return this.DbContext.SaveChanges();
		}

		/// <summary>
		/// Update notification status to be archived
		/// </summary>
		/// <param name="notificationId"></param>
		/// <returns></returns>
		public int Archive(int notificationId)
		{
			var notification = this.GetNotification(notificationId);

			if (notification == null)
			{
				throw new ArgumentNullException(nameof(notification));
			}

			if (notification.Archive())
			{
				return this.DbContext.SaveChanges();
			}

			return -1;
		}

		/// <summary>
		/// Update notification status to be read
		/// </summary>
		/// <param name="notificationId"></param>
		/// <returns></returns>
		public int MarkAsRead(int notificationId)
		{
			var notification = this.GetNotification(notificationId);

			if (notification.MarkAsRead())
			{
				return this.DbContext.SaveChanges();
			}

			return -1;
		}

		/// <summary>
		/// Update notification status to be unread
		/// </summary>
		/// <param name="notificationId"></param>
		/// <returns></returns>
		public int MarkAsUnread(int notificationId)
		{
			var notification = this.GetNotification(notificationId);

			if (notification.MarkAsUnread())
			{
				return this.DbContext.SaveChanges();
			}

			return -1;
		}

		/// <summary>
		/// Get notification by Id
		/// </summary>
		/// <param name="notificationId"></param>
		/// <returns></returns>
		public Notification GetNotification(int notificationId)
		{
			var notification = this.DbContext.Notifications.Find(notificationId);
			if (notification == null)
			{
				throw new ArgumentNullException(nameof(notification));
			}
			return notification;
		}

		/// <summary>
		/// Get all notifications for recipients
		/// </summary>
		/// <param name="recipients"></param>
		/// <param name="pageIndex"></param>
		/// <param name="pageSize"></param>
		/// <param name="showArchived"></param>
		/// <returns></returns>
		public PaginatedData<Notification> GetNotifications(
			IEnumerable<NotificationRecipient> recipients,
			int pageIndex,
			int pageSize,
			bool showArchived)
		{
			var query = this.DbContext.Notifications
				.BelongingTo(recipients.ToArray());

			if (!showArchived)
			{
				query = query.Where(t => !t.Archived);
			}

			var data = query
				.Include(t => t.Actions)
				.OrderByDescending(n => n.Id)
				.Paginate(pageIndex, pageSize);

			return new PaginatedData<Notification>
			{
				Results = data.Results,
				TotalCount = data.TotalCount
			};
		}

		/// <summary>
		/// Undo archive notification
		/// </summary>
		/// <param name="notificationId"></param>
		/// <returns></returns>
		public int UnArchive(int notificationId)
		{
			var notification = this.GetNotification(notificationId);

			if (notification == null)
			{
				throw new ArgumentNullException(nameof(notification));
			}

			if (notification.UnArchive())
			{
				return this.DbContext.SaveChanges();
			}

			return -1;
		}

		/// <summary>
		/// Run initialization migration.
		/// </summary>
		public void InitializeMigration()
		{
			this.DbContext.Database.Migrate();
		}
	}
}