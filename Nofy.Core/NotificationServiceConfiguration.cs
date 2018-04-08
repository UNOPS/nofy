namespace Nofy.Core
{
	/// <summary>
	/// Notification service configuration class
	/// </summary>
	public class NotificationServiceConfiguration
	{
		/// <summary>
		/// The threshold where the data should move from temporary storage to persistence
		/// </summary>
		public int BatchLimit { get; set; }

        /// <summary>
        /// Set the length limitation for description field
        /// </summary>
        public static int DescriptionLimit { set; get; } = 1000;
    }
}