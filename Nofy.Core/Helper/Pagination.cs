using System.Collections.Generic;

namespace Nofy.Core.Helper
{
	public class PaginatedData<T>
	{
		public IEnumerable<T> Results { get; set; }
		public int TotalCount { get; set; }
	}
}