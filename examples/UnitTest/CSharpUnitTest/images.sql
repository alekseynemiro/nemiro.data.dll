CREATE TABLE [dbo].[images]
(
		[id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [image_url] NVARCHAR(500) NULL, 
    [date_created] DATETIME NULL
)
