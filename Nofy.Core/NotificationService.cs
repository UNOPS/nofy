namespace Nofy.Core
{
	using System;
	using System.Collections.Generic;
	using Nofy.Core.Helper;
	using Nofy.Core.Model;

	/// <summary>
	/// Manage notifications for a repository 
	/// </summary>
	public class NotificationService : IDisposable
	{
		private readonly object lockKey = new object();

		//Notification repository
		private readonly INotificationRepository notificationRepository;

		//Temporary list of notification 
		private readonly List<Notification> notifications = new List<Notification>();

		/// <summary>
		/// Initialize new instance of notification service
		/// </summary>
		/// <param name="notificationRepository">Service's repository</param>
		public NotificationService(INotificationRepository notificationRepository)
		{
			this.notificationRepository = notificationRepository;
			this.Config = new NotificationServiceConfiguration
			{
				BatchLimit = 0
			};
		}

		//Service configurations
		public NotificationServiceConfiguration Config { get; set; }

		//Make sure to dispose all resources
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Archive notification by id
		/// </summary>
		/// <param name="id">Notification id</param>
		public void Archive(int id)
		{
			this.notificationRepository.Archive(id);
		}

		/// <summary>
		/// Get notification by id
		/// </summary>
		/// <param name="notificationId">Notification id</param>
		/// <returns></returns>
		public Notification GetNotification(int notificationId)
		{
			return this.notificationRepository.GetNotification(notificationId);
		}

		/// <summary>
		/// Get paginated list of notifications for list of recipients 
		/// </summary>
		/// <param name="recipients">List of recipients </param>
		/// <param name="pageIndex">Page index</param>
		/// <param name="pageSize">Page size : maximum number of notification to be returned </param>
		/// <param name="showArchived">Include archived notification in result</param>
		/// <returns></returns>
		public PaginatedData<Notification> GetNotifications(
			IEnumerable<NotificationRecipient> recipients,
			int pageIndex,
			int pageSize = 10,
			bool showArchived = false)
		{
			return this.notificationRepository.GetNotifications(recipients, pageIndex, pageSize, showArchived);
		}

		public void Publish(Notification n)
		{
			lock (this.lockKey)
			{
				this.notifications.Add(n);
			}

			if (this.notifications.Count > this.Config.BatchLimit)
			{
				this.SaveBatch();
			}
		}

		/// <summary>
		/// Unarchive notification by id
		/// </summary>
		/// <param name="id">Notification id</param>
		public void UnArchive(int id)
		{
			this.notificationRepository.UnArchive(id);
		}

		/// <summary>
		/// Dispose resources
		/// </summary>
		/// <param name="disposing"></param>
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.SaveBatch();
			}
		}

		/// <summary>
		/// Bulk save from temporary list to the repository 
		/// </summary>
		private void SaveBatch()
		{
			lock (this.lockKey)
			{
				if (this.notifications.Count == 0)
				{
					return;
				}

				this.notificationRepository.AddRange(this.notifications);
				this.notifications.Clear();
			}
		}
	}
}