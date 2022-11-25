
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Currency]') AND type in (N'U'))
BEGIN
    CREATE TABLE [Currency](
        [Id] [integer] IDENTITY(1,1) NOT NULL,
        [Code] NVARCHAR(100) NOT NULL,
        [Name] NVARCHAR(250) NOT NULL,
        CONSTRAINT [PK_Currency] PRIMARY KEY ([Id] ASC)
    );
END


IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CurrencyExchangeRates]') AND type in (N'U'))
BEGIN
	CREATE TABLE [CurrencyExchangeRates] (
	  [Id] [integer] IDENTITY(1,1) NOT NULL,
	  [SourceCurrencyId]  INT NOT NULL,
	  [TargetCurrencyId] INT NOT NULL,
	  [TargetCurrencyExchangeRate] DECIMAL(16,4) NOT NULL,
	  [RecordedOn] DATETIME NOT NULL
	  CONSTRAINT [PK_CurrencyExchangeRates] PRIMARY KEY ([Id] ASC),
	  CONSTRAINT [FK_SourceCurrencyId] FOREIGN KEY ([SourceCurrencyId]) REFERENCES Currency(Id),
	  CONSTRAINT [FK_TargetCurrencyId] FOREIGN KEY ([TargetCurrencyId]) REFERENCES Currency(Id)
	);
END

--IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CowboyDetails]') AND type in (N'U'))
--BEGIN
--    CREATE TABLE [CowboyDetails](
--        [Id] [integer] IDENTITY(1,1) NOT NULL,
--        [Name] NVARCHAR(250) NOT NULL,
--        [Age] INT NOT NULL,
--        [Height] DECIMAL(16,4) NOT NULL,
--        [Hair] NVARCHAR(250) NOT NULL,
--        [Longitude] DECIMAL(12,9) NOT NULL,
--        [Latitude] DECIMAL(12,9) NOT NULL,
--		[Life_left] NUMERIC
--        CONSTRAINT [PK_CowboyDetails] PRIMARY KEY ([Id] ASC)
--    );
--END


--IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GunDetails]') AND type in (N'U'))
--BEGIN
--    CREATE TABLE [GunDetails](
--        [Id] [integer] IDENTITY(1,1) NOT NULL,
--        [GunName] NVARCHAR(250) NOT NULL,
--        [MaxNoOfBullets] INT NOT NULL,
--        CONSTRAINT [PK_GunDetails] PRIMARY KEY ([Id] ASC)
--    );
--END


--IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CowBoyGunMapping]') AND type in (N'U'))
--BEGIN
--	CREATE TABLE [CowBoyGunMapping] (
--	  [Id] [integer] IDENTITY(1,1) NOT NULL,
--	  [Cowboy_Id]  INT NOT NULL,
--	  [Gun_Id] INT NOT NULL,
--	  [Bulets_Left] INT NOT NULL,
--	  CONSTRAINT [PK_CowBoyGunMapping] PRIMARY KEY ([Id] ASC),
--	  CONSTRAINT FK_Cowboy FOREIGN KEY (Cowboy_Id) REFERENCES CowboyDetails(Id),
--	  CONSTRAINT FK_Gun FOREIGN KEY (Gun_Id) REFERENCES GunDetails(Id)
--	);
--END


--------------------------------------------------------------------------









