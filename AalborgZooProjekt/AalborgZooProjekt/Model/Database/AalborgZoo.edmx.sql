
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/14/2017 10:22:55
-- Generated from EDMX file: C:\Users\Tobias\Source\Repos\ZooDagsrapport2\AalborgZooProjekt\AalborgZooProjekt\Model\Database\AalborgZoo.edmx
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

IF OBJECT_ID(N'[dbo].[FK_DepartmentSpecifikProductProduct]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DepartmentSpecificProductSet] DROP CONSTRAINT [FK_DepartmentSpecifikProductProduct];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductProductVersion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductVersionSet] DROP CONSTRAINT [FK_ProductProductVersion];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductVersionOrderLine]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderLineSet] DROP CONSTRAINT [FK_ProductVersionOrderLine];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderLineOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderLineSet] DROP CONSTRAINT [FK_OrderLineOrder];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderShoppingList]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderSet] DROP CONSTRAINT [FK_OrderShoppingList];
GO
IF OBJECT_ID(N'[dbo].[FK_UnitProductVersion_Unit]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UnitProductVersion] DROP CONSTRAINT [FK_UnitProductVersion_Unit];
GO
IF OBJECT_ID(N'[dbo].[FK_UnitProductVersion_ProductVersion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UnitProductVersion] DROP CONSTRAINT [FK_UnitProductVersion_ProductVersion];
GO
IF OBJECT_ID(N'[dbo].[FK_DepartmentSpecifikProductDepartment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DepartmentSpecificProductSet] DROP CONSTRAINT [FK_DepartmentSpecifikProductDepartment];
GO
IF OBJECT_ID(N'[dbo].[FK_DepartmentZookeeper]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeSet_Zookeeper] DROP CONSTRAINT [FK_DepartmentZookeeper];
GO
IF OBJECT_ID(N'[dbo].[FK_DepartmentChangeZookeeper]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DepartmentChangeSet] DROP CONSTRAINT [FK_DepartmentChangeZookeeper];
GO
IF OBJECT_ID(N'[dbo].[FK_ShopperShoppingList]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ShoppingListSet] DROP CONSTRAINT [FK_ShopperShoppingList];
GO
IF OBJECT_ID(N'[dbo].[FK_PasswordChangedShopper]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PasswordChangedSet] DROP CONSTRAINT [FK_PasswordChangedShopper];
GO
IF OBJECT_ID(N'[dbo].[FK_Zookeeper_inherits_Employee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeSet_Zookeeper] DROP CONSTRAINT [FK_Zookeeper_inherits_Employee];
GO
IF OBJECT_ID(N'[dbo].[FK_Shopper_inherits_Employee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeSet_Shopper] DROP CONSTRAINT [FK_Shopper_inherits_Employee];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[DepartmentSpecificProductSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DepartmentSpecificProductSet];
GO
IF OBJECT_ID(N'[dbo].[ProductSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductSet];
GO
IF OBJECT_ID(N'[dbo].[ProductVersionSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductVersionSet];
GO
IF OBJECT_ID(N'[dbo].[ShoppingListSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ShoppingListSet];
GO
IF OBJECT_ID(N'[dbo].[OrderSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderSet];
GO
IF OBJECT_ID(N'[dbo].[OrderLineSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderLineSet];
GO
IF OBJECT_ID(N'[dbo].[DepartmentSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DepartmentSet];
GO
IF OBJECT_ID(N'[dbo].[DepartmentChangeSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DepartmentChangeSet];
GO
IF OBJECT_ID(N'[dbo].[EmployeeSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeeSet];
GO
IF OBJECT_ID(N'[dbo].[PasswordChangedSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PasswordChangedSet];
GO
IF OBJECT_ID(N'[dbo].[UnitSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UnitSet];
GO
IF OBJECT_ID(N'[dbo].[EmployeeSet_Zookeeper]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeeSet_Zookeeper];
GO
IF OBJECT_ID(N'[dbo].[EmployeeSet_Shopper]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeeSet_Shopper];
GO
IF OBJECT_ID(N'[dbo].[UnitProductVersion]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UnitProductVersion];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'DepartmentSpecificProductSet'
CREATE TABLE [dbo].[DepartmentSpecificProductSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ProductId] int  NOT NULL,
    [DepartmentId] int  NOT NULL
);
GO

-- Creating table 'ProductSet'
CREATE TABLE [dbo].[ProductSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DateCreated] datetime  NOT NULL,
    [CreatedByID] int  NOT NULL,
    [DeletedByID] int  NULL,
    [DateDeleted] datetime  NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ProductVersionSet'
CREATE TABLE [dbo].[ProductVersionSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [IsActive] bit  NOT NULL,
    [Supplier] nvarchar(max)  NOT NULL,
    [CreatedByID] int  NOT NULL,
    [DateCreated] datetime  NOT NULL,
    [ProductId] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ShoppingListSet'
CREATE TABLE [dbo].[ShoppingListSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CreatedByID] int  NOT NULL,
    [DateCreated] datetime  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [ShopperId] int  NOT NULL
);
GO

-- Creating table 'OrderSet'
CREATE TABLE [dbo].[OrderSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DepartmentID] int  NOT NULL,
    [OrderedByID] int  NOT NULL,
    [DateOrdered] datetime  NOT NULL,
    [DateCancelled] datetime  NULL,
    [Note] nvarchar(max)  NULL,
    [DateCreated] datetime  NOT NULL,
    [DeletedByID] int  NULL,
    [Status] nvarchar(max)  NOT NULL,
    [ShoppingListId] int  NULL
);
GO

-- Creating table 'OrderLineSet'
CREATE TABLE [dbo].[OrderLineSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Quantity] int  NOT NULL,
    [UnitID] int  NOT NULL,
    [ProductVersionId] int  NOT NULL,
    [OrderId] int  NOT NULL
);
GO

-- Creating table 'DepartmentSet'
CREATE TABLE [dbo].[DepartmentSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [DateCreated] datetime  NOT NULL,
    [DateDeleted] datetime  NULL
);
GO

-- Creating table 'DepartmentChangeSet'
CREATE TABLE [dbo].[DepartmentChangeSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DepartmentID] nvarchar(max)  NOT NULL,
    [DateChanged] datetime  NOT NULL,
    [ZookeeperID] int  NULL
);
GO

-- Creating table 'EmployeeSet'
CREATE TABLE [dbo].[EmployeeSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DateHired] datetime  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [DateStopped] datetime  NULL
);
GO

-- Creating table 'PasswordChangedSet'
CREATE TABLE [dbo].[PasswordChangedSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DateChanged] datetime  NOT NULL,
    [ShopperId] int  NULL
);
GO

-- Creating table 'UnitSet'
CREATE TABLE [dbo].[UnitSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'EmployeeSet_Zookeeper'
CREATE TABLE [dbo].[EmployeeSet_Zookeeper] (
    [DepartmentId] int  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'EmployeeSet_Shopper'
CREATE TABLE [dbo].[EmployeeSet_Shopper] (
    [Username] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'UnitProductVersion'
CREATE TABLE [dbo].[UnitProductVersion] (
    [Unit_Id] int  NOT NULL,
    [ProductVersion_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'DepartmentSpecificProductSet'
ALTER TABLE [dbo].[DepartmentSpecificProductSet]
ADD CONSTRAINT [PK_DepartmentSpecificProductSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProductSet'
ALTER TABLE [dbo].[ProductSet]
ADD CONSTRAINT [PK_ProductSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProductVersionSet'
ALTER TABLE [dbo].[ProductVersionSet]
ADD CONSTRAINT [PK_ProductVersionSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ShoppingListSet'
ALTER TABLE [dbo].[ShoppingListSet]
ADD CONSTRAINT [PK_ShoppingListSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrderSet'
ALTER TABLE [dbo].[OrderSet]
ADD CONSTRAINT [PK_OrderSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrderLineSet'
ALTER TABLE [dbo].[OrderLineSet]
ADD CONSTRAINT [PK_OrderLineSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DepartmentSet'
ALTER TABLE [dbo].[DepartmentSet]
ADD CONSTRAINT [PK_DepartmentSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DepartmentChangeSet'
ALTER TABLE [dbo].[DepartmentChangeSet]
ADD CONSTRAINT [PK_DepartmentChangeSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EmployeeSet'
ALTER TABLE [dbo].[EmployeeSet]
ADD CONSTRAINT [PK_EmployeeSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PasswordChangedSet'
ALTER TABLE [dbo].[PasswordChangedSet]
ADD CONSTRAINT [PK_PasswordChangedSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UnitSet'
ALTER TABLE [dbo].[UnitSet]
ADD CONSTRAINT [PK_UnitSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EmployeeSet_Zookeeper'
ALTER TABLE [dbo].[EmployeeSet_Zookeeper]
ADD CONSTRAINT [PK_EmployeeSet_Zookeeper]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EmployeeSet_Shopper'
ALTER TABLE [dbo].[EmployeeSet_Shopper]
ADD CONSTRAINT [PK_EmployeeSet_Shopper]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Unit_Id], [ProductVersion_Id] in table 'UnitProductVersion'
ALTER TABLE [dbo].[UnitProductVersion]
ADD CONSTRAINT [PK_UnitProductVersion]
    PRIMARY KEY CLUSTERED ([Unit_Id], [ProductVersion_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ProductId] in table 'DepartmentSpecificProductSet'
ALTER TABLE [dbo].[DepartmentSpecificProductSet]
ADD CONSTRAINT [FK_DepartmentSpecifikProductProduct]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[ProductSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DepartmentSpecifikProductProduct'
CREATE INDEX [IX_FK_DepartmentSpecifikProductProduct]
ON [dbo].[DepartmentSpecificProductSet]
    ([ProductId]);
GO

-- Creating foreign key on [ProductId] in table 'ProductVersionSet'
ALTER TABLE [dbo].[ProductVersionSet]
ADD CONSTRAINT [FK_ProductProductVersion]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[ProductSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductProductVersion'
CREATE INDEX [IX_FK_ProductProductVersion]
ON [dbo].[ProductVersionSet]
    ([ProductId]);
GO

-- Creating foreign key on [ProductVersionId] in table 'OrderLineSet'
ALTER TABLE [dbo].[OrderLineSet]
ADD CONSTRAINT [FK_ProductVersionOrderLine]
    FOREIGN KEY ([ProductVersionId])
    REFERENCES [dbo].[ProductVersionSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductVersionOrderLine'
CREATE INDEX [IX_FK_ProductVersionOrderLine]
ON [dbo].[OrderLineSet]
    ([ProductVersionId]);
GO

-- Creating foreign key on [OrderId] in table 'OrderLineSet'
ALTER TABLE [dbo].[OrderLineSet]
ADD CONSTRAINT [FK_OrderLineOrder]
    FOREIGN KEY ([OrderId])
    REFERENCES [dbo].[OrderSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderLineOrder'
CREATE INDEX [IX_FK_OrderLineOrder]
ON [dbo].[OrderLineSet]
    ([OrderId]);
GO

-- Creating foreign key on [ShoppingListId] in table 'OrderSet'
ALTER TABLE [dbo].[OrderSet]
ADD CONSTRAINT [FK_OrderShoppingList]
    FOREIGN KEY ([ShoppingListId])
    REFERENCES [dbo].[ShoppingListSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderShoppingList'
CREATE INDEX [IX_FK_OrderShoppingList]
ON [dbo].[OrderSet]
    ([ShoppingListId]);
GO

-- Creating foreign key on [Unit_Id] in table 'UnitProductVersion'
ALTER TABLE [dbo].[UnitProductVersion]
ADD CONSTRAINT [FK_UnitProductVersion_Unit]
    FOREIGN KEY ([Unit_Id])
    REFERENCES [dbo].[UnitSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ProductVersion_Id] in table 'UnitProductVersion'
ALTER TABLE [dbo].[UnitProductVersion]
ADD CONSTRAINT [FK_UnitProductVersion_ProductVersion]
    FOREIGN KEY ([ProductVersion_Id])
    REFERENCES [dbo].[ProductVersionSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UnitProductVersion_ProductVersion'
CREATE INDEX [IX_FK_UnitProductVersion_ProductVersion]
ON [dbo].[UnitProductVersion]
    ([ProductVersion_Id]);
GO

-- Creating foreign key on [DepartmentId] in table 'DepartmentSpecificProductSet'
ALTER TABLE [dbo].[DepartmentSpecificProductSet]
ADD CONSTRAINT [FK_DepartmentSpecifikProductDepartment]
    FOREIGN KEY ([DepartmentId])
    REFERENCES [dbo].[DepartmentSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DepartmentSpecifikProductDepartment'
CREATE INDEX [IX_FK_DepartmentSpecifikProductDepartment]
ON [dbo].[DepartmentSpecificProductSet]
    ([DepartmentId]);
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

-- Creating foreign key on [ZookeeperID] in table 'DepartmentChangeSet'
ALTER TABLE [dbo].[DepartmentChangeSet]
ADD CONSTRAINT [FK_DepartmentChangeZookeeper]
    FOREIGN KEY ([ZookeeperID])
    REFERENCES [dbo].[EmployeeSet_Zookeeper]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DepartmentChangeZookeeper'
CREATE INDEX [IX_FK_DepartmentChangeZookeeper]
ON [dbo].[DepartmentChangeSet]
    ([ZookeeperID]);
GO

-- Creating foreign key on [ShopperId] in table 'ShoppingListSet'
ALTER TABLE [dbo].[ShoppingListSet]
ADD CONSTRAINT [FK_ShopperShoppingList]
    FOREIGN KEY ([ShopperId])
    REFERENCES [dbo].[EmployeeSet_Shopper]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ShopperShoppingList'
CREATE INDEX [IX_FK_ShopperShoppingList]
ON [dbo].[ShoppingListSet]
    ([ShopperId]);
GO

-- Creating foreign key on [ShopperId] in table 'PasswordChangedSet'
ALTER TABLE [dbo].[PasswordChangedSet]
ADD CONSTRAINT [FK_PasswordChangedShopper]
    FOREIGN KEY ([ShopperId])
    REFERENCES [dbo].[EmployeeSet_Shopper]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PasswordChangedShopper'
CREATE INDEX [IX_FK_PasswordChangedShopper]
ON [dbo].[PasswordChangedSet]
    ([ShopperId]);
GO

-- Creating foreign key on [Id] in table 'EmployeeSet_Zookeeper'
ALTER TABLE [dbo].[EmployeeSet_Zookeeper]
ADD CONSTRAINT [FK_Zookeeper_inherits_Employee]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[EmployeeSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'EmployeeSet_Shopper'
ALTER TABLE [dbo].[EmployeeSet_Shopper]
ADD CONSTRAINT [FK_Shopper_inherits_Employee]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[EmployeeSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------