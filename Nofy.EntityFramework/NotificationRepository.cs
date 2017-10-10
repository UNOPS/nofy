﻿namespace Nofy.EntityFramework
{
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Linq;
	using Nofy.Core;
	using Nofy.Core.Helper;
	using Nofy.Core.Model;
	using Nofy.EntityFramework.DataAccess;

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
			var notification = this.DbContext.Notifications.Find(notificationId);

			if (notification == null)
			{
				throw new ArgumentNullException(nameof(notification));
			}

			if (notification.Archive())
			{
				notification.Status = NotificationStatus.Archived;
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
			return this.DbContext.Notifications.Find(notificationId);
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
			var query = this.DbContext.Notifications.AsQueryable();

			if (!showArchived)
			{
				query = query.Where(t => t.Status != NotificationStatus.Archived);
			}

			query = query.Where(t => recipients.Contains(new NotificationRecipient(t.RecipientType, t.RecipientId)));

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
			var notification = this.DbContext.Notifications.Find(notificationId);

			if (notification == null)
			{
				throw new ArgumentNullException(nameof(notification));
			}

			if (notification.UnArchive())
			{
				notification.Status = NotificationStatus.Read;
				return this.DbContext.SaveChanges();
			}

			return -1;
		}
	}
}