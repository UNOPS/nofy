namespace Nofy.Core
{
	using System;

	/// <inheritdoc />
	/// <summary>
	/// Notification exception
	/// </summary>
	public class NotificationException : Exception
	{
		/// <inheritdoc />
		public NotificationException(string message) : base(message)
		{
		}

		/// <inheritdoc />
		public NotificationException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}