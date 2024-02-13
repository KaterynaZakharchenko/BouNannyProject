CREATE TABLE [dbo].[Reviews] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [Content]     NVARCHAR (MAX) NOT NULL,
    [PostingTime] DATETIME       NOT NULL,
    [ReviewStars] INT            NOT NULL,
    [AdID]        INT            NOT NULL,
	[ClientID]    INT            NOT NULL,
    CONSTRAINT [PK_dbo.Reviews] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_dbo.Reviews_dbo.Ads_AdID] FOREIGN KEY ([AdID]) REFERENCES [dbo].[Ads] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.Reviews_dbo.Ads_ClientID] FOREIGN KEY ([ClientID]) REFERENCES [dbo].[Clients] ([ID]) ON DELETE CASCADE,
);


GO
CREATE NONCLUSTERED INDEX [IX_AdID]
    ON [dbo].[Reviews]([AdID] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_ClientID]
    ON [dbo].[Reviews]([ClientID] ASC);
