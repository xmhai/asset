USE asset;

CREATE TABLE [dbo].[Cash](
	[CashID] [int] IDENTITY(1,1) NOT NULL,
	[CashType] [int] NOT NULL,
	[BankName] [nvarchar](max) NULL,
	[BankAccountName] [nvarchar](max) NULL,
	[BankAccountNo] [nvarchar](max) NULL,
	[Currency] [int] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[MaturityDate] [datetime] NULL,
	[UpdatedDate] [datetime] NOT NULL
);

CREATE TABLE [dbo].[Stock](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](max) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Exchange] int NOT NULL,
	[Currency] [int] NOT NULL,
	[Category] [int]NOT NULL	-- ETF, REITS, BOND, STOCK
);

CREATE TABLE [dbo].[Portfolio](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StockID] [int] NOT NULL,
	[Cost]	[decimal](18, 2),
	[Quantity] int
);

CREATE TABLE [dbo].[Transaction](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StockID] [int] NOT NULL,
	[Action] [int] NOT NULL, -- BUY, SELL
	[Price]	[decimal](18, 2) NOT NULL,
	[Quantity] int,
	[OrderDate] [datetime] NOT NULL
);

CREATE TABLE [dbo].[Divident](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StockID] [int] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[PayDate] [datetime] NOT NULL
);

CREATE TABLE [dbo].[AssetHistory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Cash] [decimal](18, 2) NOT NULL,
	[Stock] [decimal](18, 2) NOT NULL,
	[ETF] [decimal](18, 2) NOT NULL,
	[Bond] [decimal](18, 2) NOT NULL,
	[Loan] [decimal](18, 2) NOT NULL,
	[Debt] [decimal](18, 2) NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[CFP] [decimal](18, 2) NOT NULL,
	[RecordDate] [datetime] NOT NULL
);

CREATE TABLE [dbo].[Profit](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StockID] [int] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL
);
