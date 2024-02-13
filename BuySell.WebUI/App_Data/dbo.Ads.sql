CREATE TABLE [dbo].[Ads] (
    [ID]          INT             IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (MAX)  NOT NULL,
    [TimePeriodID] INT             NOT NULL,
    [Description] NVARCHAR (500)  NULL,
    [Price]       DECIMAL (18, 2) NOT NULL,
    [CountryID]   INT             NOT NULL,
    [StateID]     INT             NOT NULL,
    [CityID]      INT             NOT NULL,
    [ClientID]    INT             NOT NULL,
    [Slug]        NVARCHAR (MAX)  NOT NULL,
    [PostingTime] DATETIME        NOT NULL,
    [Age] INT NOT NULL, 
    [SexID] INT NOT NULL, 
    [LevelID] INT NOT NULL, 
    CONSTRAINT [PK_dbo.Ads] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_dbo.Ads_dbo.Cities_CityID] FOREIGN KEY ([CityID]) REFERENCES [dbo].[Cities] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.Ads_dbo.TimePeriods_TimePeriodID] FOREIGN KEY ([TimePeriodID]) REFERENCES [dbo].[TimePeriods] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.Ads_dbo.Countries_CountryID] FOREIGN KEY ([CountryID]) REFERENCES [dbo].[Countries] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.Ads_dbo.Clients_ClientID] FOREIGN KEY ([ClientID]) REFERENCES [dbo].[Clients] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.Ads_dbo.States_StateID] FOREIGN KEY ([StateID]) REFERENCES [dbo].[States] ([ID]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_TimePeriodID]
    ON [dbo].[Ads]([TimePeriodID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CountryID]
    ON [dbo].[Ads]([CountryID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_StateID]
    ON [dbo].[Ads]([StateID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CityID]
    ON [dbo].[Ads]([CityID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ClientID]
    ON [dbo].[Ads]([ClientID] ASC);

