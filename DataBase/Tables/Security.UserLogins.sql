CREATE TABLE [Security].[UserLogins]
(
[LoginProvider] [nvarchar] (450) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ProviderKey] [nvarchar] (450) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ProviderDisplayName] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[UserId] [bigint] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [Security].[UserLogins] ADD CONSTRAINT [PK_UserLogins] PRIMARY KEY CLUSTERED  ([ProviderKey], [LoginProvider]) ON [PRIMARY]
GO
ALTER TABLE [Security].[UserLogins] ADD CONSTRAINT [AK_UserLogins_LoginProvider_ProviderKey] UNIQUE NONCLUSTERED  ([LoginProvider], [ProviderKey]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_UserLogins_UserId] ON [Security].[UserLogins] ([UserId]) ON [PRIMARY]
GO
ALTER TABLE [Security].[UserLogins] ADD CONSTRAINT [FK_UserLogins_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Security].[Users] ([Id]) ON DELETE CASCADE
GO
