namespace Nofy.EntityFrameworkCore.Migrations
{
	using System;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Infrastructure;
	using Microsoft.EntityFrameworkCore.Metadata;
	using Nofy.EntityFrameworkCore.DataAccess;

	[DbContext(typeof(NotificationsDbContext))]
	partial class NotificationsDbContextModelSnapshot : ModelSnapshot
	{
		protected override void BuildModel(ModelBuilder modelBuilder)
		{
			modelBuilder
				.HasDefaultSchema("ntf")
				.HasAnnotation("ProductVersion", "1.1.2")
				.HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

			modelBuilder.Entity("Nofy.Core.Model.Notification", b =>
			{
				b.Property<int>("Id")
					.ValueGeneratedOnAdd()
					.HasColumnName("Id")
					.HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

				b.Property<DateTime?>("ArchivedOn")
					.HasColumnName("ArchivedOn");

				b.Property<int?>("Category")
					.HasColumnName("Category");

				b.Property<DateTime>("CreatedOn")
					.HasColumnName("CreatedOn");

				b.Property<string>("Description")
					.HasColumnName("Description")
					.HasMaxLength(1000);

				b.Property<string>("EntityId")
					.HasColumnName("EntityId")
					.HasMaxLength(20)
					.IsUnicode(false);

				b.Property<string>("EntityType")
					.HasColumnName("EntityType")
					.HasMaxLength(200)
					.IsUnicode(false);

				b.Property<string>("RecipientId")
					.HasColumnName("RecipientId")
					.HasMaxLength(50)
					.IsUnicode(false);

				b.Property<string>("RecipientType")
					.HasColumnName("RecipientType")
					.HasMaxLength(20)
					.IsUnicode(false);

				b.Property<int>("Status")
					.HasColumnName("Status");

				b.Property<string>("Summary")
					.HasColumnName("Summary")
					.HasMaxLength(100);

				b.HasKey("Id");

				b.ToTable("Notification");
			});

			modelBuilder.Entity("Nofy.Core.Model.NotificationAction", b =>
			{
				b.Property<int>("Id")
					.ValueGeneratedOnAdd()
					.HasColumnName("Id")
					.HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

				b.Property<string>("ActionLink")
					.HasColumnName("ActionLink")
					.HasMaxLength(1000)
					.IsUnicode(false);

				b.Property<string>("Label")
					.HasColumnName("Label")
					.HasMaxLength(50)
					.IsUnicode(true);

				b.Property<int>("NotificationId")
					.HasColumnName("NotificationId");

				b.HasKey("Id");

				b.HasIndex("NotificationId");

				b.ToTable("NotificationAction");
			});

			modelBuilder.Entity("Nofy.Core.Model.NotificationAction", b =>
			{
				b.HasOne("Nofy.Core.Model.Notification")
					.WithMany("Actions")
					.HasForeignKey("NotificationId")
					.OnDelete(DeleteBehavior.Cascade);
			});
		}
	}
}