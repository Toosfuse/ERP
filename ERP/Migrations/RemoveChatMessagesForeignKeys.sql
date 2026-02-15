-- حذف Foreign Key از جدول ChatMessages برای پشتیبانی از مهمانها

-- حذف FK برای SenderId
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_ChatMessages_AspNetUsers_SenderId')
BEGIN
    ALTER TABLE ChatMessages DROP CONSTRAINT FK_ChatMessages_AspNetUsers_SenderId;
END

-- حذف FK برای ReceiverId  
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_ChatMessages_AspNetUsers_ReceiverId')
BEGIN
    ALTER TABLE ChatMessages DROP CONSTRAINT FK_ChatMessages_AspNetUsers_ReceiverId;
END

GO

-- حالا SenderId و ReceiverId میتونن هم User ID و هم "guest_123" باشن
