namespace Nofy.EntityFrameworkCore
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using LinqKit;
	using Microsoft.EntityFrameworkCore;
	using Nofy.Core;
	using Nofy.Core.Helper;
	using Nofy.Core.Model;

	internal static class Extensions
	{
		public static void AddConfiguration<TEntity>(
			this ModelBuilder modelBuilder,
			DbEntityConfiguration<TEntity> entityConfiguration) where TEntity : class
		{
			modelBuilder.Entity<TEntity>(entityConfiguration.Configure);
		}

		public static IQueryable<Notification> BelongingTo(this IQueryable<Notification> queryable, params NotificationRecipient[] filters)
		{
			var filter = PredicateBuilder.New<Notification>(true);

			foreach (var recipient in filters)
			{
				filter = filter.Or(t => t.RecipientType == recipient.RecipientType && t.RecipientId == recipient.RecipientId);
			}

			return queryable
				.Where(filter);
		}

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

			// Return empty list if there is no data
			if (rowsCount <= 0)
			{
				return new PaginatedData<T>
				{
					Results = new List<T>(),
					TotalCount = rowsCount
				};
			}

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