namespace Nofy.Core
{
	using System.Collections.Generic;
	using Nofy.Core.Helper;
	using Nofy.Core.Model;

	/// <summary>
	/// Repository interface
	/// </summary>
	public interface INotificationRepository
	{
		/// <summary>
		/// Add list of notifications
		/// </summary>
		/// <param name="notifications"></param>
		/// <returns></returns>
		int AddRange(List<Notification> notifications);

		/// <summary>
		/// Update notification status to be archived 
		/// </summary>
		/// <param name="notificationId"></param>
		/// <returns></returns>
		int Archive(int notificationId);

		/// <summary>
		/// Update notification status to be read
		/// </summary>
		/// <param name="notificationId"></param>
		/// <returns></returns>
		int MarkAsRead(int notificationId);

		/// <summary>
		/// Update the notification status to unread.
		/// </summary>
		/// <param name="notificationId"></param>
		/// <returns></returns>
		int MarkAsUnread(int notificationId);

		/// <summary>
		/// Get notification by Id
		/// </summary>
		/// <param name="notificationId"></param>
		/// <returns></returns>
		Notification GetNotification(int notificationId);

		/// <summary>
		/// Get all notifications for recipients
		/// </summary>
		/// <param name="recipients"></param>
		/// <param name="pageIndex"></param>
		/// <param name="pageSize"></param>
		/// <param name="showArchived"></param>
		/// <returns></returns>
		PaginatedData<Notification> GetNotifications(
			IEnumerable<NotificationRecipient> recipients,
			int pageIndex,
			int pageSize,
			bool showArchived);

		/// <summary>
		/// Undo archive notification
		/// </summary>
		/// <param name="notificationId"></param>
		/// <returns></returns>
		int UnArchive(int notificationId);
	}
}