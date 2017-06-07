using Newtonsoft.Json;
using Nofy.Core.Model;

namespace Nofy.Core
{
	public class NotificationAction
	{
		public NotificationAction(string label, string link)
		{
			Link = link;
			Label = label;
		}

		public NotificationAction(string label, object link)
		{
			Label = label;
			Link = JsonConvert.SerializeObject(link);
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