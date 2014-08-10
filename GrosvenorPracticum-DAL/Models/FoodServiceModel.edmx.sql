
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/09/2014 22:56:47
-- Generated from EDMX file: D:\dev\GrosvenorPracticum\GrosvenorPracticum-DAL\Models\FoodServiceModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DishesDataBase];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_DishDayTimeDish]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DayTimeDishEntities] DROP CONSTRAINT [FK_DishDayTimeDish];
GO
IF OBJECT_ID(N'[dbo].[FK_DayTimeDayTimeDish]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DayTimeDishEntities] DROP CONSTRAINT [FK_DayTimeDayTimeDish];
GO
IF OBJECT_ID(N'[dbo].[FK_DishTypeEntityDishTypeDishEntity]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DishTypeDishEntities] DROP CONSTRAINT [FK_DishTypeEntityDishTypeDishEntity];
GO
IF OBJECT_ID(N'[dbo].[FK_DishEntityDishTypeDishEntity]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DishTypeDishEntities] DROP CONSTRAINT [FK_DishEntityDishTypeDishEntity];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[DishTypeEntities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DishTypeEntities];
GO
IF OBJECT_ID(N'[dbo].[DayTimeEntities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DayTimeEntities];
GO
IF OBJECT_ID(N'[dbo].[DishEntities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DishEntities];
GO
IF OBJECT_ID(N'[dbo].[DayTimeDishEntities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DayTimeDishEntities];
GO
IF OBJECT_ID(N'[dbo].[DishTypeDishEntities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DishTypeDishEntities];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'DishTypeEntities'
CREATE TABLE [dbo].[DishTypeEntities] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TypeName] nvarchar(max)  NOT NULL,
    [TypeId] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'DayTimeEntities'
CREATE TABLE [dbo].[DayTimeEntities] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TimeName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'DishEntities'
CREATE TABLE [dbo].[DishEntities] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'DayTimeDishEntities'
CREATE TABLE [dbo].[DayTimeDishEntities] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [IsMultiple] bit  NOT NULL,
    [Dish_Id] int  NOT NULL,
    [DayTime_Id] int  NOT NULL
);
GO

-- Creating table 'DishTypeDishEntities'
CREATE TABLE [dbo].[DishTypeDishEntities] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DishTypeEntity_Id] int  NOT NULL,
    [DishEntity_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'DishTypeEntities'
ALTER TABLE [dbo].[DishTypeEntities]
ADD CONSTRAINT [PK_DishTypeEntities]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DayTimeEntities'
ALTER TABLE [dbo].[DayTimeEntities]
ADD CONSTRAINT [PK_DayTimeEntities]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DishEntities'
ALTER TABLE [dbo].[DishEntities]
ADD CONSTRAINT [PK_DishEntities]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DayTimeDishEntities'
ALTER TABLE [dbo].[DayTimeDishEntities]
ADD CONSTRAINT [PK_DayTimeDishEntities]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DishTypeDishEntities'
ALTER TABLE [dbo].[DishTypeDishEntities]
ADD CONSTRAINT [PK_DishTypeDishEntities]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Dish_Id] in table 'DayTimeDishEntities'
ALTER TABLE [dbo].[DayTimeDishEntities]
ADD CONSTRAINT [FK_DishDayTimeDish]
    FOREIGN KEY ([Dish_Id])
    REFERENCES [dbo].[DishEntities]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DishDayTimeDish'
CREATE INDEX [IX_FK_DishDayTimeDish]
ON [dbo].[DayTimeDishEntities]
    ([Dish_Id]);
GO

-- Creating foreign key on [DayTime_Id] in table 'DayTimeDishEntities'
ALTER TABLE [dbo].[DayTimeDishEntities]
ADD CONSTRAINT [FK_DayTimeDayTimeDish]
    FOREIGN KEY ([DayTime_Id])
    REFERENCES [dbo].[DayTimeEntities]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DayTimeDayTimeDish'
CREATE INDEX [IX_FK_DayTimeDayTimeDish]
ON [dbo].[DayTimeDishEntities]
    ([DayTime_Id]);
GO

-- Creating foreign key on [DishTypeEntity_Id] in table 'DishTypeDishEntities'
ALTER TABLE [dbo].[DishTypeDishEntities]
ADD CONSTRAINT [FK_DishTypeEntityDishTypeDishEntity]
    FOREIGN KEY ([DishTypeEntity_Id])
    REFERENCES [dbo].[DishTypeEntities]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DishTypeEntityDishTypeDishEntity'
CREATE INDEX [IX_FK_DishTypeEntityDishTypeDishEntity]
ON [dbo].[DishTypeDishEntities]
    ([DishTypeEntity_Id]);
GO

-- Creating foreign key on [DishEntity_Id] in table 'DishTypeDishEntities'
ALTER TABLE [dbo].[DishTypeDishEntities]
ADD CONSTRAINT [FK_DishEntityDishTypeDishEntity]
    FOREIGN KEY ([DishEntity_Id])
    REFERENCES [dbo].[DishEntities]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DishEntityDishTypeDishEntity'
CREATE INDEX [IX_FK_DishEntityDishTypeDishEntity]
ON [dbo].[DishTypeDishEntities]
    ([DishEntity_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------