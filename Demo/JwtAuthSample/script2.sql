IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190526052154_InitialCreate')
BEGIN
    CREATE TABLE [Categorys] (
        [Id] nvarchar(50) NOT NULL,
        [CategoryName] nvarchar(50) NULL,
        CONSTRAINT [PK_Categorys] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190526052154_InitialCreate')
BEGIN
    CREATE TABLE [Products] (
        [Id] nvarchar(50) NOT NULL,
        [Name] nvarchar(100) NOT NULL,
        [Price] float NOT NULL,
        [CategoryId] nvarchar(50) NOT NULL,
        CONSTRAINT [PK_Products] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Products_Categorys_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categorys] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190526052154_InitialCreate')
BEGIN
    CREATE INDEX [IX_Products_CategoryId] ON [Products] ([CategoryId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190526052154_InitialCreate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190526052154_InitialCreate', N'2.2.4-servicing-10062');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190526223623_InitialCreate2')
BEGIN
    CREATE TABLE [Book] (
        [Id] nvarchar(50) NOT NULL,
        [ISBN] nvarchar(max) NULL,
        CONSTRAINT [PK_Book] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Book_Products_Id] FOREIGN KEY ([Id]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190526223623_InitialCreate2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190526223623_InitialCreate2', N'2.2.4-servicing-10062');
END;

GO

