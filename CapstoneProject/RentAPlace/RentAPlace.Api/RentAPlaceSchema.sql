IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Users] (
    [UserId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [PasswordHash] nvarchar(max) NOT NULL,
    [Role] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([UserId])
);

CREATE TABLE [Properties] (
    [PropertyId] int NOT NULL IDENTITY,
    [OwnerId] int NOT NULL,
    [Title] nvarchar(200) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [Location] nvarchar(200) NOT NULL,
    [Type] nvarchar(50) NOT NULL,
    [Features] nvarchar(max) NOT NULL,
    [PricePerNight] decimal(18,2) NOT NULL,
    [Rating] decimal(18,2) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Properties] PRIMARY KEY ([PropertyId]),
    CONSTRAINT [FK_Properties_Users_OwnerId] FOREIGN KEY ([OwnerId]) REFERENCES [Users] ([UserId]) ON DELETE CASCADE
);

CREATE TABLE [Messages] (
    [MessageId] int NOT NULL IDENTITY,
    [SenderId] int NOT NULL,
    [ReceiverId] int NOT NULL,
    [PropertyId] int NULL,
    [MessageText] nvarchar(2000) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Messages] PRIMARY KEY ([MessageId]),
    CONSTRAINT [FK_Messages_Properties_PropertyId] FOREIGN KEY ([PropertyId]) REFERENCES [Properties] ([PropertyId]),
    CONSTRAINT [FK_Messages_Users_ReceiverId] FOREIGN KEY ([ReceiverId]) REFERENCES [Users] ([UserId]),
    CONSTRAINT [FK_Messages_Users_SenderId] FOREIGN KEY ([SenderId]) REFERENCES [Users] ([UserId])
);

CREATE TABLE [PropertyImages] (
    [PropertyImageId] int NOT NULL IDENTITY,
    [PropertyId] int NOT NULL,
    [ImageUrl] nvarchar(500) NOT NULL,
    CONSTRAINT [PK_PropertyImages] PRIMARY KEY ([PropertyImageId]),
    CONSTRAINT [FK_PropertyImages_Properties_PropertyId] FOREIGN KEY ([PropertyId]) REFERENCES [Properties] ([PropertyId]) ON DELETE CASCADE
);

CREATE TABLE [Reservations] (
    [ReservationId] int NOT NULL IDENTITY,
    [PropertyId] int NOT NULL,
    [UserId] int NOT NULL,
    [CheckInDate] datetime2 NOT NULL,
    [CheckOutDate] datetime2 NOT NULL,
    [Status] nvarchar(20) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Reservations] PRIMARY KEY ([ReservationId]),
    CONSTRAINT [FK_Reservations_Properties_PropertyId] FOREIGN KEY ([PropertyId]) REFERENCES [Properties] ([PropertyId]),
    CONSTRAINT [FK_Reservations_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId])
);

CREATE INDEX [IX_Messages_PropertyId] ON [Messages] ([PropertyId]);

CREATE INDEX [IX_Messages_ReceiverId] ON [Messages] ([ReceiverId]);

CREATE INDEX [IX_Messages_SenderId] ON [Messages] ([SenderId]);

CREATE INDEX [IX_Properties_OwnerId] ON [Properties] ([OwnerId]);

CREATE INDEX [IX_PropertyImages_PropertyId] ON [PropertyImages] ([PropertyId]);

CREATE INDEX [IX_Reservations_PropertyId] ON [Reservations] ([PropertyId]);

CREATE INDEX [IX_Reservations_UserId] ON [Reservations] ([UserId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250902132441_InitialCreate4', N'9.0.8');

COMMIT;
GO

