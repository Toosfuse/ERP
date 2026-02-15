-- نمایش تمام Foreign Key های جدول ChatMessages
SELECT 
    fk.name AS ForeignKeyName,
    OBJECT_NAME(fk.parent_object_id) AS TableName,
    COL_NAME(fc.parent_object_id, fc.parent_column_id) AS ColumnName
FROM sys.foreign_keys AS fk
INNER JOIN sys.foreign_key_columns AS fc 
    ON fk.object_id = fc.constraint_object_id
WHERE OBJECT_NAME(fk.parent_object_id) = 'ChatMessages';

-- اگر Foreign Key وجود داشت، با نام واقعیش حذفش کن:
-- ALTER TABLE ChatMessages DROP CONSTRAINT [نام_واقعی_FK];
