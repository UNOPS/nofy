namespace Nofy.EntityFrameworkCore
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Infrastructure;
	using Microsoft.EntityFrameworkCore.Migrations;
	using Microsoft.Extensions.DependencyInjection;
	using Nofy.Core;
	using Nofy.Core.Helper;
	using Nofy.Core.Model;
	using Nofy.EntityFrameworkCore.DataAccess;

	/// <summary>
	/// Represent notification repository to manipulate notification data.
	/// </summary>
	public class Repository : IRepository
	{
		internal readonly NotificationsDbContext DbContext;

		/// <summary>
		/// Initialize new instance of repository
		/// </summary>
		/// <param name="connectionString"></param>
		public Repository(string connectionString)
		{
			var optionsBuilder = new DbContextOptionsBuilder<NotificationsDbContext>();
			optionsBuilder.UseSqlServer(connectionString);
			this.DbContext = new NotificationsDbContext(optionsBuilder.Options);
		}

		/// <summary>
		/// Add List of notifications
		/// </summary>
		/// <param name="notifications"></param>
		/// <returns></returns>
		public int AddRange(List<Notification> notifications)
		{
			this.DbContext.Notifications.AddRange(notifications.Select(n => new NotificationModel
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

			var ntf = Notification.Load(notification);
			if (ntf.Archive())
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
			var notification = this.DbContext.Notifications.Find(notificationId);
			return notification == null ? null : Notification.Load(notification);
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

			foreach (var recipient in recipients)
			{
				query = query.Where(t => t.RecipientType == recipient.RecipientType && t.RecipientId == recipient.RecipientId);
			}

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

			var ntf = Notification.Load(notification);
			if (ntf.UnArchive())
			{
				notification.Status = NotificationStatus.Read;
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