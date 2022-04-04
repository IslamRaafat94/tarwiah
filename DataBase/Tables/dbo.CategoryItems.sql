CREATE TABLE [dbo].[CategoryItems]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[CategoryId] [int] NOT NULL,
[Code] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ServerName] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Name_En] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Name_Ar] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Name_Fr] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Name_Fa] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Name_id] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Name_Ur] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Name_Tr] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[IsActive] [bit] NOT NULL,
[IsDeleted] [bit] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CategoryItems] ADD CONSTRAINT [PK_SubCategories] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CategoryItems] ADD CONSTRAINT [IX_SubCategories] UNIQUE NONCLUSTERED  ([Code]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CategoryItems] ADD CONSTRAINT [FK_SubCategories_Categories] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Categories] ([Id])
GO
