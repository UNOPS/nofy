namespace Nofy.Core
{
	/// <summary>
	/// Represents notification recipient
	/// </summary>
	public struct NotificationRecipient
	{
		/// <summary>
		/// Initialize new instance of recipient 
		/// </summary>
		/// <param name="recipientType">Type could be role, user etc.. </param>
		/// <param name="recipientId">Recipient id could be any key for the recipient type like roleId, userId etc.. </param>
		public NotificationRecipient(string recipientType, string recipientId)
		{
			this.RecipientType = recipientType;
			this.RecipientId = recipientId;
		}

		/// <summary>
		/// Recipient type (role, user, etc.)
		/// </summary>
		public string RecipientType { get; }

		/// <summary>
		/// Recipient id (role id, user id, etc.)
		/// </summary>
		public string RecipientId { get; }
	}
}