SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[feedbackUser](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[userId] [bigint] NOT NULL,
	[period] [date] NOT NULL,
 CONSTRAINT [PK_feedbackUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[feedbackStandarType](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[standarId] [bigint] NOT NULL,
	[name] [nchar](50) NOT NULL,
	[description] [varchar](max) NOT NULL,
	[active] [bit] NOT NULL,
 CONSTRAINT [PK_feedbackStandarType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[feedbackStandar](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [nchar](100) NOT NULL,
	[detail] [varchar](max) NOT NULL,
	[active] [bit] NOT NULL,
 CONSTRAINT [PK_feedbackStandar] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[feedbackPeriod](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[userId] [bigint] NOT NULL,
	[itemId] [bigint] NOT NULL,
	[evaluationType] [bigint] NOT NULL,
	[period] [date] NOT NULL,
 CONSTRAINT [PK_feedbackPeriod] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[feedbackItem](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [nchar](100) NOT NULL,
	[parentItemId] [bigint] NULL,
	[standarId] [bigint] NULL,
	[improvementId] [bigint] NULL,
	[active] [bit] NOT NULL,
 CONSTRAINT [PK_feedbackItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[feedbackImprovement](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [nchar](100) NOT NULL,
	[detail] [varchar](max) NOT NULL,
	[description] [varchar](max) NOT NULL,
	[active] [bit] NOT NULL,
 CONSTRAINT [PK_feedbackImprovement] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[feedbackComment](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[feedbackUserId] [bigint] NOT NULL,
	[userId] [bigint] NOT NULL,
	[comment] [varchar](max) NOT NULL,
	[date] [datetime] NOT NULL,
 CONSTRAINT [PK_feedbackComment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


ALTER TABLE [dbo].[feedbackUser] ADD  CONSTRAINT [DF_feedbackUser_period]  DEFAULT (getdate()) FOR [period]
GO

ALTER TABLE [dbo].[feedbackUser]  WITH CHECK ADD  CONSTRAINT [FK_feedbackUser_Users] FOREIGN KEY([userId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[feedbackUser] CHECK CONSTRAINT [FK_feedbackUser_Users]
GO

ALTER TABLE [dbo].[feedbackStandarType] ADD  CONSTRAINT [DF_feedbackStandarType_active]  DEFAULT ((1)) FOR [active]
GO

ALTER TABLE [dbo].[feedbackStandarType]  WITH CHECK ADD  CONSTRAINT [FK_feedbackStandarType_feedbackStandar] FOREIGN KEY([standarId])
REFERENCES [dbo].[feedbackStandar] ([Id])
GO

ALTER TABLE [dbo].[feedbackStandarType] CHECK CONSTRAINT [FK_feedbackStandarType_feedbackStandar]
GO

ALTER TABLE [dbo].[feedbackStandar] ADD  CONSTRAINT [DF_feedbackStandar_active]  DEFAULT ((1)) FOR [active]
GO

ALTER TABLE [dbo].[feedbackPeriod]  WITH CHECK ADD  CONSTRAINT [FK_feedbackPeriod_feedbackItem] FOREIGN KEY([itemId])
REFERENCES [dbo].[feedbackItem] ([Id])
GO

ALTER TABLE [dbo].[feedbackPeriod] CHECK CONSTRAINT [FK_feedbackPeriod_feedbackItem]
GO

ALTER TABLE [dbo].[feedbackPeriod]  WITH CHECK ADD  CONSTRAINT [FK_feedbackPeriod_feedbackStandarType] FOREIGN KEY([evaluationType])
REFERENCES [dbo].[feedbackStandarType] ([Id])
GO

ALTER TABLE [dbo].[feedbackPeriod] CHECK CONSTRAINT [FK_feedbackPeriod_feedbackStandarType]
GO

ALTER TABLE [dbo].[feedbackPeriod]  WITH CHECK ADD  CONSTRAINT [FK_feedbackPeriod_Users] FOREIGN KEY([userId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[feedbackPeriod] CHECK CONSTRAINT [FK_feedbackPeriod_Users]
GO

ALTER TABLE [dbo].[feedbackItem] ADD  CONSTRAINT [DF_feedbackItem_active]  DEFAULT ((1)) FOR [active]
GO

ALTER TABLE [dbo].[feedbackItem]  WITH CHECK ADD  CONSTRAINT [FK_feedbackItem_feedbackImprovement] FOREIGN KEY([improvementId])
REFERENCES [dbo].[feedbackImprovement] ([Id])
GO

ALTER TABLE [dbo].[feedbackItem] CHECK CONSTRAINT [FK_feedbackItem_feedbackImprovement]
GO

ALTER TABLE [dbo].[feedbackItem]  WITH CHECK ADD  CONSTRAINT [FK_feedbackItem_feedbackItem] FOREIGN KEY([parentItemId])
REFERENCES [dbo].[feedbackItem] ([Id])
GO

ALTER TABLE [dbo].[feedbackItem] CHECK CONSTRAINT [FK_feedbackItem_feedbackItem]
GO

ALTER TABLE [dbo].[feedbackItem]  WITH CHECK ADD  CONSTRAINT [FK_feedbackItem_feedbackStandar] FOREIGN KEY([standarId])
REFERENCES [dbo].[feedbackStandar] ([Id])
GO

ALTER TABLE [dbo].[feedbackItem] CHECK CONSTRAINT [FK_feedbackItem_feedbackStandar]
GO

ALTER TABLE [dbo].[feedbackImprovement] ADD  CONSTRAINT [DF_feedbackImprovement_active]  DEFAULT ((1)) FOR [active]
GO

ALTER TABLE [dbo].[feedbackComment] ADD  CONSTRAINT [DF_feedbackComment_date]  DEFAULT (getdate()) FOR [date]
GO

ALTER TABLE [dbo].[feedbackComment]  WITH CHECK ADD  CONSTRAINT [FK_feedbackComment_feedbackUser] FOREIGN KEY([feedbackUserId])
REFERENCES [dbo].[feedbackUser] ([Id])
GO

ALTER TABLE [dbo].[feedbackComment] CHECK CONSTRAINT [FK_feedbackComment_feedbackUser]
GO

ALTER TABLE [dbo].[feedbackComment]  WITH CHECK ADD  CONSTRAINT [FK_feedbackComment_Users] FOREIGN KEY([userId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[feedbackComment] CHECK CONSTRAINT [FK_feedbackComment_Users]
GO
