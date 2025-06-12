DROP TABLE [dbo].[ragTemplateFactor]
GO
DROP TABLE [dbo].[ragTemplate]
GO
DROP TABLE [dbo].[ragStatus]
GO
DROP TABLE [dbo].[ragFactor]
GO

CREATE TABLE [dbo].[ragTemplateFactor](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[templateId] [bigint] NOT NULL,
	[factorId] [bigint] NOT NULL,
 CONSTRAINT [PK_ragTemplateFactor] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ragTemplate](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [nchar](50) NOT NULL,
	[period] [datetime] NOT NULL,
 CONSTRAINT [PK_ragTemplate] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ragStatus](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[projectId] [bigint] NOT NULL,
	[userId] [bigint] NULL,
	[factorId] [bigint] NOT NULL,
	[status] [tinyint] NULL,
	[comment] [varchar](max) NULL,
	[date] [datetime] NULL,
	[templateId] [bigint] NULL,
 CONSTRAINT [PK_ragStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[ragFactor](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [nchar](50) NOT NULL,
	[description] [varchar](max) NOT NULL,
	[active] [bit] NOT NULL,
 CONSTRAINT [PK_ragFactor] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[ragTemplateFactor]  WITH CHECK ADD  CONSTRAINT [FK_ragTemplateFactor_ragFactor] FOREIGN KEY([factorId])
REFERENCES [dbo].[ragFactor] ([Id])
GO

ALTER TABLE [dbo].[ragTemplateFactor] CHECK CONSTRAINT [FK_ragTemplateFactor_ragFactor]
GO

ALTER TABLE [dbo].[ragTemplateFactor]  WITH CHECK ADD  CONSTRAINT [FK_ragTemplateFactor_ragTemplate] FOREIGN KEY([templateId])
REFERENCES [dbo].[ragTemplate] ([Id])
GO

ALTER TABLE [dbo].[ragTemplateFactor] CHECK CONSTRAINT [FK_ragTemplateFactor_ragTemplate]
GO

ALTER TABLE [dbo].[ragTemplate] ADD  CONSTRAINT [DF_ragTemplate_created]  DEFAULT (getdate()) FOR [period]
GO

ALTER TABLE [dbo].[ragStatus]  WITH CHECK ADD  CONSTRAINT [FK_ragStatus_Projects] FOREIGN KEY([projectId])
REFERENCES [dbo].[Projects] ([Id])
GO

ALTER TABLE [dbo].[ragStatus] CHECK CONSTRAINT [FK_ragStatus_Projects]
GO

ALTER TABLE [dbo].[ragStatus]  WITH CHECK ADD  CONSTRAINT [FK_ragStatus_ragFactor] FOREIGN KEY([factorId])
REFERENCES [dbo].[ragFactor] ([Id])
GO

ALTER TABLE [dbo].[ragStatus] CHECK CONSTRAINT [FK_ragStatus_ragFactor]
GO

ALTER TABLE [dbo].[ragStatus]  WITH CHECK ADD  CONSTRAINT [FK_ragStatus_ragTemplate] FOREIGN KEY([templateId])
REFERENCES [dbo].[ragTemplate] ([Id])
GO

ALTER TABLE [dbo].[ragStatus] CHECK CONSTRAINT [FK_ragStatus_ragTemplate]
GO

ALTER TABLE [dbo].[ragStatus]  WITH CHECK ADD  CONSTRAINT [FK_ragStatus_Users] FOREIGN KEY([userId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[ragStatus] CHECK CONSTRAINT [FK_ragStatus_Users]
GO

ALTER TABLE [dbo].[ragFactor] ADD  CONSTRAINT [DF_ragFactor_active]  DEFAULT ((1)) FOR [active]
GO

ALTER TABLE RagFactor ADD Objetive varchar(MAX);