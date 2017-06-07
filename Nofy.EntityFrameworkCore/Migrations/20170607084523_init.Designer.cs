using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Nofy.EntityFrameworkCore.DataAccess;
using Nofy.Core;

namespace Nofy.EntityFrameworkCore.Migrations
{
    [DbContext(typeof(NotificationsDbContext))]
    [Migration("20170607084523_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasDefaultSchema("ntf")
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Nofy.Core.Model.NotificationActionModel", b =>
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

                    b.Property<int?>("NotificationModelId");

                    b.HasKey("Id");

                    b.HasIndex("NotificationModelId");

                    b.ToTable("NotificationAction");
                });

            modelBuilder.Entity("Nofy.Core.Model.NotificationModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("ArchivedOn")
                        .HasColumnName("ArchivedOn");

                    b.Property<int?>("Category")
                        .HasColumnName("Category");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnName("DateCreated");

                    b.Property<string>("Description")
                        .HasColumnName("Desc")
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

            modelBuilder.Entity("Nofy.Core.Model.NotificationActionModel", b =>
                {
                    b.HasOne("Nofy.Core.Model.NotificationModel")
                        .WithMany("Actions")
                        .HasForeignKey("NotificationModelId");
                });
        }
    }
}
