DROP TABLE [dbo].[feedbackPeriod];
DROP TABLE [dbo].[feedbackItem];
DROP TABLE [dbo].[feedbackComment];
DROP TABLE [dbo].[feedbackUser];
DROP TABLE [dbo].[feedbackStandarType];
DROP TABLE [dbo].[feedbackStandar];

CREATE TABLE [dbo].[feedbackUser](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[userId] [bigint] NOT NULL,
	[period] [date] NOT NULL,
	[active] [bit] NOT NULL,
	[creation] [datetime] NOT NULL,
	[modification] [datetime] NULL,
	[deleted] [datetime] NULL,
 CONSTRAINT [PK_feedbackUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[feedbackStandarType](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[feedbackStandarId] [bigint] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[description] [nvarchar](max) NOT NULL,
	[active] [bit] NOT NULL,
 CONSTRAINT [PK_feedbackStandarType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[feedbackStandar](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[description] [nvarchar](max) NOT NULL,
	[active] [bit] NOT NULL,
 CONSTRAINT [PK_feedbackStandar] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[feedbackPeriod](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[feedbackUserId] [bigint] NOT NULL,
	[feedbackItemId] [bigint] NOT NULL,
	[feedbackStandarTypeId] [bigint] NULL,
 CONSTRAINT [PK_feedbackPeriod] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[feedbackItem](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[feedbackStandarId] [bigint] NULL,
	[name] [nvarchar](100) NOT NULL,
	[active] [bit] NOT NULL,
 CONSTRAINT [PK_feedbackItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[feedbackImprovementUser](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[feedbackImprovementId] [bigint] NOT NULL,
	[feedbackUserId] [bigint] NOT NULL,
	[description] [nvarchar](max) NULL,
 CONSTRAINT [PK_feedbackImprovementUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[feedbackImprovement](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [nchar](100) NOT NULL,
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
	[comment] [nvarchar](max) NOT NULL,
	[date] [datetime] NOT NULL,
 CONSTRAINT [PK_feedbackComment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[feedbackUser] ADD  CONSTRAINT [DF_feedbackUser_period]  DEFAULT (getdate()) FOR [period]
GO

ALTER TABLE [dbo].[feedbackUser] ADD  CONSTRAINT [DF_feedbackUser_active]  DEFAULT ((1)) FOR [active]
GO

ALTER TABLE [dbo].[feedbackUser] ADD  CONSTRAINT [DF_feedbackUser_creation]  DEFAULT (getdate()) FOR [creation]
GO

ALTER TABLE [dbo].[feedbackUser]  WITH CHECK ADD  CONSTRAINT [FK_feedbackUser_Users] FOREIGN KEY([userId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[feedbackUser] CHECK CONSTRAINT [FK_feedbackUser_Users]
GO

ALTER TABLE [dbo].[feedbackStandarType] ADD  CONSTRAINT [DF_feedbackStandarType_active]  DEFAULT ((1)) FOR [active]
GO

ALTER TABLE [dbo].[feedbackStandarType]  WITH CHECK ADD  CONSTRAINT [FK_feedbackStandarType_feedbackStandar] FOREIGN KEY([feedbackStandarId])
REFERENCES [dbo].[feedbackStandar] ([Id])
GO

ALTER TABLE [dbo].[feedbackStandarType] CHECK CONSTRAINT [FK_feedbackStandarType_feedbackStandar]
GO

ALTER TABLE [dbo].[feedbackStandar] ADD  CONSTRAINT [DF_feedbackStandar_active]  DEFAULT ((1)) FOR [active]
GO

ALTER TABLE [dbo].[feedbackPeriod]  WITH CHECK ADD  CONSTRAINT [FK_feedbackPeriod_feedbackItem] FOREIGN KEY([feedbackItemId])
REFERENCES [dbo].[feedbackItem] ([Id])
GO

ALTER TABLE [dbo].[feedbackPeriod] CHECK CONSTRAINT [FK_feedbackPeriod_feedbackItem]
GO

ALTER TABLE [dbo].[feedbackPeriod]  WITH CHECK ADD  CONSTRAINT [FK_feedbackPeriod_feedbackStandarType] FOREIGN KEY([feedbackStandarTypeId])
REFERENCES [dbo].[feedbackStandarType] ([Id])
GO

ALTER TABLE [dbo].[feedbackPeriod] CHECK CONSTRAINT [FK_feedbackPeriod_feedbackStandarType]
GO

ALTER TABLE [dbo].[feedbackPeriod]  WITH CHECK ADD  CONSTRAINT [FK_feedbackPeriod_feedbackUser] FOREIGN KEY([feedbackUserId])
REFERENCES [dbo].[feedbackUser] ([Id])
GO

ALTER TABLE [dbo].[feedbackPeriod] CHECK CONSTRAINT [FK_feedbackPeriod_feedbackUser]
GO

ALTER TABLE [dbo].[feedbackItem] ADD  CONSTRAINT [DF_feedbackItem_active]  DEFAULT ((1)) FOR [active]
GO

ALTER TABLE [dbo].[feedbackItem]  WITH CHECK ADD  CONSTRAINT [FK_feedbackItem_feedbackStandar] FOREIGN KEY([feedbackStandarId])
REFERENCES [dbo].[feedbackStandar] ([Id])
GO

ALTER TABLE [dbo].[feedbackItem] CHECK CONSTRAINT [FK_feedbackItem_feedbackStandar]
GO

ALTER TABLE [dbo].[feedbackImprovementUser]  WITH CHECK ADD  CONSTRAINT [FK_feedbackImprovementUser_feedbackImprovement] FOREIGN KEY([feedbackImprovementId])
REFERENCES [dbo].[feedbackImprovement] ([Id])
GO

ALTER TABLE [dbo].[feedbackImprovementUser] CHECK CONSTRAINT [FK_feedbackImprovementUser_feedbackImprovement]
GO

ALTER TABLE [dbo].[feedbackImprovementUser]  WITH CHECK ADD  CONSTRAINT [FK_feedbackImprovementUser_feedbackUser] FOREIGN KEY([feedbackUserId])
REFERENCES [dbo].[feedbackUser] ([Id])
GO

ALTER TABLE [dbo].[feedbackImprovementUser] CHECK CONSTRAINT [FK_feedbackImprovementUser_feedbackUser]
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

ALTER TABLE [dbo].[feedbackImprovement] -- ta bien?
DROP COLUMN detail;
GO