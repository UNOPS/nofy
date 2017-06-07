using System;
using System.Collections.Generic;
using Nofy.Core.Helper;

namespace Nofy.Core
{
	/// <summary>
	/// Manage notifications for a repository 
	/// </summary>
	public class NotificationService : IDisposable
	{
		private readonly object _lockKey = new object();

		//Temporary list of notification 
		private readonly List<Notification> _notifications = new List<Notification>();

		//Notification repository
		private readonly IRepository _repository;

		/// <summary>
		/// Initialize new instance of notification service
		/// </summary>
		/// <param name="config">Service's configuration</param>
		/// <param name="repository">Service's repository</param>
		public NotificationService(Configurations config, IRepository repository)
		{
			Config = config;
			_repository = repository;
		}

		//Service configurations
		public Configurations Config { get; }

		//Make sure to dispose all resources
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Archive notification by id
		/// </summary>
		/// <param name="id">Notification id</param>
		public void Archive(int id)
		{
			_repository.Archive(id);
		}

		/// <summary>
		/// Unarchive notification by id
		/// </summary>
		/// <param name="id">Notification id</param>
		public void UnArchive(int id)
		{
			_repository.UnArchive(id);
		}

		/// <summary>
		/// Get notification by id
		/// </summary>
		/// <param name="notificationId">Notification id</param>
		/// <returns></returns>
		public Notification GetNotification(int notificationId)
		{
			return _repository.GetNotification(notificationId);
		}


		/// <summary>
		/// Get paginated list of notifications for list of recipients 
		/// </summary>
		/// <param name="recipients">List of recipients </param>
		/// <param name="pageIndex">Page index</param>
		/// <param name="pageSize">Page size : maximum number of notification to be returned </param>
		/// <param name="showArchived">Include archived notification in result</param>
		/// <returns></returns>
		public PaginatedData<Notification> GetNotifications(IEnumerable<NotificationRecipient> recipients,
			int pageIndex, int pageSize, bool showArchived)
		{
			return _repository.GetNotifications(recipients, pageIndex, pageSize, showArchived);
		}

		public void Publish(Notification n)
		{
			lock (_lockKey)
			{
				_notifications.Add(n);
			}

			if (_notifications.Count > Config.BatchLimit)
				SaveBatch();
		}

		/// <summary>
		/// Dispose resources
		/// </summary>
		/// <param name="disposing"></param>
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
				SaveBatch();
		}

		/// <summary>
		/// Bulk save from temporary list to the repository 
		/// </summary>
		private void SaveBatch()
		{
			lock (_lockKey)
			{
				if (_notifications.Count == 0)
					return;

				_repository.AddRange(_notifications);
				_notifications.Clear();
			}
		}
	}
}