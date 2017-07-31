namespace Nofy.Core.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class NotificationAction
    {
        public NotificationAction(string actionLink, string label, int notificationId)
        {
            this.ActionLink = actionLink;
            this.Label = label;
            this.NotificationId = notificationId;

            var validations = ValidateNotificationAction(this, new ValidationContext(this)).ToList();
            if (validations.Any())
            {
                throw new ValidationException(string.Join(",", validations));
            }
        }

        public NotificationAction()
        {
        }

        public string ActionLink { get; set; }
        public int Id { get; set; }
        public string Label { get; set; }
        public int NotificationId { get; set; }

        public static IEnumerable<ValidationResult> ValidateNotificationAction(NotificationAction na, ValidationContext context)
        {
            if (na == null)
            {
                yield return new ValidationResult(string.Format("{0} is required.", context.DisplayName));
            }
            else
            {
                if (na.Label.Length > NotificationActionValidation.MaxLabelLength)
                {
                    yield return new ValidationResult(string.Format("{0} Label exceeded the maximum length of {1}.", context.DisplayName,
                        NotificationActionValidation.MaxLabelLength));
                }

                if (na.ActionLink.Length > NotificationActionValidation.MaxLinkLength)
                {
                    yield return new ValidationResult(string.Format("{0} Label exceeded the maximum length of {1}.", context.DisplayName,
                        NotificationActionValidation.MaxLinkLength));
                }
            }
        }
    }
}