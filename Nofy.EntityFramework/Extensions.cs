namespace Nofy.EntityFramework
{
	using System;
	using System.Linq;
	using Nofy.Core.Helper;

	internal static class Extensions
	{
		public static PaginatedData<T> Paginate<T>(
			this IQueryable<T> query,
			int pageNum,
			int pageSize)
		{
			if (pageSize <= 0)
			{
				pageSize = 10;
			}

			//Total result count
			var rowsCount = query.Count();

			//If page number should be > 0 else set to first page
			if (rowsCount <= pageSize || pageNum <= 0)
			{
				pageNum = 1;
			}

			//Calculate number of rows to skip on page size
			var excludedRows = (pageNum - 1) * pageSize;

			return new PaginatedData<T>
			{
				Results = query.Skip(excludedRows).Take(Math.Min(pageSize, rowsCount)).ToArray(),
				TotalCount = rowsCount
			};
		}
	}
}