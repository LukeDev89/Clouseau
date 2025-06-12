
GO

/****** Object:  Table [dbo].[ragFactor]    Script Date: 14/03/2024 11:24:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
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

ALTER TABLE [dbo].[ragFactor] ADD  CONSTRAINT [DF_ragFactor_active]  DEFAULT ((1)) FOR [active]
GO



GO

/****** Object:  Table [dbo].[ragStatus]    Script Date: 14/03/2024 11:25:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ragStatus](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[projectId] [bigint] NOT NULL,
	[userId] [bigint] NOT NULL,
	[factorId] [bigint] NOT NULL,
	[status] [tinyint] NOT NULL,
	[comment] [varchar](max) NOT NULL,
	[date] [datetime] NOT NULL,
 CONSTRAINT [PK_ragStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[ragStatus] ADD  CONSTRAINT [DF_ragStatus_date]  DEFAULT (getdate()) FOR [date]
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

ALTER TABLE [dbo].[ragStatus]  WITH CHECK ADD  CONSTRAINT [FK_ragStatus_Users] FOREIGN KEY([userId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[ragStatus] CHECK CONSTRAINT [FK_ragStatus_Users]
GO



GO

/****** Object:  Table [dbo].[ragTemplate]    Script Date: 14/03/2024 11:25:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
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

ALTER TABLE [dbo].[ragTemplate] ADD  CONSTRAINT [DF_ragTemplate_created]  DEFAULT (getdate()) FOR [period]
GO



GO

/****** Object:  Table [dbo].[ragTemplateFactor]    Script Date: 14/03/2024 11:25:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
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


