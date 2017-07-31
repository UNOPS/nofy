namespace Nofy.Core
{
    /// <summary>
    /// Represents notification recipent 
    /// </summary>
    public struct NotificationRecipient
    {
        /// <summary>
        /// Initialize new instance of recipient 
        /// </summary>
        /// <param name="recipientType">Type could be role, user etc.. </param>
        /// <param name="recipientId">Recipient id could be any key for the receipient type like roleId, userId etc.. </param>
        public NotificationRecipient(string recipientType, string recipientId)
        {
            this.RecipientType = recipientType;
            this.RecipientId = recipientId;
        }

        public string RecipientType { get; }
        public string RecipientId { get; }
    }
}