
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/17/2017 12:27:11
-- Generated from EDMX file: C:\Users\Tobias\Source\Repos\ZooDagsrapport2\AalborgZooProjekt\AalborgZooProjekt\AalborgZoo.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [AalborgZooFoder];
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

-- Creating table 'EmployeeSet'
CREATE TABLE [dbo].[EmployeeSet] (
    [Id] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'ShoppinglistSet'
CREATE TABLE [dbo].[ShoppinglistSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Shopper_Id] int  NOT NULL
);
GO

-- Creating table 'OrderlistSet'
CREATE TABLE [dbo].[OrderlistSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ShoppinglistId] int  NOT NULL,
    [ZookeeperId] int  NOT NULL,
    [DepartmentId] int  NOT NULL
);
GO

-- Creating table 'OrderSet'
CREATE TABLE [dbo].[OrderSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [OrderlistId] int  NOT NULL,
    [ProductId] int  NOT NULL
);
GO

-- Creating table 'ProductSet'
CREATE TABLE [dbo].[ProductSet] (
    [Id] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'DepartmentSet'
CREATE TABLE [dbo].[DepartmentSet] (
    [Id] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'EmployeeSet_Shopper'
CREATE TABLE [dbo].[EmployeeSet_Shopper] (
    [Id] int  NOT NULL
);
GO

-- Creating table 'EmployeeSet_Zookeeper'
CREATE TABLE [dbo].[EmployeeSet_Zookeeper] (
    [DepartmentId] int  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'DepartmentProduct'
CREATE TABLE [dbo].[DepartmentProduct] (
    [Departments_Id] int  NOT NULL,
    [Products_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'EmployeeSet'
ALTER TABLE [dbo].[EmployeeSet]
ADD CONSTRAINT [PK_EmployeeSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ShoppinglistSet'
ALTER TABLE [dbo].[ShoppinglistSet]
ADD CONSTRAINT [PK_ShoppinglistSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrderlistSet'
ALTER TABLE [dbo].[OrderlistSet]
ADD CONSTRAINT [PK_OrderlistSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrderSet'
ALTER TABLE [dbo].[OrderSet]
ADD CONSTRAINT [PK_OrderSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProductSet'
ALTER TABLE [dbo].[ProductSet]
ADD CONSTRAINT [PK_ProductSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DepartmentSet'
ALTER TABLE [dbo].[DepartmentSet]
ADD CONSTRAINT [PK_DepartmentSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EmployeeSet_Shopper'
ALTER TABLE [dbo].[EmployeeSet_Shopper]
ADD CONSTRAINT [PK_EmployeeSet_Shopper]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EmployeeSet_Zookeeper'
ALTER TABLE [dbo].[EmployeeSet_Zookeeper]
ADD CONSTRAINT [PK_EmployeeSet_Zookeeper]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Departments_Id], [Products_Id] in table 'DepartmentProduct'
ALTER TABLE [dbo].[DepartmentProduct]
ADD CONSTRAINT [PK_DepartmentProduct]
    PRIMARY KEY CLUSTERED ([Departments_Id], [Products_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Shopper_Id] in table 'ShoppinglistSet'
ALTER TABLE [dbo].[ShoppinglistSet]
ADD CONSTRAINT [FK_ShoppinglistShopper]
    FOREIGN KEY ([Shopper_Id])
    REFERENCES [dbo].[EmployeeSet_Shopper]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ShoppinglistShopper'
CREATE INDEX [IX_FK_ShoppinglistShopper]
ON [dbo].[ShoppinglistSet]
    ([Shopper_Id]);
GO

-- Creating foreign key on [ShoppinglistId] in table 'OrderlistSet'
ALTER TABLE [dbo].[OrderlistSet]
ADD CONSTRAINT [FK_OrderlistShoppinglist]
    FOREIGN KEY ([ShoppinglistId])
    REFERENCES [dbo].[ShoppinglistSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderlistShoppinglist'
CREATE INDEX [IX_FK_OrderlistShoppinglist]
ON [dbo].[OrderlistSet]
    ([ShoppinglistId]);
GO

-- Creating foreign key on [OrderlistId] in table 'OrderSet'
ALTER TABLE [dbo].[OrderSet]
ADD CONSTRAINT [FK_OrderOrderlist]
    FOREIGN KEY ([OrderlistId])
    REFERENCES [dbo].[OrderlistSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderOrderlist'
CREATE INDEX [IX_FK_OrderOrderlist]
ON [dbo].[OrderSet]
    ([OrderlistId]);
GO

-- Creating foreign key on [ZookeeperId] in table 'OrderlistSet'
ALTER TABLE [dbo].[OrderlistSet]
ADD CONSTRAINT [FK_OrderlistZookeeper]
    FOREIGN KEY ([ZookeeperId])
    REFERENCES [dbo].[EmployeeSet_Zookeeper]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderlistZookeeper'
CREATE INDEX [IX_FK_OrderlistZookeeper]
ON [dbo].[OrderlistSet]
    ([ZookeeperId]);
GO

-- Creating foreign key on [ProductId] in table 'OrderSet'
ALTER TABLE [dbo].[OrderSet]
ADD CONSTRAINT [FK_ProductOrder]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[ProductSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductOrder'
CREATE INDEX [IX_FK_ProductOrder]
ON [dbo].[OrderSet]
    ([ProductId]);
GO

-- Creating foreign key on [DepartmentId] in table 'EmployeeSet_Zookeeper'
ALTER TABLE [dbo].[EmployeeSet_Zookeeper]
ADD CONSTRAINT [FK_DepartmentZookeeper]
    FOREIGN KEY ([DepartmentId])
    REFERENCES [dbo].[DepartmentSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DepartmentZookeeper'
CREATE INDEX [IX_FK_DepartmentZookeeper]
ON [dbo].[EmployeeSet_Zookeeper]
    ([DepartmentId]);
GO

-- Creating foreign key on [Departments_Id] in table 'DepartmentProduct'
ALTER TABLE [dbo].[DepartmentProduct]
ADD CONSTRAINT [FK_DepartmentProduct_Department]
    FOREIGN KEY ([Departments_Id])
    REFERENCES [dbo].[DepartmentSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Products_Id] in table 'DepartmentProduct'
ALTER TABLE [dbo].[DepartmentProduct]
ADD CONSTRAINT [FK_DepartmentProduct_Product]
    FOREIGN KEY ([Products_Id])
    REFERENCES [dbo].[ProductSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DepartmentProduct_Product'
CREATE INDEX [IX_FK_DepartmentProduct_Product]
ON [dbo].[DepartmentProduct]
    ([Products_Id]);
GO

-- Creating foreign key on [DepartmentId] in table 'OrderlistSet'
ALTER TABLE [dbo].[OrderlistSet]
ADD CONSTRAINT [FK_DepartmentOrderlist]
    FOREIGN KEY ([DepartmentId])
    REFERENCES [dbo].[DepartmentSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DepartmentOrderlist'
CREATE INDEX [IX_FK_DepartmentOrderlist]
ON [dbo].[OrderlistSet]
    ([DepartmentId]);
GO

-- Creating foreign key on [Id] in table 'EmployeeSet_Shopper'
ALTER TABLE [dbo].[EmployeeSet_Shopper]
ADD CONSTRAINT [FK_Shopper_inherits_Employee]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[EmployeeSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'EmployeeSet_Zookeeper'
ALTER TABLE [dbo].[EmployeeSet_Zookeeper]
ADD CONSTRAINT [FK_Zookeeper_inherits_Employee]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[EmployeeSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------