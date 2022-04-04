CREATE TABLE [dbo].[FeedbackQuestions]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[IsActive] [bit] NOT NULL,
[Name_En] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Name_Ar] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Name_Fr] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Name_Fa] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Name_id] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Name_Ur] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Name_Tr] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[FeedbackQuestions] ADD CONSTRAINT [PK_Questions] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
