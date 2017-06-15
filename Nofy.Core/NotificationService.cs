namespace Nofy.Core
{
	using System;
	using System.Collections.Generic;
	using Nofy.Core.Helper;

	/// <summary>
	/// Manage notifications for a repository 
	/// </summary>
	public class NotificationService : IDisposable
	{
		private readonly object lockKey = new object();

		//Temporary list of notification 
		private readonly List<Notification> notifications = new List<Notification>();

		//Notification repository
		private readonly IRepository repository;

		/// <summary>
		/// Initialize new instance of notification service
		/// </summary>
		/// <param name="repository">Service's repository</param>
		public NotificationService(IRepository repository)
		{
			this.repository = repository;
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
			this.repository.Archive(id);
		}

		/// <summary>
		/// Get notification by id
		/// </summary>
		/// <param name="notificationId">Notification id</param>
		/// <returns></returns>
		public Notification GetNotification(int notificationId)
		{
			return this.repository.GetNotification(notificationId);
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
			int pageSize,
			bool showArchived)
		{
			return this.repository.GetNotifications(recipients, pageIndex, pageSize, showArchived);
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
			this.repository.UnArchive(id);
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

				this.repository.AddRange(this.notifications);
				this.notifications.Clear();
			}
		}
	}
}