CREATE TABLE [dbo].[Categories]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
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
ALTER TABLE [dbo].[Categories] ADD CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
