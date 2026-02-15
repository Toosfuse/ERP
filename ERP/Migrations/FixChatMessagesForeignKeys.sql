-- بررسی و حذف Foreign Key های جدول ChatMessages

-- نمایش تمام Foreign Key های موجود
SELECT 
    fk.name AS ForeignKeyName,
    OBJECT_NAME(fk.parent_object_id) AS TableName,
    COL_NAME(fc.parent_object_id, fc.parent_column_id) AS ColumnName
FROM 
    sys.foreign_keys AS fk
INNER JOIN 
    sys.foreign_key_columns AS fc ON fk.object_id = fc.constraint_object_id
WHERE 
    OBJECT_NAME(fk.parent_object_id) = 'ChatMessages';

-- حذف Foreign Key ها اگر وجود داشته باشند
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_ChatMessages_AspNetUsers_SenderId')
BEGIN
    ALTER TABLE [dbo].[ChatMessages] DROP CONSTRAINT [FK_ChatMessages_AspNetUsers_SenderId];
    PRINT 'FK_ChatMessages_AspNetUsers_SenderId حذف شد';
END

IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_ChatMessages_AspNetUsers_ReceiverId')
BEGIN
    ALTER TABLE [dbo].[ChatMessages] DROP CONSTRAINT [FK_ChatMessages_AspNetUsers_ReceiverId];
    PRINT 'FK_ChatMessages_AspNetUsers_ReceiverId حذف شد';
END

PRINT 'عملیات با موفقیت انجام شد';
