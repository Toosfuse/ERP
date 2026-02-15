-- حذف Foreign Key از جدول ChatMessages (با چک کردن وجود)

USE ERP;
GO

-- چک و حذف FK برای SenderId
DECLARE @ConstraintName1 NVARCHAR(200)
SELECT @ConstraintName1 = name 
FROM sys.foreign_keys 
WHERE parent_object_id = OBJECT_ID('ChatMessages') 
AND referenced_object_id = OBJECT_ID('AspNetUsers')
AND COL_NAME(parent_object_id, parent_columns.column_id) = 'SenderId'
FROM sys.foreign_key_columns parent_columns
WHERE parent_columns.constraint_object_id = sys.foreign_keys.object_id

IF @ConstraintName1 IS NOT NULL
BEGIN
    EXEC('ALTER TABLE ChatMessages DROP CONSTRAINT ' + @ConstraintName1)
    PRINT 'Dropped constraint: ' + @ConstraintName1
END
ELSE
    PRINT 'SenderId constraint not found'

-- چک و حذف FK برای ReceiverId
DECLARE @ConstraintName2 NVARCHAR(200)
SELECT @ConstraintName2 = name 
FROM sys.foreign_keys 
WHERE parent_object_id = OBJECT_ID('ChatMessages') 
AND referenced_object_id = OBJECT_ID('AspNetUsers')
AND COL_NAME(parent_object_id, parent_columns.column_id) = 'ReceiverId'
FROM sys.foreign_key_columns parent_columns
WHERE parent_columns.constraint_object_id = sys.foreign_keys.object_id

IF @ConstraintName2 IS NOT NULL
BEGIN
    EXEC('ALTER TABLE ChatMessages DROP CONSTRAINT ' + @ConstraintName2)
    PRINT 'Dropped constraint: ' + @ConstraintName2
END
ELSE
    PRINT 'ReceiverId constraint not found'

GO

PRINT 'Migration completed successfully!'
