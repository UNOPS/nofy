namespace Nofy.EntityFrameworkCore.Mappings
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;
	using Nofy.Core.Model;

	internal class NotificationMap : DbEntityConfiguration<Notification>
	{
		public const int MaxDescriptionLength = 1000;
		public const int MaxSummaryLength = 100;

		public override void Configure(EntityTypeBuilder<Notification> entity)
		{
			entity.ToTable("Notification");
			entity.HasKey(t => t.Id);
			entity.Property(t => t.Id).HasColumnName("Id").UseSqlServerIdentityColumn();
			entity.Property(t => t.Status).HasColumnName("Status");
			entity.Property(t => t.ArchivedOn).HasColumnName("ArchivedOn");
			entity.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
			entity.Property(t => t.Description).HasColumnName("Description").HasMaxLength(MaxDescriptionLength);
			entity.Property(t => t.Summary).HasColumnName("Summary").HasMaxLength(MaxSummaryLength);
			entity.Property(t => t.EntityId).HasColumnName("EntityId").HasMaxLength(20).IsUnicode(false);
			entity.Property(t => t.EntityType).HasColumnName("EntityType").HasMaxLength(200).IsUnicode(false);
			entity.Property(t => t.RecipientId).HasColumnName("RecipientId").HasMaxLength(50).IsUnicode(false);
			entity.Property(t => t.RecipientType).HasColumnName("RecipientType").HasMaxLength(20).IsUnicode(false);
			entity.Property(t => t.Category).HasColumnName("Category");
		}
	}
}