CREATE TABLE [dbo].[phone_book] (
    [id]           INT IDENTITY	 NOT NULL,
    [first_name]   NVARCHAR (50) NULL,
    [last_name]    NVARCHAR (50) NULL,
    [home_phone]   NVARCHAR (50) NULL,
    [mobile_phone] NVARCHAR (50) NULL,
    [office_phone] NVARCHAR (50) NULL,
    [birthday]     DATETIME      NULL,
    [gender]       CHAR (1)      NULL,
    [date_created] DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

