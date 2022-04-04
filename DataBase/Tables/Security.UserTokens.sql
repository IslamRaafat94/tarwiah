CREATE TABLE [Security].[UserTokens]
(
[UserId] [bigint] NOT NULL,
[LoginProvider] [nvarchar] (450) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Name] [nvarchar] (450) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Value] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [Security].[UserTokens] ADD CONSTRAINT [PK_UserTokens] PRIMARY KEY CLUSTERED  ([UserId], [LoginProvider], [Name]) ON [PRIMARY]
GO
ALTER TABLE [Security].[UserTokens] ADD CONSTRAINT [FK_UserTokens_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Security].[Users] ([Id]) ON DELETE CASCADE
GO
