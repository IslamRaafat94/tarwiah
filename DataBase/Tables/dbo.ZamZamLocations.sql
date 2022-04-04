CREATE TABLE [dbo].[ZamZamLocations]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name_En] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Name_Ar] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Name_Fr] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Name_Fa] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Name_id] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Name_Ur] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Name_Tr] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Longitude] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Latitude] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[IsDeleted] [bit] NOT NULL CONSTRAINT [DF_ZamZamLocations_IsDeleted] DEFAULT ((0))
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ZamZamLocations] ADD CONSTRAINT [PK_ZamZamLocations] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
