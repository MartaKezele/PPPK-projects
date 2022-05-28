
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/15/2022 18:01:59
-- Generated from EDMX file: C:\Users\Marta\Desktop\Algebra\5.semestar\PPPK\Zadace\DZ_4\PPPK_DZ4\PPPK_DZ4\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Movies];
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

-- Creating table 'Movies'
CREATE TABLE [dbo].[Movies] (
    [IDMovie] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(100)  NOT NULL,
    [Description] nvarchar(300)  NOT NULL
);
GO

-- Creating table 'UploadedFiles'
CREATE TABLE [dbo].[UploadedFiles] (
    [IDUploadedFile] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(100)  NOT NULL,
    [ContentType] nvarchar(50)  NOT NULL,
    [Content] varbinary(max)  NOT NULL,
    [MovieIDMovie] int  NOT NULL
);
GO

-- Creating table 'Actors'
CREATE TABLE [dbo].[Actors] (
    [IDActor] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(100)  NOT NULL,
    [LastName] nvarchar(100)  NOT NULL
);
GO

-- Creating table 'Directors'
CREATE TABLE [dbo].[Directors] (
    [IDDirector] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(100)  NOT NULL,
    [LastName] nvarchar(100)  NOT NULL
);
GO

-- Creating table 'MovieActor'
CREATE TABLE [dbo].[MovieActor] (
    [Movies_IDMovie] int  NOT NULL,
    [Actors_IDActor] int  NOT NULL
);
GO

-- Creating table 'MovieDirector'
CREATE TABLE [dbo].[MovieDirector] (
    [Movies_IDMovie] int  NOT NULL,
    [Directors_IDDirector] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [IDMovie] in table 'Movies'
ALTER TABLE [dbo].[Movies]
ADD CONSTRAINT [PK_Movies]
    PRIMARY KEY CLUSTERED ([IDMovie] ASC);
GO

-- Creating primary key on [IDUploadedFile] in table 'UploadedFiles'
ALTER TABLE [dbo].[UploadedFiles]
ADD CONSTRAINT [PK_UploadedFiles]
    PRIMARY KEY CLUSTERED ([IDUploadedFile] ASC);
GO

-- Creating primary key on [IDActor] in table 'Actors'
ALTER TABLE [dbo].[Actors]
ADD CONSTRAINT [PK_Actors]
    PRIMARY KEY CLUSTERED ([IDActor] ASC);
GO

-- Creating primary key on [IDDirector] in table 'Directors'
ALTER TABLE [dbo].[Directors]
ADD CONSTRAINT [PK_Directors]
    PRIMARY KEY CLUSTERED ([IDDirector] ASC);
GO

-- Creating primary key on [Movies_IDMovie], [Actors_IDActor] in table 'MovieActor'
ALTER TABLE [dbo].[MovieActor]
ADD CONSTRAINT [PK_MovieActor]
    PRIMARY KEY CLUSTERED ([Movies_IDMovie], [Actors_IDActor] ASC);
GO

-- Creating primary key on [Movies_IDMovie], [Directors_IDDirector] in table 'MovieDirector'
ALTER TABLE [dbo].[MovieDirector]
ADD CONSTRAINT [PK_MovieDirector]
    PRIMARY KEY CLUSTERED ([Movies_IDMovie], [Directors_IDDirector] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [MovieIDMovie] in table 'UploadedFiles'
ALTER TABLE [dbo].[UploadedFiles]
ADD CONSTRAINT [FK_MovieUploadedFile]
    FOREIGN KEY ([MovieIDMovie])
    REFERENCES [dbo].[Movies]
        ([IDMovie])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MovieUploadedFile'
CREATE INDEX [IX_FK_MovieUploadedFile]
ON [dbo].[UploadedFiles]
    ([MovieIDMovie]);
GO

-- Creating foreign key on [Movies_IDMovie] in table 'MovieActor'
ALTER TABLE [dbo].[MovieActor]
ADD CONSTRAINT [FK_MovieActor_Movie]
    FOREIGN KEY ([Movies_IDMovie])
    REFERENCES [dbo].[Movies]
        ([IDMovie])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Actors_IDActor] in table 'MovieActor'
ALTER TABLE [dbo].[MovieActor]
ADD CONSTRAINT [FK_MovieActor_Actor]
    FOREIGN KEY ([Actors_IDActor])
    REFERENCES [dbo].[Actors]
        ([IDActor])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MovieActor_Actor'
CREATE INDEX [IX_FK_MovieActor_Actor]
ON [dbo].[MovieActor]
    ([Actors_IDActor]);
GO

-- Creating foreign key on [Movies_IDMovie] in table 'MovieDirector'
ALTER TABLE [dbo].[MovieDirector]
ADD CONSTRAINT [FK_MovieDirector_Movie]
    FOREIGN KEY ([Movies_IDMovie])
    REFERENCES [dbo].[Movies]
        ([IDMovie])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Directors_IDDirector] in table 'MovieDirector'
ALTER TABLE [dbo].[MovieDirector]
ADD CONSTRAINT [FK_MovieDirector_Director]
    FOREIGN KEY ([Directors_IDDirector])
    REFERENCES [dbo].[Directors]
        ([IDDirector])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MovieDirector_Director'
CREATE INDEX [IX_FK_MovieDirector_Director]
ON [dbo].[MovieDirector]
    ([Directors_IDDirector]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------