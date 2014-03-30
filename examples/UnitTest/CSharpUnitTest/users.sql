CREATE TABLE [dbo].[users]
(
		[id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [first_name] NVARCHAR(50) NULL, 
    [last_name] NVARCHAR(50) NULL, 
    [birthday] DATE NULL
)
