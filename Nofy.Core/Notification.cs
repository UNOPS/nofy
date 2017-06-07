using System;
using System.Collections.Generic;
using System.Linq;
using Nofy.Core.Model;

namespace Nofy.Core
{
	public class Notification
	{
		private readonly ICollection<NotificationAction> actions = new List<NotificationAction>();

		public Notification(string desc, string entityType, string entityId, string recipientType, string recipientId,
			string summary, int? category, params NotificationAction[] actions)
		{
			Description = desc;
			EntityType = entityType;
			EntityId = entityId;
			RecipientType = recipientType;
			RecipientId = recipientId;
			this.actions = actions?.ToList() ?? new List<NotificationAction>();
			Status = NotificationStatus.UnRead;
			CreatedOn = DateTime.UtcNow;
			Summary = summary;
			Category = category;
		}

		private Notification()
		{
		}

		/// <summary>
		/// Get the category to which this notification belongs.
		/// </summary>
		public int? Category { get; private set; }

		public IEnumerable<NotificationAction> Actions => actions;

		public DateTime? ArchivedOn { get; private set; }
		public DateTime CreatedOn { get; private set; }

		/// <summary>
		/// Full notification text.
		/// </summary>
		public string Description { get; private set; }

		public string EntityId { get; private set; }
		public string EntityType { get; private set; }

		public int Id { get; private set; }
		public string RecipientId { get; private set; }
		public string RecipientType { get; private set; }
		public NotificationStatus Status { get; private set; }

		/// <summary>
		/// A very brief description of the notification.
		/// </summary>
		public string Summary { get; private set; }

		public void AddAction(NotificationAction actionModel)
		{
			actions.Add(actionModel);
		}

		public static Notification Load(NotificationModel notificationModel)
		{
			var n = new Notification
			{
				Status = notificationModel.Status,
				ArchivedOn = notificationModel.ArchivedOn,
				Description = notificationModel.Description,
				EntityId = notificationModel.EntityId,
				EntityType = notificationModel.EntityType,
				RecipientId = notificationModel.RecipientId,
				RecipientType = notificationModel.RecipientType,
				Id = notificationModel.Id,
				CreatedOn = notificationModel.DateCreated,
				Summary = notificationModel.Summary,
				Category = notificationModel.Category
			};

			if (notificationModel.Actions != null)
				foreach (var action in notificationModel.Actions)
					n.actions.Add(NotificationAction.Load(action));

			return n;
		}

		public void AddRange(List<Notification> notifications)
		{
		}

		public bool Archive()
		{
			if (Status == NotificationStatus.Archive)
				return false;

			Status = NotificationStatus.Archive;
			ArchivedOn = DateTime.UtcNow;
			return true;
		}

		public void CopyTo(NotificationModel notification)
		{
			if (Id != notification.Id)
				throw new Exception("Id of domain and data objects don't match.");

			notification.ArchivedOn = ArchivedOn;
			notification.DateCreated = CreatedOn;
			notification.Description = Description;
			notification.EntityType = EntityType;
			notification.EntityId = EntityId;
			notification.RecipientId = RecipientId;
			notification.RecipientType = RecipientType;
			notification.Status = Status;
			notification.Summary = Summary;
			notification.Category = Category;
		}

		public bool UnArchive()
		{
			if (Status != NotificationStatus.Archive)
				return false;

			Status = NotificationStatus.Read;
			ArchivedOn = null;
			return true;
		}
	}

	public enum NotificationStatus
	{
		UnRead = 0,
		Read = 1,
		Archive = 2
	}
}