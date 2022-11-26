
IF NOT EXISTS( SELECT 1 FROM dbo.Currency)
BEGIN
	INSERT [dbo].[Currency] ([Code], [Name]) 
	VALUES 
	(N'NOK', N'Norwegian Krone'),
	(N'USD', N'United States Dollar'),
	(N'INR', N'Indian Rupee'),
	(N'EUR', N'Euro'),
	(N'JPY', N'Japanese Yen'),
	(N'GBP', N'British Pound Sterling'),
	(N'SAR', N'Saudi Riyal'),
	(N'KWD', N'Kuwaiti Dinar'),
	(N'OMR', N'Omani Rial'),
	(N'CHF', N'Swiss Franc')
END 


