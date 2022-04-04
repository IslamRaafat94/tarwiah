CREATE TABLE [Security].[Roles]
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[NormalizedName] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ConcurrencyStamp] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [Security].[Roles] ADD CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [Security].[Roles] ([NormalizedName]) WHERE ([NormalizedName] IS NOT NULL) ON [PRIMARY]
GO
