CREATE TABLE [Security].[UserClaims]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[UserId] [bigint] NOT NULL,
[ClaimType] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ClaimValue] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [Security].[UserClaims] ADD CONSTRAINT [PK_UserClaims] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_UserClaims_UserId] ON [Security].[UserClaims] ([UserId]) ON [PRIMARY]
GO
ALTER TABLE [Security].[UserClaims] ADD CONSTRAINT [FK_UserClaims_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Security].[Users] ([Id]) ON DELETE CASCADE
GO
