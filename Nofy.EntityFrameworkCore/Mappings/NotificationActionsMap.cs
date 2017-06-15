﻿namespace Nofy.EntityFrameworkCore.Mappings
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;
	using Nofy.Core.Model;

	internal class NotificationActionsMap : DbEntityConfiguration<NotificationAction>
	{
		public override void Configure(EntityTypeBuilder<NotificationAction> entity)
		{
			entity.ToTable("NotificationAction");
			entity.HasKey(t => t.Id);
			entity.Property(t => t.Id).HasColumnName("Id").UseSqlServerIdentityColumn();
			entity.Property(t => t.Label).HasColumnName("Label").HasMaxLength(50).IsUnicode();
			entity.Property(t => t.ActionLink).HasColumnName("ActionLink").HasMaxLength(1000).IsUnicode(false);
			entity.Property(t => t.NotificationId).HasColumnName("NotificationId");
		}
	}
}