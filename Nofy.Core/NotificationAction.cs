namespace Nofy.Core
{
	using Newtonsoft.Json;
	using Nofy.Core.Model;

	public class NotificationAction
	{
		public NotificationAction(string label, string link)
		{
			this.Link = link;
			this.Label = label;
		}

		public NotificationAction(string label, object link)
		{
			this.Label = label;
			this.Link = JsonConvert.SerializeObject(link);
		}

		private NotificationAction()
		{
		}

		public string Label { get; private set; }

		public string Link { get; private set; }

		internal static NotificationAction Load(NotificationActionModel action)
		{
			return new NotificationAction
			{
				Label = action.Label,
				Link = action.ActionLink
			};
		}
	}
}