USE [master]
GO
/****** Object:  Database [ShopMS]    Script Date: 22-11-2020 11:36:09 ******/
CREATE DATABASE [ShopMS] ON  PRIMARY 
( NAME = N'ShopMS', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\ShopMS.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ShopMS_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\ShopMS_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ShopMS] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ShopMS].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ShopMS] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ShopMS] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ShopMS] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ShopMS] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ShopMS] SET ARITHABORT OFF 
GO
ALTER DATABASE [ShopMS] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ShopMS] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ShopMS] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ShopMS] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ShopMS] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ShopMS] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ShopMS] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ShopMS] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ShopMS] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ShopMS] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ShopMS] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ShopMS] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ShopMS] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ShopMS] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ShopMS] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ShopMS] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ShopMS] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ShopMS] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ShopMS] SET  MULTI_USER 
GO
ALTER DATABASE [ShopMS] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ShopMS] SET DB_CHAINING OFF 
GO
USE [ShopMS]
GO
/****** Object:  Table [dbo].[tblCustomers]    Script Date: 22-11-2020 11:36:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCustomers](
	[customer_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[dob] [date] NULL,
	[phone_no] [bigint] NULL,
	[address] [nvarchar](100) NULL,
	[sex] [nchar](10) NULL,
 CONSTRAINT [PK_tblCustomers] PRIMARY KEY CLUSTERED 
(
	[customer_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblEmployees]    Script Date: 22-11-2020 11:36:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblEmployees](
	[employee_id] [int] IDENTITY(1,1) NOT NULL,
	[first_name] [nvarchar](50) NOT NULL,
	[middle_name] [nvarchar](50) NULL,
	[last_name] [nvarchar](50) NOT NULL,
	[user_id] [nvarchar](50) NOT NULL,
	[password] [nvarchar](50) NOT NULL,
	[salary] [numeric](18, 0) NULL,
	[phone_no] [bigint] NULL,
	[email_id] [nvarchar](50) NULL,
	[dob] [date] NULL,
	[uid] [bigint] NULL,
	[address] [nvarchar](100) NULL,
	[sex] [nchar](10) NULL,
	[approval] [int] NULL,
	[role] [nvarchar](50) NULL,
 CONSTRAINT [PK_tblEmployees] PRIMARY KEY CLUSTERED 
(
	[employee_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblOrders]    Script Date: 22-11-2020 11:36:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblOrders](
	[order_id] [bigint] IDENTITY(1,1) NOT NULL,
	[product_id] [int] NOT NULL,
	[customer_id] [int] NOT NULL,
	[employee_id] [int] NOT NULL,
	[date] [date] NOT NULL,
	[time] [time](7) NOT NULL,
	[quantity] [int] NOT NULL,
	[discount] [numeric](18, 0) NULL,
 CONSTRAINT [PK_tblOrders] PRIMARY KEY CLUSTERED 
(
	[order_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblProducts]    Script Date: 22-11-2020 11:36:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblProducts](
	[product_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[brand] [nvarchar](50) NULL,
	[description] [nvarchar](50) NULL,
	[quantity] [int] NOT NULL,
	[cost_price] [numeric](18, 0) NOT NULL,
	[sell_price] [numeric](18, 0) NOT NULL,
	[purchace_date] [date] NOT NULL,
	[mfg_date] [date] NOT NULL,
	[expiry_date] [date] NOT NULL,
	[contents] [nvarchar](50) NULL,
	[defective_items] [int] NULL,
	[type] [nvarchar](50) NULL,
 CONSTRAINT [PK_tblProducts] PRIMARY KEY CLUSTERED 
(
	[product_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tblEmployees] ADD  CONSTRAINT [DF_tblEmployees_approval]  DEFAULT ((0)) FOR [approval]
GO
ALTER TABLE [dbo].[tblOrders]  WITH CHECK ADD  CONSTRAINT [FK_tblOrders_tblCustomers] FOREIGN KEY([customer_id])
REFERENCES [dbo].[tblCustomers] ([customer_id])
GO
ALTER TABLE [dbo].[tblOrders] CHECK CONSTRAINT [FK_tblOrders_tblCustomers]
GO
ALTER TABLE [dbo].[tblOrders]  WITH CHECK ADD  CONSTRAINT [FK_tblOrders_tblEmployees] FOREIGN KEY([employee_id])
REFERENCES [dbo].[tblEmployees] ([employee_id])
GO
ALTER TABLE [dbo].[tblOrders] CHECK CONSTRAINT [FK_tblOrders_tblEmployees]
GO
ALTER TABLE [dbo].[tblOrders]  WITH CHECK ADD  CONSTRAINT [FK_tblOrders_tblProducts] FOREIGN KEY([product_id])
REFERENCES [dbo].[tblProducts] ([product_id])
GO
ALTER TABLE [dbo].[tblOrders] CHECK CONSTRAINT [FK_tblOrders_tblProducts]
GO
USE [master]
GO
ALTER DATABASE [ShopMS] SET  READ_WRITE 
GO
