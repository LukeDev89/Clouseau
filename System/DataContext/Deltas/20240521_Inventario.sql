CREATE TABLE [dbo].[inventoryPartType](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [nchar](50) NOT NULL,
 CONSTRAINT [PK_inventoryPartType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[inventoryHistory](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[inventoryId] [bigint] NOT NULL,
	[inventoryPartId] [bigint] NOT NULL,
	[inventoryLicenseId] [bigint] NOT NULL,
	[observation] [nchar](250) NULL,
	[date] [datetime] NULL,
 CONSTRAINT [PK_inventoryHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[inventoryParts](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [nchar](250) NOT NULL,
	[description] [nchar](250) NULL,
	[serial] [nchar](250) NULL,
	[partTypeId] [bigint] NOT NULL,
	[active] [bit] NOT NULL,
 CONSTRAINT [PK_inventoryParts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[inventoryModel](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[brandId] [bigint] NOT NULL,
	[name] [nchar](50) NOT NULL,
 CONSTRAINT [PK_inventoryModel] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[inventoryComment](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[inventoryId] [bigint] NOT NULL,
	[comment] [varchar](max) NULL,
	[date] [datetime] NOT NULL,
 CONSTRAINT [PK_inventoryComment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[inventorySoftware](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[inventoryId] [bigint] NOT NULL,
	[inventoryLicenseId] [bigint] NOT NULL,
	[date] [datetime] NOT NULL,
 CONSTRAINT [PK_inventorySoftware] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[inventoryUser](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[inventoryId] [bigint] NOT NULL,
	[userId] [bigint] NOT NULL,
	[dateFrom] [date] NOT NULL,
	[dateTo] [date] NULL,
 CONSTRAINT [PK_inventoryUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[inventoryLicenseType](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [nchar](50) NOT NULL,
 CONSTRAINT [PK_inventoryLicenseType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[inventoryLicense](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [nchar](50) NOT NULL,
	[description] [nchar](250) NULL,
	[serial] [nchar](50) NULL,
	[licenseTypeId] [bigint] NOT NULL,
	[active] [bit] NOT NULL,
	[userId] [bigint] NULL,
	[requestDate] [date] NULL,
	[requestFinished] [date] NULL,
 CONSTRAINT [PK_inventoryLicense] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[inventoryBrand](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [nchar](50) NOT NULL,
 CONSTRAINT [PK_inventoryBrand] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[inventory](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[modelId] [bigint] NOT NULL,
	[serial] [nvarchar](50) NOT NULL,
	[username] [nvarchar](50) NULL,
	[password] [nvarchar](50) NULL,
	[domain] [nvarchar](50) NULL,
	[processor] [nvarchar](50) NULL,
	[memory] [nvarchar](50) NULL,
	[storage] [nvarchar](50) NULL,
	[crystal] [nvarchar](50) NULL,
	[startDate] [datetime] NOT NULL,
	[endDate] [datetime] NULL,
	[state] [smallint] NOT NULL,
 CONSTRAINT [PK_inventory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[inventory] ADD  CONSTRAINT [DF_inventory_startDate]  DEFAULT (getdate()) FOR [startDate]
GO

ALTER TABLE [dbo].[inventory]  WITH CHECK ADD  CONSTRAINT [FK_inventory_inventoryModel] FOREIGN KEY([modelId])
REFERENCES [dbo].[inventoryModel] ([Id])
GO

ALTER TABLE [dbo].[inventory] CHECK CONSTRAINT [FK_inventory_inventoryModel]
GO

ALTER TABLE [dbo].[inventorySoftware]  WITH CHECK ADD  CONSTRAINT [FK_inventorySoftware_inventory] FOREIGN KEY([inventoryId])
REFERENCES [dbo].[inventory] ([Id])
GO


ALTER TABLE [dbo].[inventorySoftware] CHECK CONSTRAINT [FK_inventorySoftware_inventory]
GO

ALTER TABLE [dbo].[inventorySoftware]  WITH CHECK ADD  CONSTRAINT [FK_inventorySoftware_inventoryLicense] FOREIGN KEY([inventoryLicenseId])
REFERENCES [dbo].[inventoryLicense] ([Id])
GO

ALTER TABLE [dbo].[inventorySoftware] CHECK CONSTRAINT [FK_inventorySoftware_inventoryLicense]
GO

ALTER TABLE [dbo].[inventoryComment] ADD  CONSTRAINT [DF_inventoryComment_date]  DEFAULT (getdate()) FOR [date]
GO

ALTER TABLE [dbo].[inventoryComment]  WITH CHECK ADD  CONSTRAINT [FK_inventoryComment_inventory] FOREIGN KEY([inventoryId])
REFERENCES [dbo].[inventory] ([Id])
GO

ALTER TABLE [dbo].[inventoryComment] CHECK CONSTRAINT [FK_inventoryComment_inventory]
GO

ALTER TABLE [dbo].[inventoryHistory]  WITH CHECK ADD  CONSTRAINT [FK_inventoryHistory_inventory] FOREIGN KEY([inventoryId])
REFERENCES [dbo].[inventory] ([Id])
GO

ALTER TABLE [dbo].[inventoryHistory] CHECK CONSTRAINT [FK_inventoryHistory_inventory]
GO

ALTER TABLE [dbo].[inventoryHistory]  WITH CHECK ADD  CONSTRAINT [FK_inventoryHistory_inventoryLicense] FOREIGN KEY([inventoryLicenseId])
REFERENCES [dbo].[inventoryLicense] ([Id])
GO

ALTER TABLE [dbo].[inventoryHistory] CHECK CONSTRAINT [FK_inventoryHistory_inventoryLicense]
GO

ALTER TABLE [dbo].[inventoryHistory]  WITH CHECK ADD  CONSTRAINT [FK_inventoryHistory_inventoryParts] FOREIGN KEY([inventoryPartId])
REFERENCES [dbo].[inventoryParts] ([Id])
GO

ALTER TABLE [dbo].[inventoryHistory] CHECK CONSTRAINT [FK_inventoryHistory_inventoryParts]
GO

ALTER TABLE [dbo].[inventoryModel]  WITH CHECK ADD  CONSTRAINT [FK_inventoryModel_inventoryBrand] FOREIGN KEY([brandId])
REFERENCES [dbo].[inventoryBrand] ([Id])
GO

ALTER TABLE [dbo].[inventoryModel] CHECK CONSTRAINT [FK_inventoryModel_inventoryBrand]
GO

ALTER TABLE [dbo].[inventoryParts] ADD  CONSTRAINT [DF_inventoryParts_active]  DEFAULT ((1)) FOR [active]
GO

ALTER TABLE [dbo].[inventoryParts]  WITH CHECK ADD  CONSTRAINT [FK_inventoryParts_inventoryPartType] FOREIGN KEY([partTypeId])
REFERENCES [dbo].[inventoryPartType] ([Id])
GO

ALTER TABLE [dbo].[inventoryParts] CHECK CONSTRAINT [FK_inventoryParts_inventoryPartType]
GO

ALTER TABLE [dbo].[inventoryUser] ADD  CONSTRAINT [DF_inventoryUser_dateFrom]  DEFAULT (getdate()) FOR [dateFrom]
GO

ALTER TABLE [dbo].[inventoryUser]  WITH CHECK ADD  CONSTRAINT [FK_inventoryUser_inventory] FOREIGN KEY([inventoryId])
REFERENCES [dbo].[inventory] ([Id])
GO

ALTER TABLE [dbo].[inventoryUser] CHECK CONSTRAINT [FK_inventoryUser_inventory]
GO

ALTER TABLE [dbo].[inventoryUser]  WITH CHECK ADD  CONSTRAINT [FK_inventoryUser_Users] FOREIGN KEY([userId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[inventoryUser] CHECK CONSTRAINT [FK_inventoryUser_Users]
GO

ALTER TABLE [dbo].[inventoryLicense] ADD  CONSTRAINT [DF_inventoryLicense_active]  DEFAULT ((1)) FOR [active]
GO

ALTER TABLE [dbo].[inventoryLicense]  WITH CHECK ADD  CONSTRAINT [FK_inventoryLicense_inventoryLicenseType] FOREIGN KEY([licenseTypeId])
REFERENCES [dbo].[inventoryLicenseType] ([Id])
GO

ALTER TABLE [dbo].[inventoryLicense] CHECK CONSTRAINT [FK_inventoryLicense_inventoryLicenseType]
GO

ALTER TABLE [dbo].[inventoryLicense]  WITH CHECK ADD  CONSTRAINT [FK_inventoryLicense_Users] FOREIGN KEY([userId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[inventoryLicense] CHECK CONSTRAINT [FK_inventoryLicense_Users]
GO

ALTER TABLE InventoryParts
ALTER COLUMN [Name] nvarchar(100);

ALTER TABLE InventoryParts
ALTER COLUMN [Description] nvarchar(100);

ALTER TABLE InventoryParts
ALTER COLUMN [Serial] nvarchar(100);

ALTER TABLE dbo.inventoryHistory
DROP CONSTRAINT FK_inventoryHistory_inventoryParts;

ALTER TABLE dbo.inventoryHistory
DROP CONSTRAINT FK_inventoryHistory_inventoryLicense;

ALTER TABLE dbo.inventoryHistory
ALTER COLUMN inventoryPartId BIGINT NULL;

ALTER TABLE dbo.inventoryHistory
ALTER COLUMN inventoryLicenseId BIGINT NULL;

ALTER TABLE dbo.inventoryHistory
ADD CONSTRAINT FK_inventoryHistory_inventoryParts
FOREIGN KEY (inventoryPartId) REFERENCES dbo.inventoryParts(Id);

ALTER TABLE dbo.inventoryHistory
ADD CONSTRAINT FK_inventoryHistory_inventoryLicense
FOREIGN KEY (inventoryLicenseId) REFERENCES dbo.inventoryLicense(Id);
