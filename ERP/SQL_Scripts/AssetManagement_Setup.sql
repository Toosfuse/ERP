-- ============================================
-- Asset Management System - SQL Script
-- ============================================

-- 1. حذف جدول AssetProperties (اگر وجود داشته باشد)
IF OBJECT_ID('dbo.AssetProperties', 'U') IS NOT NULL
BEGIN
    DROP TABLE dbo.AssetProperties;
    PRINT 'جدول AssetProperties حذف شد';
END

-- 2. اضافه کردن ستون CurrentOwnerId به جدول Assets
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS 
               WHERE TABLE_NAME = 'Assets' AND COLUMN_NAME = 'CurrentOwnerId')
BEGIN
    ALTER TABLE dbo.Assets ADD CurrentOwnerId INT NOT NULL DEFAULT 0;
    PRINT 'ستون CurrentOwnerId اضافه شد';
END

-- 3. اضافه کردن Foreign Key برای CurrentOwnerId
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS 
               WHERE CONSTRAINT_NAME = 'FK_Assets_CurrentOwnerId')
BEGIN
    ALTER TABLE dbo.Assets
    ADD CONSTRAINT FK_Assets_CurrentOwnerId
    FOREIGN KEY (CurrentOwnerId) REFERENCES dbo.AssestUsers(Id)
    ON DELETE RESTRICT;
    PRINT 'Foreign Key FK_Assets_CurrentOwnerId اضافه شد';
END

-- 4. اصلاح جدول AssetHistories
-- حذف ستون AssetPropertyId (اگر وجود داشته باشد)
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS 
           WHERE TABLE_NAME = 'AssetHistories' AND COLUMN_NAME = 'AssetPropertyId')
BEGIN
    -- حذف Foreign Key اگر وجود داشته باشد
    IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS 
               WHERE CONSTRAINT_NAME = 'FK_AssetHistories_AssetPropertyId')
    BEGIN
        ALTER TABLE dbo.AssetHistories
        DROP CONSTRAINT FK_AssetHistories_AssetPropertyId;
    END
    
    ALTER TABLE dbo.AssetHistories DROP COLUMN AssetPropertyId;
    PRINT 'ستون AssetPropertyId حذف شد';
END

-- 5. اضافه کردن ستونهای FromUserId و ToUserId
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS 
               WHERE TABLE_NAME = 'AssetHistories' AND COLUMN_NAME = 'FromUserId')
BEGIN
    ALTER TABLE dbo.AssetHistories ADD FromUserId INT NOT NULL DEFAULT 0;
    PRINT 'ستون FromUserId اضافه شد';
END

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS 
               WHERE TABLE_NAME = 'AssetHistories' AND COLUMN_NAME = 'ToUserId')
BEGIN
    ALTER TABLE dbo.AssetHistories ADD ToUserId INT NOT NULL DEFAULT 0;
    PRINT 'ستون ToUserId اضافه شد';
END

-- 6. اضافه کردن Foreign Keys برای AssetHistories
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS 
               WHERE CONSTRAINT_NAME = 'FK_AssetHistories_FromUserId')
BEGIN
    ALTER TABLE dbo.AssetHistories
    ADD CONSTRAINT FK_AssetHistories_FromUserId
    FOREIGN KEY (FromUserId) REFERENCES dbo.AssestUsers(Id)
    ON DELETE RESTRICT;
    PRINT 'Foreign Key FK_AssetHistories_FromUserId اضافه شد';
END

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS 
               WHERE CONSTRAINT_NAME = 'FK_AssetHistories_ToUserId')
BEGIN
    ALTER TABLE dbo.AssetHistories
    ADD CONSTRAINT FK_AssetHistories_ToUserId
    FOREIGN KEY (ToUserId) REFERENCES dbo.AssestUsers(Id)
    ON DELETE RESTRICT;
    PRINT 'Foreign Key FK_AssetHistories_ToUserId اضافه شد';
END

-- 7. ایجاد Indexes برای بهتر شدن Performance
IF NOT EXISTS (SELECT 1 FROM sys.indexes 
               WHERE name = 'IX_Assets_CurrentOwnerId' AND object_id = OBJECT_ID('dbo.Assets'))
BEGIN
    CREATE INDEX IX_Assets_CurrentOwnerId ON dbo.Assets(CurrentOwnerId);
    PRINT 'Index IX_Assets_CurrentOwnerId ایجاد شد';
END

IF NOT EXISTS (SELECT 1 FROM sys.indexes 
               WHERE name = 'IX_AssetHistories_FromUserId' AND object_id = OBJECT_ID('dbo.AssetHistories'))
BEGIN
    CREATE INDEX IX_AssetHistories_FromUserId ON dbo.AssetHistories(FromUserId);
    PRINT 'Index IX_AssetHistories_FromUserId ایجاد شد';
END

IF NOT EXISTS (SELECT 1 FROM sys.indexes 
               WHERE name = 'IX_AssetHistories_ToUserId' AND object_id = OBJECT_ID('dbo.AssetHistories'))
BEGIN
    CREATE INDEX IX_AssetHistories_ToUserId ON dbo.AssetHistories(ToUserId);
    PRINT 'Index IX_AssetHistories_ToUserId ایجاد شد';
END

-- 8. تحقق از ساختار نهایی
PRINT '';
PRINT '========== ساختار نهایی جداول ==========';
PRINT '';

PRINT 'جدول Assets:';
SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE 
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'Assets'
ORDER BY ORDINAL_POSITION;

PRINT '';
PRINT 'جدول AssetHistories:';
SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE 
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'AssetHistories'
ORDER BY ORDINAL_POSITION;

PRINT '';
PRINT 'تمام تغییرات با موفقیت اعمال شد!';
