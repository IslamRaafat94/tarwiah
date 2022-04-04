CREATE TABLE [dbo].[FeedbackQuestionAnswers]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[FeedbackId] [int] NOT NULL,
[QuestionId] [int] NOT NULL,
[Value] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[FeedbackQuestionAnswers] ADD CONSTRAINT [PK_FeedbackQuestionAnswers] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[FeedbackQuestionAnswers] ADD CONSTRAINT [FK_FeedbackQuestionAnswers_FeedbackQuestions] FOREIGN KEY ([QuestionId]) REFERENCES [dbo].[FeedbackQuestions] ([Id])
GO
ALTER TABLE [dbo].[FeedbackQuestionAnswers] ADD CONSTRAINT [FK_FeedbackQuestionAnswers_Feedbacks] FOREIGN KEY ([FeedbackId]) REFERENCES [dbo].[Feedbacks] ([Id])
GO
