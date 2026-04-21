BEGIN TRANSACTION;

CREATE UNIQUE INDEX [IX_Addresses_Name_UserId] ON [Addresses] ([Name], [UserId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260421152227_AddUniqueConstraintToAddressName', N'10.0.6');

COMMIT;
GO
