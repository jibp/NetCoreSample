IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Categorys] (
    [Id] nvarchar(50) NOT NULL,
    [CategoryName] nvarchar(50) NULL,
    CONSTRAINT [PK_Categorys] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Products] (
    [Id] nvarchar(50) NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    [Price] float NOT NULL,
    [CategoryId] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Products_Categorys_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categorys] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_Products_CategoryId] ON [Products] ([CategoryId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190526052154_InitialCreate', N'2.2.4-servicing-10062');

GO

CREATE TABLE [Book] (
    [Id] nvarchar(50) NOT NULL,
    [ISBN] nvarchar(max) NULL,
    CONSTRAINT [PK_Book] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Book_Products_Id] FOREIGN KEY ([Id]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190526223623_InitialCreate2', N'2.2.4-servicing-10062');

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190531100737_addsql4', N'2.2.4-servicing-10062');

GO

CREATE TABLE [Posts] (
    [PostId] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NULL,
    CONSTRAINT [PK_Posts] PRIMARY KEY ([PostId])
);

GO

CREATE TABLE [Tags] (
    [TagId] int NOT NULL IDENTITY,
    [Text] nvarchar(max) NULL,
    CONSTRAINT [PK_Tags] PRIMARY KEY ([TagId])
);

GO

CREATE TABLE [PostTags] (
    [PostId] int NOT NULL,
    [TagId] int NOT NULL,
    CONSTRAINT [PK_PostTags] PRIMARY KEY ([PostId], [TagId]),
    CONSTRAINT [FK_PostTags_Posts_PostId] FOREIGN KEY ([PostId]) REFERENCES [Posts] ([PostId]) ON DELETE CASCADE,
    CONSTRAINT [FK_PostTags_Tags_TagId] FOREIGN KEY ([TagId]) REFERENCES [Tags] ([TagId]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_PostTags_TagId] ON [PostTags] ([TagId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190605214525_2.2.2.190613_201906052', N'2.2.4-servicing-10062');

GO

