CREATE TABLE [Security].[UserRoles]
(
[UserId] [bigint] NOT NULL,
[RoleId] [bigint] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [Security].[UserRoles] ADD CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED  ([UserId], [RoleId]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_UserRoles_RoleId] ON [Security].[UserRoles] ([RoleId]) ON [PRIMARY]
GO
ALTER TABLE [Security].[UserRoles] ADD CONSTRAINT [FK_UserRoles_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Security].[Roles] ([Id]) ON DELETE CASCADE
GO
ALTER TABLE [Security].[UserRoles] ADD CONSTRAINT [FK_UserRoles_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Security].[Users] ([Id]) ON DELETE CASCADE
GO
