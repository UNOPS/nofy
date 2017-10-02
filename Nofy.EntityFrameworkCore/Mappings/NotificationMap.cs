namespace Nofy.EntityFrameworkCore.Mappings
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;
	using Nofy.Core.Model;

	internal class NotificationMap : DbEntityConfiguration<Notification>
	{
		private readonly string schema;

		public NotificationMap(string schema)
		{
			this.schema = schema;
		}

		public override void Configure(EntityTypeBuilder<Notification> entity)
		{
			entity.ToTable("Notification", this.schema);
			entity.HasKey(t => t.Id);
			entity.Property(t => t.Id).HasColumnName("Id").UseSqlServerIdentityColumn();
			entity.Property(t => t.Status).HasColumnName("Status");
			entity.Property(t => t.ArchivedOn).HasColumnName("ArchivedOn");
			entity.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
			entity.Property(t => t.Description).HasColumnName("Description").HasMaxLength(NotificationValidation.MaxDescriptionLength);
			entity.Property(t => t.Summary).HasColumnName("Summary").HasMaxLength(NotificationValidation.MaxSummaryLength);
			entity.Property(t => t.EntityId).HasColumnName("EntityId").HasMaxLength(NotificationValidation.MaxEntityIdLength).IsUnicode(false);
			entity.Property(t => t.EntityType).HasColumnName("EntityType").HasMaxLength(NotificationValidation.MaxEntityTypeLength).IsUnicode(false);
			entity.Property(t => t.RecipientId).HasColumnName("RecipientId").HasMaxLength(NotificationValidation.MaxRecipientIdLength)
				.IsUnicode(false);
			entity.Property(t => t.RecipientType).HasColumnName("RecipientType").HasMaxLength(NotificationValidation.MaxRecipientTypeLength)
				.IsUnicode(false);
			entity.Property(t => t.Category).HasColumnName("Category");
		}
	}
}