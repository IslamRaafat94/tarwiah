CREATE TABLE [dbo].[Feedbacks]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Comment] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CreationDate] [datetime] NOT NULL CONSTRAINT [DF_Feedbacks_CreationDate] DEFAULT (getdate())
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Feedbacks] ADD CONSTRAINT [PK_Answers] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
