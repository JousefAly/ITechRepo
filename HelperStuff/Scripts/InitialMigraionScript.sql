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

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [Discriminator] nvarchar(max) NOT NULL,
    [FirstName] nvarchar(max) NULL,
    [LastName] nvarchar(max) NULL,
    [ProfilePicture] varbinary(max) NULL,
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
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Categories] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [ImageUrl] nvarchar(max) NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Jobs] (
    [Id] nvarchar(450) NOT NULL,
    [JobTitle] nvarchar(max) NULL,
    [Available] bit NOT NULL,
    CONSTRAINT [PK_Jobs] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Seeds] (
    [Id] int NOT NULL IDENTITY,
    [NameOfSeedType] nvarchar(max) NULL,
    [DesiredSeed] int NOT NULL,
    [Seeded] bit NOT NULL,
    [SeedAttempts] int NOT NULL,
    CONSTRAINT [PK_Seeds] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Customers] (
    [Id] nvarchar(450) NOT NULL,
    [UserId] nvarchar(450) NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Customers_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE SET NULL
);
GO

CREATE TABLE [Notifications] (
    [Id] nvarchar(450) NOT NULL,
    [Message] nvarchar(max) NULL,
    [SentTime] datetime2 NOT NULL,
    [Checked] bit NOT NULL,
    [LastCheckTime] datetime2 NOT NULL,
    [ReceiverId] nvarchar(450) NULL,
    [SenderId] nvarchar(450) NULL,
    CONSTRAINT [PK_Notifications] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Notifications_AspNetUsers_ReceiverId] FOREIGN KEY ([ReceiverId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Notifications_AspNetUsers_SenderId] FOREIGN KEY ([SenderId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Orders] (
    [OrderId] int NOT NULL IDENTITY,
    [Accepted] bit NOT NULL,
    [OrderHandeled] datetime2 NOT NULL,
    [AddressLine] nvarchar(100) NOT NULL,
    [City] nvarchar(50) NOT NULL,
    [Governorate] nvarchar(50) NOT NULL,
    [Country] nvarchar(50) NOT NULL,
    [PhoneNumber] nvarchar(25) NOT NULL,
    [OrderTotal] decimal(18,2) NOT NULL,
    [OrderPlaced] datetime2 NOT NULL,
    [UserId] nvarchar(450) NULL,
    [HandlerId] nvarchar(450) NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY ([OrderId]),
    CONSTRAINT [FK_Orders_AspNetUsers_HandlerId] FOREIGN KEY ([HandlerId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Orders_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Sellers] (
    [Id] nvarchar(450) NOT NULL,
    [UserId] nvarchar(450) NULL,
    [Activated] bit NOT NULL,
    [Address] nvarchar(max) NULL,
    CONSTRAINT [PK_Sellers] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Sellers_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE SET NULL
);
GO

CREATE TABLE [JobApplications] (
    [Id] nvarchar(450) NOT NULL,
    [Accepted] bit NOT NULL,
    [JobId] nvarchar(450) NULL,
    [ApplicantId] nvarchar(450) NULL,
    CONSTRAINT [PK_JobApplications] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_JobApplications_AspNetUsers_ApplicantId] FOREIGN KEY ([ApplicantId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_JobApplications_Jobs_JobId] FOREIGN KEY ([JobId]) REFERENCES [Jobs] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Products] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NULL,
    [Activated] bit NOT NULL,
    [ITSIN] nvarchar(max) NULL,
    [Brand] nvarchar(max) NULL,
    [ShortDescription] nvarchar(max) NULL,
    [Price] decimal(18,2) NOT NULL,
    [PriceAfterDiscount] decimal(18,2) NOT NULL,
    [DiscountPercentage] decimal(18,2) NOT NULL,
    [LaunchTime] datetime2 NOT NULL,
    [Stock] int NOT NULL,
    [CategoryId] int NULL,
    [SellerId] nvarchar(450) NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Products_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Products_Sellers_SellerId] FOREIGN KEY ([SellerId]) REFERENCES [Sellers] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [AppUserProduct] (
    [SavedProductsId] int NOT NULL,
    [SavingUsersId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AppUserProduct] PRIMARY KEY ([SavedProductsId], [SavingUsersId]),
    CONSTRAINT [FK_AppUserProduct_AspNetUsers_SavingUsersId] FOREIGN KEY ([SavingUsersId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AppUserProduct_Products_SavedProductsId] FOREIGN KEY ([SavedProductsId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [OrderDetails] (
    [OrderDetailId] int NOT NULL IDENTITY,
    [Amount] int NOT NULL,
    [ProductId] int NOT NULL,
    [OrderId] int NOT NULL,
    CONSTRAINT [PK_OrderDetails] PRIMARY KEY ([OrderDetailId]),
    CONSTRAINT [FK_OrderDetails_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([OrderId]) ON DELETE CASCADE,
    CONSTRAINT [FK_OrderDetails_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [ProductDetails] (
    [Id] int NOT NULL IDENTITY,
    [ITSIN] nvarchar(max) NULL,
    [Title] nvarchar(max) NULL,
    [Content] nvarchar(max) NULL,
    [ProductId] int NOT NULL,
    CONSTRAINT [PK_ProductDetails] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProductDetails_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [ProductImages] (
    [Id] int NOT NULL IDENTITY,
    [ImageNumber] int NOT NULL,
    [ImageUrl] nvarchar(max) NULL,
    [ProductId] int NOT NULL,
    CONSTRAINT [PK_ProductImages] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProductImages_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [ShoppingCartItems] (
    [Id] int NOT NULL IDENTITY,
    [ProductId] int NOT NULL,
    [Amount] int NOT NULL,
    [ShoppingCartId] nvarchar(max) NULL,
    CONSTRAINT [PK_ShoppingCartItems] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ShoppingCartItems_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_AppUserProduct_SavingUsersId] ON [AppUserProduct] ([SavingUsersId]);
GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

CREATE UNIQUE INDEX [IX_Customers_UserId] ON [Customers] ([UserId]) WHERE [UserId] IS NOT NULL;
GO

CREATE INDEX [IX_JobApplications_ApplicantId] ON [JobApplications] ([ApplicantId]);
GO

CREATE INDEX [IX_JobApplications_JobId] ON [JobApplications] ([JobId]);
GO

CREATE INDEX [IX_Notifications_ReceiverId] ON [Notifications] ([ReceiverId]);
GO

CREATE INDEX [IX_Notifications_SenderId] ON [Notifications] ([SenderId]);
GO

CREATE INDEX [IX_OrderDetails_OrderId] ON [OrderDetails] ([OrderId]);
GO

CREATE INDEX [IX_OrderDetails_ProductId] ON [OrderDetails] ([ProductId]);
GO

CREATE INDEX [IX_Orders_HandlerId] ON [Orders] ([HandlerId]);
GO

CREATE INDEX [IX_Orders_UserId] ON [Orders] ([UserId]);
GO

CREATE INDEX [IX_ProductDetails_ProductId] ON [ProductDetails] ([ProductId]);
GO

CREATE INDEX [IX_ProductImages_ProductId] ON [ProductImages] ([ProductId]);
GO

CREATE INDEX [IX_Products_CategoryId] ON [Products] ([CategoryId]);
GO

CREATE INDEX [IX_Products_SellerId] ON [Products] ([SellerId]);
GO

CREATE UNIQUE INDEX [IX_Sellers_UserId] ON [Sellers] ([UserId]) WHERE [UserId] IS NOT NULL;
GO

CREATE INDEX [IX_ShoppingCartItems_ProductId] ON [ShoppingCartItems] ([ProductId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220512113845_InitialMigration', N'5.0.15');
GO

COMMIT;
GO

