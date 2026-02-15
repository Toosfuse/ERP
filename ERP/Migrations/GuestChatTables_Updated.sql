-- اجرای این اسکریپت در SQL Server برای ایجاد جداول مهمان (بدون GuestChatMessages)

-- جدول کاربران مهمان
CREATE TABLE GuestUsers (
    Id INT PRIMARY KEY IDENTITY(1,1),
    PhoneNumber NVARCHAR(11) NOT NULL,
    FullName NVARCHAR(200),
    UniqueToken NVARCHAR(100) NOT NULL UNIQUE,
    CreatedDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    ExpiryDate DATETIME2 NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    LastActivity DATETIME2,
    GroupId INT NULL
);

CREATE INDEX IX_GuestUsers_PhoneNumber ON GuestUsers(PhoneNumber);
CREATE INDEX IX_GuestUsers_UniqueToken ON GuestUsers(UniqueToken);

-- جدول کدهای تایید SMS
CREATE TABLE GuestVerificationCodes (
    Id INT PRIMARY KEY IDENTITY(1,1),
    PhoneNumber NVARCHAR(11) NOT NULL,
    Code NVARCHAR(5) NOT NULL,
    CreatedDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    ExpiryDate DATETIME2 NOT NULL,
    IsUsed BIT NOT NULL DEFAULT 0,
    AttemptCount INT NOT NULL DEFAULT 0
);

CREATE INDEX IX_GuestVerificationCodes_PhoneNumber ON GuestVerificationCodes(PhoneNumber);

-- جدول دسترسی چت مهمانان
CREATE TABLE GuestChatAccesses (
    Id INT PRIMARY KEY IDENTITY(1,1),
    GuestUserId INT NOT NULL,
    AllowedUserId NVARCHAR(450) NOT NULL,
    GrantedDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (GuestUserId) REFERENCES GuestUsers(Id) ON DELETE CASCADE,
    FOREIGN KEY (AllowedUserId) REFERENCES AspNetUsers(Id) ON DELETE CASCADE
);

-- توضیح: پیامهای مهمان در جدول ChatMessages ذخیره میشوند
-- SenderId و ReceiverId برای مهمانها به صورت "guest_123" ذخیره میشوند

GO
