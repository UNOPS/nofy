# Nofy

Nofy is a very simple .NET Standard library to help you manage notifications inside your app.

# How to use?

Install the nuget package:

```
Install-Package Nofy.EntityFrameworkCore -Pre
```

Initialize database:

```
IF SCHEMA_ID(N'ntf') IS NULL EXEC(N'CREATE SCHEMA [ntf];');

GO

CREATE TABLE [ntf].[Notification] (
    [Id] int NOT NULL IDENTITY,
    [ArchivedOn] datetime2,
    [Category] int,
    [CreatedOn] datetime2 NOT NULL,
    [Description] nvarchar(1000),
    [EntityId] varchar(20),
    [EntityType] varchar(200),
    [RecipientId] varchar(50),
    [RecipientType] varchar(20),
    [Status] int NOT NULL,
    [Summary] nvarchar(100),
    CONSTRAINT [PK_Notification] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [ntf].[NotificationAction] (
    [Id] int NOT NULL IDENTITY,
    [ActionLink] varchar(1000),
    [Label] nvarchar(50),
    [NotificationId] int NOT NULL,
    CONSTRAINT [PK_NotificationAction] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_NotificationAction_Notification_NotificationId] FOREIGN KEY ([NotificationId]) REFERENCES [ntf].[Notification] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_NotificationAction_NotificationId] ON [ntf].[NotificationAction] ([NotificationId]);

GO
```