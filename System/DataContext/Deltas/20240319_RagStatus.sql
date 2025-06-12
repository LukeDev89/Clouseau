DROP TABLE [dbo].[ragStatus];

CREATE TABLE [dbo].[ragStatus](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[projectId] [bigint] NOT NULL,
	[userId] [bigint] NULL,
	[factorId] [bigint] NOT NULL,
	[status] [tinyint] NULL,
	[comment] [varchar](max) NULL,
	[date] [datetime] NULL,
 CONSTRAINT [PK_ragStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
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