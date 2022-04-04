CREATE TABLE [dbo].[Toilets]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Code] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Longitude] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Latitude] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[IsActive] [bit] NOT NULL,
[IsDeleted] [bit] NOT NULL,
[P1] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[P2] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[P3] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Toilets] ADD CONSTRAINT [PK_Assets] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
