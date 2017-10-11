namespace Nofy.EntityFramework6.Migrations
{
	using System.Data.Entity.Migrations;

	public partial class Init : DbMigration
	{
		public override void Down()
		{
			this.DropForeignKey("dbo.NotificationAction", "NotificationId", "dbo.Notification");
			this.DropIndex("dbo.NotificationAction", new[] { "NotificationId" });
			this.DropTable("dbo.NotificationAction");
			this.DropTable("dbo.Notification");
		}

		public override void Up()
		{
			this.CreateTable(
					"dbo.Notification",
					c => new
					{
						Id = c.Int(nullable: false, identity: true),
						ArchivedOn = c.DateTime(),
						Category = c.Int(),
						CreatedOn = c.DateTime(nullable: false),
						Description = c.String(maxLength: 1000),
						EntityId = c.String(maxLength: 50, unicode: false),
						EntityType = c.String(maxLength: 200, unicode: false),
						RecipientId = c.String(maxLength: 50, unicode: false),
						RecipientType = c.String(maxLength: 50, unicode: false),
						Status = c.Int(nullable: false),
						Summary = c.String(maxLength: 100),
					})
				.PrimaryKey(t => t.Id);

			this.CreateTable(
					"dbo.NotificationAction",
					c => new
					{
						Id = c.Int(nullable: false, identity: true),
						ActionLink = c.String(maxLength: 1000, unicode: false),
						Label = c.String(maxLength: 50),
						NotificationId = c.Int(nullable: false),
					})
				.PrimaryKey(t => t.Id)
				.ForeignKey("dbo.Notification", t => t.NotificationId, cascadeDelete: true)
				.Index(t => t.NotificationId);
		}
	}
}