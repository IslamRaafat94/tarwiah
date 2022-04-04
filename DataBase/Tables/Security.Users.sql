CREATE TABLE [Security].[Users]
(
[Id] [bigint] NOT NULL IDENTITY(1, 1),
[UserName] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[NormalizedUserName] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Email] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[NormalizedEmail] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[EmailConfirmed] [bit] NOT NULL,
[PasswordHash] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[SecurityStamp] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ConcurrencyStamp] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PhoneNumber] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PhoneNumberConfirmed] [bit] NOT NULL,
[TwoFactorEnabled] [bit] NOT NULL,
[LockoutEnd] [datetimeoffset] NULL,
[LockoutEnabled] [bit] NOT NULL,
[AccessFailedCount] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [Security].[Users] ADD CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [EmailIndex] ON [Security].[Users] ([NormalizedEmail]) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [Security].[Users] ([NormalizedUserName]) WHERE ([NormalizedUserName] IS NOT NULL) ON [PRIMARY]
GO
