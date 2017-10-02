namespace Nofy.EntityFrameworkCore.Mappings
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;
	using Nofy.Core.Model;

	internal class NotificationActionsMap : DbEntityConfiguration<NotificationAction>
	{
		private readonly string schema;

		public NotificationActionsMap(string schema)
		{
			this.schema = schema;
		}

		public override void Configure(EntityTypeBuilder<NotificationAction> entity)
		{
			entity.ToTable("NotificationAction", this.schema);
			entity.HasKey(t => t.Id);
			entity.Property(t => t.Id).HasColumnName("Id").UseSqlServerIdentityColumn();
			entity.Property(t => t.Label).HasColumnName("Label").HasMaxLength(NotificationActionValidation.MaxLabelLength).IsUnicode();
			entity.Property(t => t.ActionLink).HasColumnName("ActionLink").HasMaxLength(NotificationActionValidation.MaxLinkLength).IsUnicode(false);
			entity.Property(t => t.NotificationId).HasColumnName("NotificationId");
		}
	}
}