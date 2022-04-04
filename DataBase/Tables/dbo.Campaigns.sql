CREATE TABLE [dbo].[Campaigns]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Type] [char] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Name_En] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Name_Ar] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Name_Fr] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Name_Fa] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Name_id] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Name_Ur] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Name_Tr] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[IsActive] [bit] NOT NULL,
[IsDeleted] [bit] NOT NULL,
[Longitude] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Latitude] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Campaigns] ADD CONSTRAINT [PK_Hamalat] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
