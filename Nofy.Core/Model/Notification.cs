namespace Nofy.Core.Model
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
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

			var context = new ValidationContext(this, serviceProvider: null, items: null);
			var results = new List<ValidationResult>();

			var isValid = Validator.TryValidateObject(this, context, results, true);
			if (!isValid)
			{
				foreach (var validationResult in results)
				{
					throw new ValidationException(validationResult.ErrorMessage);
				}
			}
		}

		public Notification()
		{
		}

		public virtual ICollection<NotificationAction> Actions { get; set; }
		public DateTime? ArchivedOn { get; set; }
		public int? Category { get; set; }
		public DateTime CreatedOn { get; set; }

		[MaxLength(NotificationValidation.MaxDescriptionLength)]
		public string Description { get; set; }

		[Required]
		[MaxLength(NotificationValidation.MaxEntityIdLength)]
		public string EntityId { get; set; }

		[MaxLength(NotificationValidation.MaxEntityTypeLength)]
		public string EntityType { get; set; }

		public int Id { get; set; }

		[MaxLength(NotificationValidation.MaxRecipientIdLength)]
		public string RecipientId { get; set; }

		[MaxLength(NotificationValidation.MaxRecipientTypeLength)]
		public string RecipientType { get; set; }

		public NotificationStatus Status { get; set; }

		[MaxLength(NotificationValidation.MaxSummaryLength)]
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
				throw new NotificationException("Id of domain and data objects don't match.");
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