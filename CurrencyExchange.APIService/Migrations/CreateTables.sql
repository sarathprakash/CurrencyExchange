
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

IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserDetails]') AND type in (N'U'))
BEGIN
    CREATE TABLE [UserDetails](
        [Id] [integer] IDENTITY(1,1) NOT NULL,
        [Username] NVARCHAR(100) NOT NULL,
        [Password] NVARCHAR(100) NOT NULL,
        CONSTRAINT [PK_UserDetails] PRIMARY KEY ([Id] ASC)
    );
END

--------------------------------------------------------------------------









