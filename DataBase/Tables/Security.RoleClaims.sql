CREATE TABLE [Security].[RoleClaims]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[RoleId] [bigint] NOT NULL,
[ClaimType] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ClaimValue] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [Security].[RoleClaims] ADD CONSTRAINT [PK_RoleClaims] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_RoleClaims_RoleId] ON [Security].[RoleClaims] ([RoleId]) ON [PRIMARY]
GO
ALTER TABLE [Security].[RoleClaims] ADD CONSTRAINT [FK_RoleClaims_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Security].[Roles] ([Id]) ON DELETE CASCADE
GO
