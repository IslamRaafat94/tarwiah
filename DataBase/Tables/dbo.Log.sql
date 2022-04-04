CREATE TABLE [dbo].[Log]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[timestamp] [datetime] NOT NULL,
[level] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[logger] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[exceptionMessage] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[userid] [int] NULL,
[exception] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[stacktrace] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[message] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Log] ADD CONSTRAINT [PK_ExceptionLog] PRIMARY KEY CLUSTERED  ([id]) ON [PRIMARY]
GO
