namespace Nofy.EntityFramework6.Mappings
{
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Data.Entity.ModelConfiguration;
	using Nofy.Core.Model;

	internal class NotificationActionsMap : EntityTypeConfiguration<NotificationAction>
	{
		public NotificationActionsMap(string schema)
		{
			this.ToTable("NotificationAction", schema);
			this.HasKey(t => t.Id);
			this.Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
			this.Property(t => t.Label).HasColumnName("Label").HasMaxLength(NotificationActionValidation.MaxLabelLength).IsUnicode();
			this.Property(t => t.ActionLink).HasColumnName("ActionLink").HasMaxLength(NotificationActionValidation.MaxLinkLength).IsUnicode(false);
			this.Property(t => t.NotificationId).HasColumnName("NotificationId");
		}
	}
}