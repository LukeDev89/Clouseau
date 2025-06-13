-- Create ExtraHours table
CREATE TABLE [dbo].[ExtraHours] (
    [Id] BIGINT IDENTITY(1,1) NOT NULL,
    [userId] BIGINT NOT NULL,
    [taskId] BIGINT NOT NULL,
    [taskTypeId] BIGINT NOT NULL,
    [date] DATETIME NOT NULL,
    [hours] DECIMAL(5,2) NOT NULL,
    [comment] CHAR(250) NOT NULL DEFAULT '',
    [status] SMALLINT NOT NULL DEFAULT 1, -- 1=En Evaluación, 2=Aprobadas, 3=Rechazadas
    [created] DATETIME NOT NULL DEFAULT GETDATE(),
    [reviewed] DATETIME NULL,
    [reviewedBy] BIGINT NULL,
    [reviewComment] CHAR(250) NULL,
    
    CONSTRAINT [PK_ExtraHours] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ExtraHours_Tasks] FOREIGN KEY ([taskId]) REFERENCES [ProjectTasks] ([Id]),
    CONSTRAINT [FK_ExtraHours_TaskTypes] FOREIGN KEY ([taskTypeId]) REFERENCES [TaskTypes] ([Id]),
    CONSTRAINT [FK_ExtraHours_Users] FOREIGN KEY ([userId]) REFERENCES [Users] ([Id]),
    CONSTRAINT [FK_ExtraHours_ReviewedBy_Users] FOREIGN KEY ([reviewedBy]) REFERENCES [Users] ([Id])
);

-- Create index for better performance
CREATE INDEX [IX_ExtraHours_UserId] ON [dbo].[ExtraHours] ([userId]);
CREATE INDEX [IX_ExtraHours_Status] ON [dbo].[ExtraHours] ([status]);
CREATE INDEX [IX_ExtraHours_Date] ON [dbo].[ExtraHours] ([date]);
CREATE INDEX [IX_ExtraHours_ReviewedBy] ON [dbo].[ExtraHours] ([reviewedBy]);

-- Remove ExtraHours column from TaskProgress table (reverting previous addition)
ALTER TABLE [dbo].[TaskProgress] DROP COLUMN [extraHours];