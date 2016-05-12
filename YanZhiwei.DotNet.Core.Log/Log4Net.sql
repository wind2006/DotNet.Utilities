
CREATE TABLE [dbo].[Log4net](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Level] [nvarchar](50) NULL,
	[Logger] [nvarchar](255) NULL,
	[Host] [nvarchar](50) NULL,
	[Date] [datetime] NULL,
	[Thread] [nvarchar](255) NULL,
	[Message] [nvarchar](max) NULL,
	[Exception] [nvarchar](max) NULL,
 CONSTRAINT [PK_Log4net] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


