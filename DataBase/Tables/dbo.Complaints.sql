CREATE TABLE [dbo].[Complaints]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Issuer_Name] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Issuer_MobileNumber] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Description] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[AssetId] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[SubCategoryId] [int] NOT NULL,
[MantinanceArea] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[AgetLanguage] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[AgetOS] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[AgentLocation] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Coordintes] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[DefaultAssetId] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[IsSyncedToCCB] [bit] NOT NULL CONSTRAINT [DF_Complaints_IsSyncedToCCB] DEFAULT ((0)),
[CreationDate] [datetime] NOT NULL CONSTRAINT [DF_Complaints_CreationDate] DEFAULT (getdate())
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Complaints] ADD CONSTRAINT [PK_Complaints] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Complaints] ADD CONSTRAINT [FK_Complaints_SubCategories] FOREIGN KEY ([SubCategoryId]) REFERENCES [dbo].[CategoryItems] ([Id])
GO
