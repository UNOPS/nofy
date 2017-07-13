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

			var validations = ValidateNotification(this, new ValidationContext(this)).ToList();
			if (validations.Any())
			{
				throw new ValidationException(string.Join(",", validations));
			}
		}

		public static IEnumerable<ValidationResult> ValidateNotification(Notification notification, ValidationContext context)
		{

			if (notification == null)
			{
				yield return new ValidationResult(string.Format("{0} is required.", context.DisplayName));
			}
			else
			{

				if (notification.Summary.Length > NotificationValidation.MaxSummaryLength)
				{
					yield return new ValidationResult(string.Format("{0} Summary exceeded the maximum length of {1}.", context.DisplayName, NotificationValidation.MaxSummaryLength));
				}

				if (notification.EntityType.Length > NotificationValidation.MaxEntityTypeLength)
				{
					yield return new ValidationResult(string.Format("{0} EntityType exceeded the maximum length of {1}.", context.DisplayName, NotificationValidation.MaxEntityTypeLength));
				}

				if (notification.EntityId.Length > NotificationValidation.MaxEntityIdLength)
				{
					yield return new ValidationResult(string.Format("{0} EntityId exceeded the maximum length of {1}.", context.DisplayName, NotificationValidation.MaxEntityIdLength));
				}

				if (notification.RecipientType.Length > NotificationValidation.MaxRecipientTypeLength)
				{
					yield return new ValidationResult(string.Format("{0} RecipientType exceeded the maximum length of {1}.", context.DisplayName, NotificationValidation.MaxRecipientTypeLength));
				}

				if (notification.RecipientId.Length > NotificationValidation.MaxRecipientIdLength)
				{
					yield return new ValidationResult(string.Format("{0} RecipientId exceeded the maximum length of {1}.", context.DisplayName, NotificationValidation.MaxRecipientIdLength));
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
		public string Description { get; set; }

		[StringLength(50, ErrorMessage = "EntityType exceeded the maximum length of 50")]
		public string EntityId { get; set; }

		[StringLength(200, ErrorMessage = "EntityType exceeded the maximum length of 50")]
		public string EntityType { get; set; }

		public int Id { get; set; }


		[StringLength(50, ErrorMessage = "EntityType exceeded the maximum length of 50")]
		public string RecipientId { get; set; }

		[StringLength(1, ErrorMessage = "EntityType exceeded the maximum length of 50")]
		public string RecipientType { get; set; }


		public NotificationStatus Status { get; set; }
		[StringLength(100, ErrorMessage = "EntityType exceeded the maximum length of 100")]
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