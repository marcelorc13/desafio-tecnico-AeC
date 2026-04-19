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
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [Username] nvarchar(50) NOT NULL,
    [Password] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);

CREATE TABLE [Addresses] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [CEP] nvarchar(8) NOT NULL,
    [PublicPlace] nvarchar(200) NOT NULL,
    [Complement] nvarchar(100) NULL,
    [District] nvarchar(100) NOT NULL,
    [City] nvarchar(100) NOT NULL,
    [FederalUnit] nvarchar(2) NOT NULL,
    [Number] nvarchar(10) NOT NULL,
    [UserId] int NOT NULL,
    CONSTRAINT [PK_Addresses] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Addresses_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_Addresses_UserId] ON [Addresses] ([UserId]);

CREATE UNIQUE INDEX [IX_Users_Username] ON [Users] ([Username]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260419203017_CreateUsersAndAddressesTables', N'10.0.6');

COMMIT;
GO

