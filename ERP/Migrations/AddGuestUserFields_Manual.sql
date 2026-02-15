-- اضافه کردن فیلدهای جدید به جدول GuestUsers
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[GuestUsers]') AND name = 'FirstName')
BEGIN
    ALTER TABLE [dbo].[GuestUsers] ADD [FirstName] NVARCHAR(MAX) NULL;
END

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[GuestUsers]') AND name = 'LastName')
BEGIN
    ALTER TABLE [dbo].[GuestUsers] ADD [LastName] NVARCHAR(MAX) NULL;
END

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[GuestUsers]') AND name = 'Image')
BEGIN
    ALTER TABLE [dbo].[GuestUsers] ADD [Image] NVARCHAR(MAX) NOT NULL DEFAULT 'Male.png';
END

PRINT 'فیلدها با موفقیت اضافه شدند';
