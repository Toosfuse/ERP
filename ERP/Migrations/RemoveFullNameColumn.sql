-- حذف ستون FullName از جدول GuestUsers
IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[GuestUsers]') AND name = 'FullName')
BEGIN
    ALTER TABLE [dbo].[GuestUsers] DROP COLUMN [FullName];
    PRINT 'ستون FullName حذف شد';
END
ELSE
BEGIN
    PRINT 'ستون FullName وجود ندارد';
END
