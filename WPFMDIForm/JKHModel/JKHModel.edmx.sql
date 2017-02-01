
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/01/2017 22:47:10
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

IF OBJECT_ID(N'[dbo].[FK_КвартираСчетчик]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[СчетчикSet] DROP CONSTRAINT [FK_КвартираСчетчик];
GO
IF OBJECT_ID(N'[dbo].[FK_КвартираЖилец]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ЖилецSet] DROP CONSTRAINT [FK_КвартираЖилец];
GO
IF OBJECT_ID(N'[dbo].[FK_УслугаСчетчик]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[СчетчикSet] DROP CONSTRAINT [FK_УслугаСчетчик];
GO
IF OBJECT_ID(N'[dbo].[FK_УслугаСоц_норма]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Соц_нормаSet] DROP CONSTRAINT [FK_УслугаСоц_норма];
GO
IF OBJECT_ID(N'[dbo].[FK_ЖилецЛьгота_Жилец]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ЖилецЛьгота] DROP CONSTRAINT [FK_ЖилецЛьгота_Жилец];
GO
IF OBJECT_ID(N'[dbo].[FK_ЖилецЛьгота_Льгота]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ЖилецЛьгота] DROP CONSTRAINT [FK_ЖилецЛьгота_Льгота];
GO
IF OBJECT_ID(N'[dbo].[FK_КалендарьПоказания_квартир]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Показания_квартирSet] DROP CONSTRAINT [FK_КалендарьПоказания_квартир];
GO
IF OBJECT_ID(N'[dbo].[FK_КалендарьПоказания_ОДУ]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Показания_ОДУSet] DROP CONSTRAINT [FK_КалендарьПоказания_ОДУ];
GO
IF OBJECT_ID(N'[dbo].[FK_ДомКвартира]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[КвартираSet] DROP CONSTRAINT [FK_ДомКвартира];
GO
IF OBJECT_ID(N'[dbo].[FK_ДомПоказания_ОДУ]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Показания_ОДУSet] DROP CONSTRAINT [FK_ДомПоказания_ОДУ];
GO
IF OBJECT_ID(N'[dbo].[FK_СчетчикПоказания_квартир]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Показания_квартирSet] DROP CONSTRAINT [FK_СчетчикПоказания_квартир];
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
IF OBJECT_ID(N'[dbo].[УслугаSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[УслугаSet];
GO
IF OBJECT_ID(N'[dbo].[Соц_нормаSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Соц_нормаSet];
GO
IF OBJECT_ID(N'[dbo].[ЛьготаSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ЛьготаSet];
GO
IF OBJECT_ID(N'[dbo].[Показания_квартирSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Показания_квартирSet];
GO
IF OBJECT_ID(N'[dbo].[КалендарьSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[КалендарьSet];
GO
IF OBJECT_ID(N'[dbo].[Показания_ОДУSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Показания_ОДУSet];
GO
IF OBJECT_ID(N'[dbo].[ЖилецЛьгота]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ЖилецЛьгота];
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
    [Id] int IDENTITY(1,1) NOT NULL,
    [Площадь_квартиры] decimal(18,0)  NOT NULL,
    [Счетчики_установлены] bit  NOT NULL,
    [Номер_квартиры] smallint  NOT NULL,
    [Дом_Id] int  NOT NULL
);
GO

-- Creating table 'СчетчикSet'
CREATE TABLE [dbo].[СчетчикSet] (
    [Код_счетчика] int IDENTITY(1,1) NOT NULL,
    [Квартира_Id] int  NOT NULL,
    [Услуга_Id] int  NOT NULL
);
GO

-- Creating table 'ЖилецSet'
CREATE TABLE [dbo].[ЖилецSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ФИО] nvarchar(max)  NOT NULL,
    [Номер_паспорта] nvarchar(max)  NOT NULL,
    [Квартира_Id] int  NOT NULL
);
GO

-- Creating table 'УслугаSet'
CREATE TABLE [dbo].[УслугаSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Вид_услуги] nvarchar(max)  NOT NULL,
    [Тариф] decimal(18,0)  NOT NULL,
    [ГВС] bit  NOT NULL,
    [Отопление] bit  NOT NULL,
    [Водоотведение] bit  NOT NULL,
    [Содерж_дома] bit  NOT NULL,
    [ХВС] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Соц_нормаSet'
CREATE TABLE [dbo].[Соц_нормаSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Вид_нормы] nvarchar(max)  NOT NULL,
    [Значение_нормы] decimal(18,0)  NOT NULL,
    [Услуга_Id] int  NOT NULL
);
GO

-- Creating table 'ЛьготаSet'
CREATE TABLE [dbo].[ЛьготаSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Имя_льготы] nvarchar(max)  NOT NULL,
    [Значение_льготы] decimal(18,0)  NOT NULL,
    [Общая] bit  NOT NULL
);
GO

-- Creating table 'Показания_квартирSet'
CREATE TABLE [dbo].[Показания_квартирSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Значение_показания_кв] smallint  NOT NULL,
    [Календарь_Id] int  NOT NULL,
    [Счетчик_Код_счетчика] int  NOT NULL
);
GO

-- Creating table 'КалендарьSet'
CREATE TABLE [dbo].[КалендарьSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Месяц] nvarchar(max)  NOT NULL,
    [Год] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Показания_ОДУSet'
CREATE TABLE [dbo].[Показания_ОДУSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Показание_ГВС] nvarchar(max)  NOT NULL,
    [Показание_ХВС] nvarchar(max)  NOT NULL,
    [Календарь_Id] int  NOT NULL,
    [Дом_Id] int  NOT NULL
);
GO

-- Creating table 'ЖилецЛьгота'
CREATE TABLE [dbo].[ЖилецЛьгота] (
    [Жилец_Id] int  NOT NULL,
    [Льгота_Id] int  NOT NULL
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

-- Creating primary key on [Id] in table 'КвартираSet'
ALTER TABLE [dbo].[КвартираSet]
ADD CONSTRAINT [PK_КвартираSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
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

-- Creating primary key on [Id] in table 'УслугаSet'
ALTER TABLE [dbo].[УслугаSet]
ADD CONSTRAINT [PK_УслугаSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Соц_нормаSet'
ALTER TABLE [dbo].[Соц_нормаSet]
ADD CONSTRAINT [PK_Соц_нормаSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ЛьготаSet'
ALTER TABLE [dbo].[ЛьготаSet]
ADD CONSTRAINT [PK_ЛьготаSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Показания_квартирSet'
ALTER TABLE [dbo].[Показания_квартирSet]
ADD CONSTRAINT [PK_Показания_квартирSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'КалендарьSet'
ALTER TABLE [dbo].[КалендарьSet]
ADD CONSTRAINT [PK_КалендарьSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Показания_ОДУSet'
ALTER TABLE [dbo].[Показания_ОДУSet]
ADD CONSTRAINT [PK_Показания_ОДУSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Жилец_Id], [Льгота_Id] in table 'ЖилецЛьгота'
ALTER TABLE [dbo].[ЖилецЛьгота]
ADD CONSTRAINT [PK_ЖилецЛьгота]
    PRIMARY KEY CLUSTERED ([Жилец_Id], [Льгота_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Квартира_Id] in table 'СчетчикSet'
ALTER TABLE [dbo].[СчетчикSet]
ADD CONSTRAINT [FK_КвартираСчетчик]
    FOREIGN KEY ([Квартира_Id])
    REFERENCES [dbo].[КвартираSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_КвартираСчетчик'
CREATE INDEX [IX_FK_КвартираСчетчик]
ON [dbo].[СчетчикSet]
    ([Квартира_Id]);
GO

-- Creating foreign key on [Квартира_Id] in table 'ЖилецSet'
ALTER TABLE [dbo].[ЖилецSet]
ADD CONSTRAINT [FK_КвартираЖилец]
    FOREIGN KEY ([Квартира_Id])
    REFERENCES [dbo].[КвартираSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_КвартираЖилец'
CREATE INDEX [IX_FK_КвартираЖилец]
ON [dbo].[ЖилецSet]
    ([Квартира_Id]);
GO

-- Creating foreign key on [Услуга_Id] in table 'СчетчикSet'
ALTER TABLE [dbo].[СчетчикSet]
ADD CONSTRAINT [FK_УслугаСчетчик]
    FOREIGN KEY ([Услуга_Id])
    REFERENCES [dbo].[УслугаSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_УслугаСчетчик'
CREATE INDEX [IX_FK_УслугаСчетчик]
ON [dbo].[СчетчикSet]
    ([Услуга_Id]);
GO

-- Creating foreign key on [Услуга_Id] in table 'Соц_нормаSet'
ALTER TABLE [dbo].[Соц_нормаSet]
ADD CONSTRAINT [FK_УслугаСоц_норма]
    FOREIGN KEY ([Услуга_Id])
    REFERENCES [dbo].[УслугаSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_УслугаСоц_норма'
CREATE INDEX [IX_FK_УслугаСоц_норма]
ON [dbo].[Соц_нормаSet]
    ([Услуга_Id]);
GO

-- Creating foreign key on [Жилец_Id] in table 'ЖилецЛьгота'
ALTER TABLE [dbo].[ЖилецЛьгота]
ADD CONSTRAINT [FK_ЖилецЛьгота_Жилец]
    FOREIGN KEY ([Жилец_Id])
    REFERENCES [dbo].[ЖилецSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Льгота_Id] in table 'ЖилецЛьгота'
ALTER TABLE [dbo].[ЖилецЛьгота]
ADD CONSTRAINT [FK_ЖилецЛьгота_Льгота]
    FOREIGN KEY ([Льгота_Id])
    REFERENCES [dbo].[ЛьготаSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ЖилецЛьгота_Льгота'
CREATE INDEX [IX_FK_ЖилецЛьгота_Льгота]
ON [dbo].[ЖилецЛьгота]
    ([Льгота_Id]);
GO

-- Creating foreign key on [Календарь_Id] in table 'Показания_квартирSet'
ALTER TABLE [dbo].[Показания_квартирSet]
ADD CONSTRAINT [FK_КалендарьПоказания_квартир]
    FOREIGN KEY ([Календарь_Id])
    REFERENCES [dbo].[КалендарьSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_КалендарьПоказания_квартир'
CREATE INDEX [IX_FK_КалендарьПоказания_квартир]
ON [dbo].[Показания_квартирSet]
    ([Календарь_Id]);
GO

-- Creating foreign key on [Календарь_Id] in table 'Показания_ОДУSet'
ALTER TABLE [dbo].[Показания_ОДУSet]
ADD CONSTRAINT [FK_КалендарьПоказания_ОДУ]
    FOREIGN KEY ([Календарь_Id])
    REFERENCES [dbo].[КалендарьSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_КалендарьПоказания_ОДУ'
CREATE INDEX [IX_FK_КалендарьПоказания_ОДУ]
ON [dbo].[Показания_ОДУSet]
    ([Календарь_Id]);
GO

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

-- Creating foreign key on [Дом_Id] in table 'Показания_ОДУSet'
ALTER TABLE [dbo].[Показания_ОДУSet]
ADD CONSTRAINT [FK_ДомПоказания_ОДУ]
    FOREIGN KEY ([Дом_Id])
    REFERENCES [dbo].[ДомSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ДомПоказания_ОДУ'
CREATE INDEX [IX_FK_ДомПоказания_ОДУ]
ON [dbo].[Показания_ОДУSet]
    ([Дом_Id]);
GO

-- Creating foreign key on [Счетчик_Код_счетчика] in table 'Показания_квартирSet'
ALTER TABLE [dbo].[Показания_квартирSet]
ADD CONSTRAINT [FK_СчетчикПоказания_квартир]
    FOREIGN KEY ([Счетчик_Код_счетчика])
    REFERENCES [dbo].[СчетчикSet]
        ([Код_счетчика])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_СчетчикПоказания_квартир'
CREATE INDEX [IX_FK_СчетчикПоказания_квартир]
ON [dbo].[Показания_квартирSet]
    ([Счетчик_Код_счетчика]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------