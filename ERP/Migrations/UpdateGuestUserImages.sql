-- بروزرسانی Image خالی به Male.png
UPDATE [dbo].[GuestUsers]
SET [Image] = 'Male.png'
WHERE [Image] IS NULL OR [Image] = '';

SELECT * FROM [dbo].[GuestUsers];
