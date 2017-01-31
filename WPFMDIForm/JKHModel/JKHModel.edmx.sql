
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/01/2017 00:32:17
-- Generated from EDMX file: C:\WPFMDIForm\WPFMDIForm\WPFMDIForm\JKHModel\JKHModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Database1111];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ДомКвартира]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[КвартираSet] DROP CONSTRAINT [FK_ДомКвартира];
GO
IF OBJECT_ID(N'[dbo].[FK_КвартираСчетчик]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[СчетчикSet] DROP CONSTRAINT [FK_КвартираСчетчик];
GO
IF OBJECT_ID(N'[dbo].[FK_КвартираЖилец]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ЖилецSet] DROP CONSTRAINT [FK_КвартираЖилец];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ДомSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ДомSet];
GO
IF OBJECT_ID(N'[dbo].[КвартираSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[КвартираSet];
GO
IF OBJECT_ID(N'[dbo].[СчетчикSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[СчетчикSet];
GO
IF OBJECT_ID(N'[dbo].[ЖилецSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ЖилецSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ДомSet'
CREATE TABLE [dbo].[ДомSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Адрес] nvarchar(max)  NOT NULL,
    [Площадь] decimal(18,0)  NOT NULL
);
GO

-- Creating table 'КвартираSet'
CREATE TABLE [dbo].[КвартираSet] (
    [Номер] int IDENTITY(1,1) NOT NULL,
    [Площадь_квартиры] decimal(18,0)  NOT NULL,
    [Счетчики_установлены] bit  NOT NULL,
    [Дом_Id] int  NOT NULL
);
GO

-- Creating table 'СчетчикSet'
CREATE TABLE [dbo].[СчетчикSet] (
    [Код_счетчика] int IDENTITY(1,1) NOT NULL,
    [Квартира_Номер] int  NOT NULL
);
GO

-- Creating table 'ЖилецSet'
CREATE TABLE [dbo].[ЖилецSet] (
    [Id] nvarchar(max)  NOT NULL,
    [ФИО] nvarchar(max)  NOT NULL,
    [Квартира_Номер] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'ДомSet'
ALTER TABLE [dbo].[ДомSet]
ADD CONSTRAINT [PK_ДомSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Номер] in table 'КвартираSet'
ALTER TABLE [dbo].[КвартираSet]
ADD CONSTRAINT [PK_КвартираSet]
    PRIMARY KEY CLUSTERED ([Номер] ASC);
GO

-- Creating primary key on [Код_счетчика] in table 'СчетчикSet'
ALTER TABLE [dbo].[СчетчикSet]
ADD CONSTRAINT [PK_СчетчикSet]
    PRIMARY KEY CLUSTERED ([Код_счетчика] ASC);
GO

-- Creating primary key on [Id] in table 'ЖилецSet'
ALTER TABLE [dbo].[ЖилецSet]
ADD CONSTRAINT [PK_ЖилецSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Дом_Id] in table 'КвартираSet'
ALTER TABLE [dbo].[КвартираSet]
ADD CONSTRAINT [FK_ДомКвартира]
    FOREIGN KEY ([Дом_Id])
    REFERENCES [dbo].[ДомSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ДомКвартира'
CREATE INDEX [IX_FK_ДомКвартира]
ON [dbo].[КвартираSet]
    ([Дом_Id]);
GO

-- Creating foreign key on [Квартира_Номер] in table 'СчетчикSet'
ALTER TABLE [dbo].[СчетчикSet]
ADD CONSTRAINT [FK_КвартираСчетчик]
    FOREIGN KEY ([Квартира_Номер])
    REFERENCES [dbo].[КвартираSet]
        ([Номер])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_КвартираСчетчик'
CREATE INDEX [IX_FK_КвартираСчетчик]
ON [dbo].[СчетчикSet]
    ([Квартира_Номер]);
GO

-- Creating foreign key on [Квартира_Номер] in table 'ЖилецSet'
ALTER TABLE [dbo].[ЖилецSet]
ADD CONSTRAINT [FK_КвартираЖилец]
    FOREIGN KEY ([Квартира_Номер])
    REFERENCES [dbo].[КвартираSet]
        ([Номер])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_КвартираЖилец'
CREATE INDEX [IX_FK_КвартираЖилец]
ON [dbo].[ЖилецSet]
    ([Квартира_Номер]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------