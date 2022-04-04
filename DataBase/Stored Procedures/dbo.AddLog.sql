SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
create procedure [dbo].[AddLog] 
(
	@level varchar(max),
	@callSite varchar(max),
	@type varchar(max),
	@exceptionMessage varchar(max),
	@exception varchar(max),
	@stackTrace varchar(max),
	@innerException varchar(max),
	@message varchar(max)
)
as
BEGIN
	INSERT into dbo.Log
(
	[timestamp],
	[Level],
	[logger],
	[exceptionMessage],
	[userid],
	[exception],
	[stacktrace],
	[message]
)
values
(
	GETDATE(),
	@level,
	@callSite,
	@exceptionMessage,
	0,
	@exception,
	@stackTrace,
	@message
	
    
)
END
GO
