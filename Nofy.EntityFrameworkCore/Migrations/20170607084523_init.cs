using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nofy.EntityFrameworkCore.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ntf");

            migrationBuilder.CreateTable(
                name: "Notification",
                schema: "ntf",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ArchivedOn = table.Column<DateTime>(nullable: true),
                    Category = table.Column<int>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    Desc = table.Column<string>(maxLength: 1000, nullable: true),
                    EntityId = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    EntityType = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    RecipientId = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    RecipientType = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Summary = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationAction",
                schema: "ntf",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActionLink = table.Column<string>(unicode: false, maxLength: 1000, nullable: true),
                    Label = table.Column<string>(maxLength: 50, nullable: true),
                    NotificationId = table.Column<int>(nullable: false),
                    NotificationModelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationAction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationAction_Notification_NotificationModelId",
                        column: x => x.NotificationModelId,
                        principalSchema: "ntf",
                        principalTable: "Notification",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotificationAction_NotificationModelId",
                schema: "ntf",
                table: "NotificationAction",
                column: "NotificationModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationAction",
                schema: "ntf");

            migrationBuilder.DropTable(
                name: "Notification",
                schema: "ntf");
        }
    }
}
