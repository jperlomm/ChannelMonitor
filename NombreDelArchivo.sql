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
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240201201738_CreateDbAndTable'
)
BEGIN
    CREATE TABLE [AlertStatus] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Color] nvarchar(max) NULL,
        [Emoji] nvarchar(max) NULL,
        CONSTRAINT [PK_AlertStatus] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240201201738_CreateDbAndTable'
)
BEGIN
    CREATE TABLE [ChannelOrigins] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_ChannelOrigins] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240201201738_CreateDbAndTable'
)
BEGIN
    CREATE TABLE [FailureTypes] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_FailureTypes] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240201201738_CreateDbAndTable'
)
BEGIN
    CREATE TABLE [ChannelDetails] (
        [Id] int NOT NULL IDENTITY,
        [IdChannel] int NULL,
        [PidAudio] int NULL,
        [PidVideo] int NULL,
        [ChannelOriginId] int NULL,
        CONSTRAINT [PK_ChannelDetails] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ChannelDetails_ChannelOrigins_ChannelOriginId] FOREIGN KEY ([ChannelOriginId]) REFERENCES [ChannelOrigins] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240201201738_CreateDbAndTable'
)
BEGIN
    CREATE TABLE [Channels] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Number] int NOT NULL,
        [Ip] nvarchar(max) NOT NULL,
        [Port] nvarchar(max) NOT NULL,
        [InProcessing] bit NOT NULL,
        [ShouldMonitorVideo] bit NOT NULL,
        [ShouldMonitorAudio] bit NOT NULL,
        [AudioThreshold] int NOT NULL,
        [VideoFilterLevel] int NOT NULL,
        [MonitoringStartTime] time NULL,
        [MonitoringEndTime] time NULL,
        [VideoFailureId] int NOT NULL,
        [AudioFailureId] int NOT NULL,
        [GeneralFailureId] int NOT NULL,
        [ChannelDetailsId] int NOT NULL,
        [LastScan] datetime2 NULL,
        [LastVolume] float NULL,
        [IdChannelBackUp] int NULL,
        CONSTRAINT [PK_Channels] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Channels_AlertStatus_AudioFailureId] FOREIGN KEY ([AudioFailureId]) REFERENCES [AlertStatus] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Channels_AlertStatus_GeneralFailureId] FOREIGN KEY ([GeneralFailureId]) REFERENCES [AlertStatus] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Channels_AlertStatus_VideoFailureId] FOREIGN KEY ([VideoFailureId]) REFERENCES [AlertStatus] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Channels_ChannelDetails_ChannelDetailsId] FOREIGN KEY ([ChannelDetailsId]) REFERENCES [ChannelDetails] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240201201738_CreateDbAndTable'
)
BEGIN
    CREATE TABLE [FailureLoggings] (
        [Id] int NOT NULL IDENTITY,
        [IdChannel] int NOT NULL,
        [FailureTypeId] int NOT NULL,
        [Url] nvarchar(max) NULL,
        [DateFailure] datetime2 NULL,
        [ChannelId] int NULL,
        CONSTRAINT [PK_FailureLoggings] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_FailureLoggings_Channels_ChannelId] FOREIGN KEY ([ChannelId]) REFERENCES [Channels] ([Id]),
        CONSTRAINT [FK_FailureLoggings_FailureTypes_FailureTypeId] FOREIGN KEY ([FailureTypeId]) REFERENCES [FailureTypes] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240201201738_CreateDbAndTable'
)
BEGIN
    CREATE INDEX [IX_ChannelDetails_ChannelOriginId] ON [ChannelDetails] ([ChannelOriginId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240201201738_CreateDbAndTable'
)
BEGIN
    CREATE INDEX [IX_Channels_AudioFailureId] ON [Channels] ([AudioFailureId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240201201738_CreateDbAndTable'
)
BEGIN
    CREATE INDEX [IX_Channels_ChannelDetailsId] ON [Channels] ([ChannelDetailsId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240201201738_CreateDbAndTable'
)
BEGIN
    CREATE INDEX [IX_Channels_GeneralFailureId] ON [Channels] ([GeneralFailureId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240201201738_CreateDbAndTable'
)
BEGIN
    CREATE INDEX [IX_Channels_VideoFailureId] ON [Channels] ([VideoFailureId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240201201738_CreateDbAndTable'
)
BEGIN
    CREATE INDEX [IX_FailureLoggings_ChannelId] ON [FailureLoggings] ([ChannelId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240201201738_CreateDbAndTable'
)
BEGIN
    CREATE INDEX [IX_FailureLoggings_FailureTypeId] ON [FailureLoggings] ([FailureTypeId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240201201738_CreateDbAndTable'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240201201738_CreateDbAndTable', N'8.0.1');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240202111457_QuitamosRestriccionChannelDetailsInChannel'
)
BEGIN
    ALTER TABLE [Channels] DROP CONSTRAINT [FK_Channels_ChannelDetails_ChannelDetailsId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240202111457_QuitamosRestriccionChannelDetailsInChannel'
)
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Channels]') AND [c].[name] = N'ChannelDetailsId');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Channels] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Channels] ALTER COLUMN [ChannelDetailsId] int NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240202111457_QuitamosRestriccionChannelDetailsInChannel'
)
BEGIN
    ALTER TABLE [Channels] ADD CONSTRAINT [FK_Channels_ChannelDetails_ChannelDetailsId] FOREIGN KEY ([ChannelDetailsId]) REFERENCES [ChannelDetails] ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240202111457_QuitamosRestriccionChannelDetailsInChannel'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240202111457_QuitamosRestriccionChannelDetailsInChannel', N'8.0.1');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240202133106_propiedadAlertStatusOpcional'
)
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Channels]') AND [c].[name] = N'VideoFailureId');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Channels] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Channels] ALTER COLUMN [VideoFailureId] int NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240202133106_propiedadAlertStatusOpcional'
)
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Channels]') AND [c].[name] = N'GeneralFailureId');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Channels] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Channels] ALTER COLUMN [GeneralFailureId] int NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240202133106_propiedadAlertStatusOpcional'
)
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Channels]') AND [c].[name] = N'AudioFailureId');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Channels] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [Channels] ALTER COLUMN [AudioFailureId] int NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240202133106_propiedadAlertStatusOpcional'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240202133106_propiedadAlertStatusOpcional', N'8.0.1');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240205145505_AddPropertyChannelDetailsId'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240205145505_AddPropertyChannelDetailsId', N'8.0.1');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240206211836_Errores'
)
BEGIN
    CREATE TABLE [Errors] (
        [Id] uniqueidentifier NOT NULL,
        [Message] nvarchar(max) NOT NULL,
        [StackTrace] nvarchar(max) NULL,
        [Date] datetime2 NOT NULL,
        CONSTRAINT [PK_Errors] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240206211836_Errores'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240206211836_Errores', N'8.0.1');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240206235654_SistemaDeUsuarios'
)
BEGIN
    CREATE TABLE [Roles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_Roles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240206235654_SistemaDeUsuarios'
)
BEGIN
    CREATE TABLE [Usuarios] (
        [Id] nvarchar(450) NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_Usuarios] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240206235654_SistemaDeUsuarios'
)
BEGIN
    CREATE TABLE [RolesClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_RolesClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_RolesClaims_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240206235654_SistemaDeUsuarios'
)
BEGIN
    CREATE TABLE [UsuariosClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_UsuariosClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_UsuariosClaims_Usuarios_UserId] FOREIGN KEY ([UserId]) REFERENCES [Usuarios] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240206235654_SistemaDeUsuarios'
)
BEGIN
    CREATE TABLE [UsuariosLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_UsuariosLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_UsuariosLogins_Usuarios_UserId] FOREIGN KEY ([UserId]) REFERENCES [Usuarios] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240206235654_SistemaDeUsuarios'
)
BEGIN
    CREATE TABLE [UsuariosRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_UsuariosRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_UsuariosRoles_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_UsuariosRoles_Usuarios_UserId] FOREIGN KEY ([UserId]) REFERENCES [Usuarios] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240206235654_SistemaDeUsuarios'
)
BEGIN
    CREATE TABLE [UsuariosTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_UsuariosTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_UsuariosTokens_Usuarios_UserId] FOREIGN KEY ([UserId]) REFERENCES [Usuarios] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240206235654_SistemaDeUsuarios'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [Roles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240206235654_SistemaDeUsuarios'
)
BEGIN
    CREATE INDEX [IX_RolesClaims_RoleId] ON [RolesClaims] ([RoleId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240206235654_SistemaDeUsuarios'
)
BEGIN
    CREATE INDEX [EmailIndex] ON [Usuarios] ([NormalizedEmail]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240206235654_SistemaDeUsuarios'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [Usuarios] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240206235654_SistemaDeUsuarios'
)
BEGIN
    CREATE INDEX [IX_UsuariosClaims_UserId] ON [UsuariosClaims] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240206235654_SistemaDeUsuarios'
)
BEGIN
    CREATE INDEX [IX_UsuariosLogins_UserId] ON [UsuariosLogins] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240206235654_SistemaDeUsuarios'
)
BEGIN
    CREATE INDEX [IX_UsuariosRoles_RoleId] ON [UsuariosRoles] ([RoleId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240206235654_SistemaDeUsuarios'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240206235654_SistemaDeUsuarios', N'8.0.1');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240208235629_QuitamosChannelIDFailureChannel'
)
BEGIN
    ALTER TABLE [FailureLoggings] DROP CONSTRAINT [FK_FailureLoggings_FailureTypes_FailureTypeId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240208235629_QuitamosChannelIDFailureChannel'
)
BEGIN
    DROP INDEX [IX_FailureLoggings_FailureTypeId] ON [FailureLoggings];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240208235629_QuitamosChannelIDFailureChannel'
)
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[FailureLoggings]') AND [c].[name] = N'FailureTypeId');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [FailureLoggings] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [FailureLoggings] ALTER COLUMN [FailureTypeId] int NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240208235629_QuitamosChannelIDFailureChannel'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240208235629_QuitamosChannelIDFailureChannel', N'8.0.1');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240209001429_UpdateChannelId'
)
BEGIN
    ALTER TABLE [FailureLoggings] DROP CONSTRAINT [FK_FailureLoggings_Channels_ChannelId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240209001429_UpdateChannelId'
)
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[FailureLoggings]') AND [c].[name] = N'IdChannel');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [FailureLoggings] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [FailureLoggings] DROP COLUMN [IdChannel];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240209001429_UpdateChannelId'
)
BEGIN
    DROP INDEX [IX_FailureLoggings_ChannelId] ON [FailureLoggings];
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[FailureLoggings]') AND [c].[name] = N'ChannelId');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [FailureLoggings] DROP CONSTRAINT [' + @var6 + '];');
    EXEC(N'UPDATE [FailureLoggings] SET [ChannelId] = 0 WHERE [ChannelId] IS NULL');
    ALTER TABLE [FailureLoggings] ALTER COLUMN [ChannelId] int NOT NULL;
    ALTER TABLE [FailureLoggings] ADD DEFAULT 0 FOR [ChannelId];
    CREATE INDEX [IX_FailureLoggings_ChannelId] ON [FailureLoggings] ([ChannelId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240209001429_UpdateChannelId'
)
BEGIN
    ALTER TABLE [FailureLoggings] ADD CONSTRAINT [FK_FailureLoggings_Channels_ChannelId] FOREIGN KEY ([ChannelId]) REFERENCES [Channels] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240209001429_UpdateChannelId'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240209001429_UpdateChannelId', N'8.0.1');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240216143753_AddPropertyDetailFailureLog'
)
BEGIN
    ALTER TABLE [FailureLoggings] ADD [Detail] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240216143753_AddPropertyDetailFailureLog'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240216143753_AddPropertyDetailFailureLog', N'8.0.1');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240228142829_AddMultiTenant'
)
BEGIN
    ALTER TABLE [Usuarios] ADD [TenantId] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240228142829_AddMultiTenant'
)
BEGIN
    ALTER TABLE [FailureLoggings] ADD [TenantId] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240228142829_AddMultiTenant'
)
BEGIN
    ALTER TABLE [Channels] ADD [TenantId] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240228142829_AddMultiTenant'
)
BEGIN
    ALTER TABLE [ChannelOrigins] ADD [TenantId] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240228142829_AddMultiTenant'
)
BEGIN
    ALTER TABLE [ChannelDetails] ADD [TenantId] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240228142829_AddMultiTenant'
)
BEGIN
    CREATE TABLE [Tenants] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NULL,
        CONSTRAINT [PK_Tenants] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240228142829_AddMultiTenant'
)
BEGIN
    CREATE INDEX [IX_Usuarios_TenantId] ON [Usuarios] ([TenantId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240228142829_AddMultiTenant'
)
BEGIN
    ALTER TABLE [Usuarios] ADD CONSTRAINT [FK_Usuarios_Tenants_TenantId] FOREIGN KEY ([TenantId]) REFERENCES [Tenants] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240228142829_AddMultiTenant'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240228142829_AddMultiTenant', N'8.0.1');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240228143135_InitialCreate'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240228143135_InitialCreate', N'8.0.1');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240228151822_AddValueaAlertStatusFailureTypes'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Color', N'Emoji', N'Name') AND [object_id] = OBJECT_ID(N'[AlertStatus]'))
        SET IDENTITY_INSERT [AlertStatus] ON;
    EXEC(N'INSERT INTO [AlertStatus] ([Id], [Color], [Emoji], [Name])
    VALUES (1, N''green'', NULL, N''Ok''),
    (2, N''yellow'', NULL, N''Alert''),
    (3, N''red'', NULL, N''Fail''),
    (4, N''grey'', NULL, N''Pause'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Color', N'Emoji', N'Name') AND [object_id] = OBJECT_ID(N'[AlertStatus]'))
        SET IDENTITY_INSERT [AlertStatus] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240228151822_AddValueaAlertStatusFailureTypes'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[FailureTypes]'))
        SET IDENTITY_INSERT [FailureTypes] ON;
    EXEC(N'INSERT INTO [FailureTypes] ([Id], [Name])
    VALUES (1, N''Audio''),
    (2, N''Video''),
    (3, N''General'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[FailureTypes]'))
        SET IDENTITY_INSERT [FailureTypes] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240228151822_AddValueaAlertStatusFailureTypes'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240228151822_AddValueaAlertStatusFailureTypes', N'8.0.1');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240229113734_CreateSuperUser'
)
BEGIN
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[FailureLoggings]') AND [c].[name] = N'TenantId');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [FailureLoggings] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [FailureLoggings] ALTER COLUMN [TenantId] uniqueidentifier NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240229113734_CreateSuperUser'
)
BEGIN
    DECLARE @var8 sysname;
    SELECT @var8 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Channels]') AND [c].[name] = N'TenantId');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Channels] DROP CONSTRAINT [' + @var8 + '];');
    ALTER TABLE [Channels] ALTER COLUMN [TenantId] uniqueidentifier NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240229113734_CreateSuperUser'
)
BEGIN
    DECLARE @var9 sysname;
    SELECT @var9 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ChannelOrigins]') AND [c].[name] = N'TenantId');
    IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [ChannelOrigins] DROP CONSTRAINT [' + @var9 + '];');
    ALTER TABLE [ChannelOrigins] ALTER COLUMN [TenantId] uniqueidentifier NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240229113734_CreateSuperUser'
)
BEGIN
    DECLARE @var10 sysname;
    SELECT @var10 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ChannelDetails]') AND [c].[name] = N'TenantId');
    IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [ChannelDetails] DROP CONSTRAINT [' + @var10 + '];');
    ALTER TABLE [ChannelDetails] ALTER COLUMN [TenantId] uniqueidentifier NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240229113734_CreateSuperUser'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240229113734_CreateSuperUser', N'8.0.1');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240229114022_TenantUserOptional'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240229114022_TenantUserOptional', N'8.0.1');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240229114638_TenantDefault'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Tenants]'))
        SET IDENTITY_INSERT [Tenants] ON;
    EXEC(N'INSERT INTO [Tenants] ([Id], [Name])
    VALUES (''ec576c36-9da4-4d2c-821e-7888f0b4e8a9'', N''General'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Tenants]'))
        SET IDENTITY_INSERT [Tenants] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240229114638_TenantDefault'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240229114638_TenantDefault', N'8.0.1');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240306152354_AddWorkerClass'
)
BEGIN
    ALTER TABLE [Usuarios] DROP CONSTRAINT [FK_Usuarios_Tenants_TenantId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240306152354_AddWorkerClass'
)
BEGIN
    DECLARE @var11 sysname;
    SELECT @var11 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Usuarios]') AND [c].[name] = N'TenantId');
    IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [Usuarios] DROP CONSTRAINT [' + @var11 + '];');
    ALTER TABLE [Usuarios] ALTER COLUMN [TenantId] uniqueidentifier NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240306152354_AddWorkerClass'
)
BEGIN
    CREATE TABLE [Workers] (
        [Id] int NOT NULL IDENTITY,
        [Ip] nvarchar(max) NULL,
        [Port] nvarchar(max) NULL,
        [TenantId] uniqueidentifier NOT NULL,
        [Status] nvarchar(max) NULL,
        CONSTRAINT [PK_Workers] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240306152354_AddWorkerClass'
)
BEGIN
    ALTER TABLE [Usuarios] ADD CONSTRAINT [FK_Usuarios_Tenants_TenantId] FOREIGN KEY ([TenantId]) REFERENCES [Tenants] ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240306152354_AddWorkerClass'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240306152354_AddWorkerClass', N'8.0.1');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240308144304_AddMessageProviderAndContacts'
)
BEGIN
    CREATE TABLE [MessageProviders] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_MessageProviders] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240308144304_AddMessageProviderAndContacts'
)
BEGIN
    CREATE TABLE [ContactsTenants] (
        [Id] int NOT NULL IDENTITY,
        [TenantId] uniqueidentifier NULL,
        [MessageProviderId] int NOT NULL,
        [Number] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_ContactsTenants] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ContactsTenants_MessageProviders_MessageProviderId] FOREIGN KEY ([MessageProviderId]) REFERENCES [MessageProviders] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ContactsTenants_Tenants_TenantId] FOREIGN KEY ([TenantId]) REFERENCES [Tenants] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240308144304_AddMessageProviderAndContacts'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[MessageProviders]'))
        SET IDENTITY_INSERT [MessageProviders] ON;
    EXEC(N'INSERT INTO [MessageProviders] ([Id], [Name])
    VALUES (1, N''Telegram''),
    (2, N''Whatsapp'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[MessageProviders]'))
        SET IDENTITY_INSERT [MessageProviders] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240308144304_AddMessageProviderAndContacts'
)
BEGIN
    CREATE INDEX [IX_ContactsTenants_MessageProviderId] ON [ContactsTenants] ([MessageProviderId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240308144304_AddMessageProviderAndContacts'
)
BEGIN
    CREATE INDEX [IX_ContactsTenants_TenantId] ON [ContactsTenants] ([TenantId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240308144304_AddMessageProviderAndContacts'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240308144304_AddMessageProviderAndContacts', N'8.0.1');
END;
GO

COMMIT;
GO

