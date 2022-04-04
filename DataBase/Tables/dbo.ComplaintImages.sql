CREATE TABLE [dbo].[ComplaintImages]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[ComplaintId] [int] NOT NULL,
[LocalName] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[EAM_Path] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ComplaintImages] ADD CONSTRAINT [PK_ComplaintImages] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ComplaintImages] ADD CONSTRAINT [FK_ComplaintImages_Complaints] FOREIGN KEY ([ComplaintId]) REFERENCES [dbo].[Complaints] ([Id])
GO
