namespace Nofy.Core
{
	public class Configurations
	{
		public readonly int BatchLimit;

		/// <summary>
		/// Configure Nofy service
		/// </summary>
		/// <param name="batchLimit">The threshold where the data should move from temporary storage to persistence.</param>
		public Configurations(int batchLimit = 0)
		{
			BatchLimit = batchLimit;
		}
	}
}