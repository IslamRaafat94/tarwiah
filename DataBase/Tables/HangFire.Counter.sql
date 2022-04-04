CREATE TABLE [HangFire].[Counter]
(
[Key] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Value] [int] NOT NULL,
[ExpireAt] [datetime] NULL
) ON [PRIMARY]
GO
CREATE CLUSTERED INDEX [CX_HangFire_Counter] ON [HangFire].[Counter] ([Key]) ON [PRIMARY]
GO
