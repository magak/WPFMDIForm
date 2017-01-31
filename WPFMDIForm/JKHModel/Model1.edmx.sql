
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/31/2017 23:55:57
-- Generated from EDMX file: C:\WPFMDIForm\WPFMDIForm\WPFMDIForm\JKHModel\Model1.edmx
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


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Accruals'
CREATE TABLE [dbo].[Accruals] (
    [ID] int  NOT NULL,
    [Number] float  NOT NULL,
    [Date] datetime  NOT NULL,
    [Flat_ID] bigint  NOT NULL,
    [Service_ID] int  NOT NULL
);
GO

-- Creating table 'Accruals_no_benefits'
CREATE TABLE [dbo].[Accruals_no_benefits] (
    [ID] bigint  NOT NULL,
    [Number] float  NOT NULL,
    [Date] datetime  NOT NULL,
    [Flat_ID] bigint  NOT NULL,
    [Service_ID] int  NOT NULL
);
GO

-- Creating table 'Benefits'
CREATE TABLE [dbo].[Benefits] (
    [ID] int  NOT NULL,
    [Льгота] varchar(50)  NOT NULL,
    [Значение_льгота] int  NOT NULL,
    [Общая] binary(50)  NOT NULL,
    [Жильцы_Паспорт] bigint  NOT NULL
);
GO

-- Creating table 'Counter_Data'
CREATE TABLE [dbo].[Counter_Data] (
    [ID] bigint  NOT NULL,
    [Код_счетчика] int  NOT NULL,
    [Услуга_ID] int  NOT NULL,
    [Квартира_Номер] bigint  NOT NULL
);
GO

-- Creating table 'Expense'
CREATE TABLE [dbo].[Expense] (
    [ID] bigint  NOT NULL,
    [Service_ID] int  NOT NULL,
    [Number] int  NOT NULL
);
GO

-- Creating table 'Flat'
CREATE TABLE [dbo].[Flat] (
    [Номер] bigint  NOT NULL,
    [Water_Counters] binary(50)  NOT NULL,
    [Площадь_квартиры] float  NOT NULL,
    [Дом_ID] int  NOT NULL
);
GO

-- Creating table 'House'
CREATE TABLE [dbo].[House] (
    [ID] int  NOT NULL,
    [Адрес] varchar(50)  NOT NULL,
    [Площадь] decimal(18,0)  NOT NULL
);
GO

-- Creating table 'Occupant'
CREATE TABLE [dbo].[Occupant] (
    [Паспорт] bigint  NOT NULL,
    [ФИО] varchar(50)  NOT NULL
);
GO

-- Creating table 'Overall_accruals'
CREATE TABLE [dbo].[Overall_accruals] (
    [ID] int  NOT NULL,
    [Debt] binary(50)  NOT NULL,
    [Date] datetime  NOT NULL,
    [Service_ID] int  NOT NULL
);
GO

-- Creating table 'Services'
CREATE TABLE [dbo].[Services] (
    [ID] int  NOT NULL,
    [Вид_услуги] varchar(50)  NOT NULL,
    [Тариф] decimal(18,0)  NOT NULL,
    [ХВС_ГВС] varbinary(max)  NOT NULL,
    [Отопление] varbinary(max)  NOT NULL,
    [Водоотведение] varbinary(max)  NOT NULL,
    [Содерж_дома] varbinary(max)  NOT NULL
);
GO

-- Creating table 'Social_minimum'
CREATE TABLE [dbo].[Social_minimum] (
    [ID] int  NOT NULL,
    [Вид_нормы] nvarchar(max)  NOT NULL,
    [Значение_нормы] bigint  NOT NULL,
    [Услуга_ID] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'Accruals'
ALTER TABLE [dbo].[Accruals]
ADD CONSTRAINT [PK_Accruals]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Accruals_no_benefits'
ALTER TABLE [dbo].[Accruals_no_benefits]
ADD CONSTRAINT [PK_Accruals_no_benefits]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Benefits'
ALTER TABLE [dbo].[Benefits]
ADD CONSTRAINT [PK_Benefits]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Counter_Data'
ALTER TABLE [dbo].[Counter_Data]
ADD CONSTRAINT [PK_Counter_Data]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Expense'
ALTER TABLE [dbo].[Expense]
ADD CONSTRAINT [PK_Expense]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Номер] in table 'Flat'
ALTER TABLE [dbo].[Flat]
ADD CONSTRAINT [PK_Flat]
    PRIMARY KEY CLUSTERED ([Номер] ASC);
GO

-- Creating primary key on [ID] in table 'House'
ALTER TABLE [dbo].[House]
ADD CONSTRAINT [PK_House]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Паспорт] in table 'Occupant'
ALTER TABLE [dbo].[Occupant]
ADD CONSTRAINT [PK_Occupant]
    PRIMARY KEY CLUSTERED ([Паспорт] ASC);
GO

-- Creating primary key on [ID] in table 'Overall_accruals'
ALTER TABLE [dbo].[Overall_accruals]
ADD CONSTRAINT [PK_Overall_accruals]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Services'
ALTER TABLE [dbo].[Services]
ADD CONSTRAINT [PK_Services]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Social_minimum'
ALTER TABLE [dbo].[Social_minimum]
ADD CONSTRAINT [PK_Social_minimum]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Жильцы_Паспорт] in table 'Benefits'
ALTER TABLE [dbo].[Benefits]
ADD CONSTRAINT [FK_ЖильцыЛьготы]
    FOREIGN KEY ([Жильцы_Паспорт])
    REFERENCES [dbo].[Occupant]
        ([Паспорт])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ЖильцыЛьготы'
CREATE INDEX [IX_FK_ЖильцыЛьготы]
ON [dbo].[Benefits]
    ([Жильцы_Паспорт]);
GO

-- Creating foreign key on [Услуга_ID] in table 'Counter_Data'
ALTER TABLE [dbo].[Counter_Data]
ADD CONSTRAINT [FK_УслугаСчетчики]
    FOREIGN KEY ([Услуга_ID])
    REFERENCES [dbo].[Services]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_УслугаСчетчики'
CREATE INDEX [IX_FK_УслугаСчетчики]
ON [dbo].[Counter_Data]
    ([Услуга_ID]);
GO

-- Creating foreign key on [Квартира_Номер] in table 'Counter_Data'
ALTER TABLE [dbo].[Counter_Data]
ADD CONSTRAINT [FK_КвартираСчетчики]
    FOREIGN KEY ([Квартира_Номер])
    REFERENCES [dbo].[Flat]
        ([Номер])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_КвартираСчетчики'
CREATE INDEX [IX_FK_КвартираСчетчики]
ON [dbo].[Counter_Data]
    ([Квартира_Номер]);
GO

-- Creating foreign key on [Дом_ID] in table 'Flat'
ALTER TABLE [dbo].[Flat]
ADD CONSTRAINT [FK_ДомКвартира]
    FOREIGN KEY ([Дом_ID])
    REFERENCES [dbo].[House]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ДомКвартира'
CREATE INDEX [IX_FK_ДомКвартира]
ON [dbo].[Flat]
    ([Дом_ID]);
GO

-- Creating foreign key on [Услуга_ID] in table 'Social_minimum'
ALTER TABLE [dbo].[Social_minimum]
ADD CONSTRAINT [FK_УслугаСоц_норма]
    FOREIGN KEY ([Услуга_ID])
    REFERENCES [dbo].[Services]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_УслугаСоц_норма'
CREATE INDEX [IX_FK_УслугаСоц_норма]
ON [dbo].[Social_minimum]
    ([Услуга_ID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------