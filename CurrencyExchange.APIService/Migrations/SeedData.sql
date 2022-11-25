
IF NOT EXISTS( SELECT 1 FROM dbo.Currency)
BEGIN
	INSERT [dbo].[Currency] ([Code], [Name]) 
	VALUES 
	(N'USD', N'United States Dollar'),
	(N'INR', N'Indian Rupee'),
	(N'EUR', N'Euro'),
	(N'JPY', N'Japanese Yen'),
	(N'GBP', N'British Pound Sterling'),
	(N'SAR', N'Saudi Riyal'),
	(N'KWD', N'Kuwaiti Dinar'),
	(N'OMR', N'Omani Rial'),
	(N'CHF', N'Swiss Franc'),
	(N'BHD', N'Bahraini Dinar')
END 

--IF NOT EXISTS( SELECT 1 FROM GunDetails)
--BEGIN
--	INSERT [dbo].[GunDetails] ([GunName], [MaxNoOfBullets]) 
--	VALUES 
--	(N'AK 47', 20),
--	(N'Handguns', 30),
--	(N'Shotguns', 45),
--	(N'Sniper Rifles', 60),
--	(N'Pistols', 6),
--	(N'Rifles', 46),
--	(N'Light Machine Guns', 50)
--END 

