namespace Nofy.EntityFramework6.Mappings
{
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Data.Entity.ModelConfiguration;
	using Nofy.Core.Model;

	internal class NotificationMap : EntityTypeConfiguration<Notification>
	{
		public NotificationMap(string schema)
		{
			this.ToTable("Notification", schema);
			this.HasKey(t => t.Id);
			this.Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
			this.Property(t => t.Status).HasColumnName("Status");
			this.Property(t => t.ArchivedOn).HasColumnName("ArchivedOn");
			this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
			this.Property(t => t.Description).HasColumnName("Description").HasMaxLength(NotificationValidation.MaxDescriptionLength);
			this.Property(t => t.Summary).HasColumnName("Summary").HasMaxLength(NotificationValidation.MaxSummaryLength);
			this.Property(t => t.EntityId).HasColumnName("EntityId").HasMaxLength(NotificationValidation.MaxEntityIdLength).IsUnicode(false);
			this.Property(t => t.EntityType).HasColumnName("EntityType").HasMaxLength(NotificationValidation.MaxEntityTypeLength).IsUnicode(false);
			this.Property(t => t.RecipientId).HasColumnName("RecipientId").HasMaxLength(NotificationValidation.MaxRecipientIdLength)
				.IsUnicode(false);
			this.Property(t => t.RecipientType).HasColumnName("RecipientType").HasMaxLength(NotificationValidation.MaxRecipientTypeLength)
				.IsUnicode(false);
			this.Property(t => t.Category).HasColumnName("Category");
			this.Property(t => t.Archived).HasColumnName("Archived");
		}
	}
}