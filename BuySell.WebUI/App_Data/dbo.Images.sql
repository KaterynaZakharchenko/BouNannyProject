CREATE TABLE [dbo].[Images] (
    [ID]   INT            IDENTITY (1, 1) NOT NULL,
    [Path] NVARCHAR (255) NOT NULL,
    CONSTRAINT [PK_dbo.Images] PRIMARY KEY CLUSTERED ([ID] ASC),
);

