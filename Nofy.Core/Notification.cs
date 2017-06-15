namespace Nofy.Core
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Nofy.Core.Model;

	public class Notification
	{
		private readonly ICollection<NotificationAction> actions = new List<NotificationAction>();

		public Notification(
			string description,
			string entityType,
			string entityId,
			string recipientType,
			string recipientId,
			string summary,
			int? category,
			params NotificationAction[] actions)
		{
			this.Description = description;
			this.EntityType = entityType;
			this.EntityId = entityId;
			this.RecipientType = recipientType;
			this.RecipientId = recipientId;
			this.actions = actions?.ToList() ?? new List<NotificationAction>();
			this.Status = NotificationStatus.UnRead;
			this.CreatedOn = DateTime.UtcNow;
			this.Summary = summary;
			this.Category = category;
		}

		private Notification()
		{
		}

		public IEnumerable<NotificationAction> Actions => this.actions;

		public DateTime? ArchivedOn { get; private set; }

		/// <summary>
		/// Get the category to which this notification belongs.
		/// </summary>
		public int? Category { get; private set; }

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
				CreatedOn = notificationModel.CreatedOn,
				Summary = notificationModel.Summary,
				Category = notificationModel.Category
			};

			if (notificationModel.Actions != null)
			{
				foreach (var action in notificationModel.Actions)
				{
					n.actions.Add(NotificationAction.Load(action));
				}
			}

			return n;
		}

		public void AddAction(NotificationAction actionModel)
		{
			this.actions.Add(actionModel);
		}

		public void AddRange(List<Notification> notifications)
		{
		}

		public bool Archive()
		{
			if (this.Status == NotificationStatus.Archived)
			{
				return false;
			}

			this.Status = NotificationStatus.Archived;
			this.ArchivedOn = DateTime.UtcNow;
			return true;
		}

		public void CopyTo(NotificationModel notification)
		{
			if (this.Id != notification.Id)
			{
				throw new Exception("Id of domain and data objects don't match.");
			}

			notification.ArchivedOn = this.ArchivedOn;
			notification.CreatedOn = this.CreatedOn;
			notification.Description = this.Description;
			notification.EntityType = this.EntityType;
			notification.EntityId = this.EntityId;
			notification.RecipientId = this.RecipientId;
			notification.RecipientType = this.RecipientType;
			notification.Status = this.Status;
			notification.Summary = this.Summary;
			notification.Category = this.Category;
		}

		public bool UnArchive()
		{
			if (this.Status != NotificationStatus.Archived)
			{
				return false;
			}

			this.Status = NotificationStatus.Read;
			this.ArchivedOn = null;
			return true;
		}
	}
}