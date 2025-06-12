DROP TABLE [dbo].[UserNonworkingDays]
GO

CREATE TABLE [dbo].[LicenseType](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [nchar](50) NOT NULL,
	[helperMessage] [nchar](250) NOT NULL,
	[fileRequired] [bit] NOT NULL,
	[consecutiveDays] [int] NOT NULL,
	[maxDaysPerYear] [int] NOT NULL,
 CONSTRAINT [PK_LicenseType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[LicenseType] ADD  CONSTRAINT [DF_LicenseType_requireFile]  DEFAULT ((0)) FOR [fileRequired]
GO

ALTER TABLE [dbo].[LicenseType] ADD  CONSTRAINT [DF_LicenseType_consecutiveDays]  DEFAULT ((0)) FOR [consecutiveDays]
GO

ALTER TABLE [dbo].[LicenseType] ADD  CONSTRAINT [DF_LicenseType_maxDaysPerYear]  DEFAULT ((0)) FOR [maxDaysPerYear]
GO

CREATE TABLE [dbo].[UserNonworkingDays](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[userId] [bigint] NOT NULL,
	[dateFrom] [date] NOT NULL,
	[dateTo] [date] NOT NULL,
	[typeId] [bigint] NOT NULL,
	[comment] [nchar](255) NULL,
	[commentCfo] [nchar](255) NULL,
	[path] [nchar](255) NULL,
	[state] [smallint] NOT NULL,
	[created] [datetime] NOT NULL,
 CONSTRAINT [PK_UserNonworkingDays_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[UserNonworkingDays] ADD  CONSTRAINT [DF_UserNonworkingDays_type_1]  DEFAULT ((1)) FOR [typeId]
GO

ALTER TABLE [dbo].[UserNonworkingDays] ADD  CONSTRAINT [DF_UserNonworkingDays_state]  DEFAULT ((0)) FOR [state]
GO

ALTER TABLE [dbo].[UserNonworkingDays] ADD  CONSTRAINT [DF_UserNonworkingDays_created]  DEFAULT (getdate()) FOR [created]
GO

ALTER TABLE [dbo].[UserNonworkingDays]  WITH CHECK ADD  CONSTRAINT [FK_UserNonworkingDays_LicenseType] FOREIGN KEY([typeId])
REFERENCES [dbo].[LicenseType] ([Id])
GO

ALTER TABLE [dbo].[UserNonworkingDays] CHECK CONSTRAINT [FK_UserNonworkingDays_LicenseType]
GO

ALTER TABLE [dbo].[UserNonworkingDays]  WITH CHECK ADD  CONSTRAINT [FK_UserNonworkingDays_Users1] FOREIGN KEY([userId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[UserNonworkingDays] CHECK CONSTRAINT [FK_UserNonworkingDays_Users1]
GO


insert into [Clouseau].[dbo].[LicenseType] values ( 'Vacaciones','',0,15,15 );
insert into [Clouseau].[dbo].[LicenseType] values ( 'Semana CFO','',0,7,7 );
insert into [Clouseau].[dbo].[LicenseType] values ( 'Licencia Médica','',0,0,0 );
insert into [Clouseau].[dbo].[LicenseType] values ( 'Licencia de Estudio','',0,1,10 );
insert into [Clouseau].[dbo].[LicenseType] values ( 'Licencia por Maternidad','',0,0,0 );
insert into [Clouseau].[dbo].[LicenseType] values ( 'Licencia por Paternidad','',0,0,0 );
insert into [Clouseau].[dbo].[LicenseType] values ( 'Licencia por Matrimonio','',0,0,0 );
insert into [Clouseau].[dbo].[LicenseType] values ( 'Licencia por Fallecimiento','',0,0,0 );
insert into [Clouseau].[dbo].[LicenseType] values ( 'Licencia sin Goce de Sueldo','',0,0,0 );
insert into [Clouseau].[dbo].[LicenseType] values ( 'Licencia por Asuntos Judiciales','',0,0,0 );
insert into [Clouseau].[dbo].[LicenseType] values ( 'Donación de Sangre','',0,0,0 );
insert into [Clouseau].[dbo].[LicenseType] values ( 'Ausencia Injustificada','',0,0,0 );
insert into [Clouseau].[dbo].[LicenseType] values ( 'Otros','',0,0,0 );