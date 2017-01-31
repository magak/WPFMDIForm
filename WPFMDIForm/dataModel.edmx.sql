
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/31/2017 20:26:22
-- Generated from EDMX file: K:\ВАЖНО\R\Multiple Document WPF\WPFMDIForm\dataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Vshp2016];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ПоказанияУслуги]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Услуги] DROP CONSTRAINT [FK_ПоказанияУслуги];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Показания]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Показания];
GO
IF OBJECT_ID(N'[dbo].[Услуги]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Услуги];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Показания'
CREATE TABLE [dbo].[Показания] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Значение] decimal(18,0)  NOT NULL,
    [Услуги_Id] int  NOT NULL
);
GO

-- Creating table 'Услуги'
CREATE TABLE [dbo].[Услуги] (
    [Id] int IDENTITY(1,1) NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Показания'
ALTER TABLE [dbo].[Показания]
ADD CONSTRAINT [PK_Показания]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Услуги'
ALTER TABLE [dbo].[Услуги]
ADD CONSTRAINT [PK_Услуги]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Услуги_Id] in table 'Показания'
ALTER TABLE [dbo].[Показания]
ADD CONSTRAINT [FK_ПоказанияУслуги]
    FOREIGN KEY ([Услуги_Id])
    REFERENCES [dbo].[Услуги]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ПоказанияУслуги'
CREATE INDEX [IX_FK_ПоказанияУслуги]
ON [dbo].[Показания]
    ([Услуги_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------