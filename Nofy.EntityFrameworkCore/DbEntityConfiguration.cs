using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nofy.EntityFrameworkCore
{
	public abstract class DbEntityConfiguration<TEntity> where TEntity : class
	{
		public abstract void Configure(EntityTypeBuilder<TEntity> entity);
	}
}