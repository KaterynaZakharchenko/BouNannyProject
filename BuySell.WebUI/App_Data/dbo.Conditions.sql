CREATE TABLE [dbo].[TimePeriods] (
    [ID]            INT            IDENTITY (1, 1) NOT NULL,
    [Period] NVARCHAR (MAX) NOT NULL,
    [Description]   NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.TimePeriods] PRIMARY KEY CLUSTERED ([ID] ASC)
);

