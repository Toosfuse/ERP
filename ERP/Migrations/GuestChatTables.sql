-- اجرای این اسکریپت در SQL Server برای ایجاد جداول مهمان

-- جدول کاربران مهمان
CREATE TABLE GuestUsers (
    Id INT PRIMARY KEY IDENTITY(1,1),
    PhoneNumber NVARCHAR(11) NOT NULL,
    FullName NVARCHAR(200),
    UniqueToken NVARCHAR(100) NOT NULL UNIQUE,
    CreatedDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    ExpiryDate DATETIME2 NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    LastActivity DATETIME2
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

-- جدول پیام‌های چت مهمان
CREATE TABLE GuestChatMessages (
    Id INT PRIMARY KEY IDENTITY(1,1),
    GuestSenderId INT NULL,
    UserSenderId NVARCHAR(450) NULL,
    GuestReceiverId INT NULL,
    UserReceiverId NVARCHAR(450) NULL,
    Message NVARCHAR(MAX) NOT NULL,
    SentAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    IsRead BIT NOT NULL DEFAULT 0,
    ReadAt DATETIME2 NULL,
    IsDelivered BIT NOT NULL DEFAULT 0,
    DeliveredAt DATETIME2 NULL,
    AttachmentPath NVARCHAR(500) NULL,
    AttachmentName NVARCHAR(200) NULL,
    FOREIGN KEY (GuestSenderId) REFERENCES GuestUsers(Id),
    FOREIGN KEY (UserSenderId) REFERENCES AspNetUsers(Id),
    FOREIGN KEY (GuestReceiverId) REFERENCES GuestUsers(Id),
    FOREIGN KEY (UserReceiverId) REFERENCES AspNetUsers(Id)
);

GO
