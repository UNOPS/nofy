namespace Nofy.Core.Model
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class Notification
	{
		public Notification(string description,
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
			this.Actions = actions?.ToList() ?? new List<NotificationAction>();
			this.Status = NotificationStatus.UnRead;
			this.CreatedOn = DateTime.UtcNow;
			this.Summary = summary;
			this.Category = category;
		}

		public Notification()
		{
		}

		public virtual ICollection<NotificationAction> Actions { get; set; }
		public DateTime? ArchivedOn { get; set; }
		public int? Category { get; set; }
		public DateTime CreatedOn { get; set; }
		public string Description { get; set; }
		public string EntityId { get; set; }
		public string EntityType { get; set; }

		public int Id { get; set; }
		public string RecipientId { get; set; }
		public string RecipientType { get; set; }
		public NotificationStatus Status { get; set; }
		public string Summary { get; set; }

		public void AddAction(NotificationAction action)
		{
			this.Actions.Add(action);
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

		public void CopyTo(Notification notification)
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