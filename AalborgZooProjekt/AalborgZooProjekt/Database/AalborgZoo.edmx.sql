
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/17/2017 14:30:58
-- Generated from EDMX file: C:\Users\Tobias\Source\Repos\ZooDagsrapport2\AalborgZooProjekt\AalborgZooProjekt\Database\AalborgZoo.edmx
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

IF OBJECT_ID(N'[dbo].[FK_DepartmentOrderlist]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderlistSet] DROP CONSTRAINT [FK_DepartmentOrderlist];
GO
IF OBJECT_ID(N'[dbo].[FK_DepartmentProduct_Department]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DepartmentProduct] DROP CONSTRAINT [FK_DepartmentProduct_Department];
GO
IF OBJECT_ID(N'[dbo].[FK_DepartmentProduct_Product]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DepartmentProduct] DROP CONSTRAINT [FK_DepartmentProduct_Product];
GO
IF OBJECT_ID(N'[dbo].[FK_DepartmentZookeeper]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeSet_Zookeeper] DROP CONSTRAINT [FK_DepartmentZookeeper];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderlistShoppinglist]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderlistSet] DROP CONSTRAINT [FK_OrderlistShoppinglist];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderlistZookeeper]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderlistSet] DROP CONSTRAINT [FK_OrderlistZookeeper];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderOrderlist]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderSet] DROP CONSTRAINT [FK_OrderOrderlist];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderSet] DROP CONSTRAINT [FK_ProductOrder];
GO
IF OBJECT_ID(N'[dbo].[FK_Shopper_inherits_Employee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeSet_Shopper] DROP CONSTRAINT [FK_Shopper_inherits_Employee];
GO
IF OBJECT_ID(N'[dbo].[FK_ShoppinglistShopper]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ShoppinglistSet] DROP CONSTRAINT [FK_ShoppinglistShopper];
GO
IF OBJECT_ID(N'[dbo].[FK_Zookeeper_inherits_Employee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeSet_Zookeeper] DROP CONSTRAINT [FK_Zookeeper_inherits_Employee];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[DepartmentProduct]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DepartmentProduct];
GO
IF OBJECT_ID(N'[dbo].[DepartmentSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DepartmentSet];
GO
IF OBJECT_ID(N'[dbo].[EmployeeSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeeSet];
GO
IF OBJECT_ID(N'[dbo].[EmployeeSet_Shopper]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeeSet_Shopper];
GO
IF OBJECT_ID(N'[dbo].[EmployeeSet_Zookeeper]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeeSet_Zookeeper];
GO
IF OBJECT_ID(N'[dbo].[OrderlistSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderlistSet];
GO
IF OBJECT_ID(N'[dbo].[OrderSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderSet];
GO
IF OBJECT_ID(N'[dbo].[ProductSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductSet];
GO
IF OBJECT_ID(N'[dbo].[ShoppinglistSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ShoppinglistSet];
GO
IF OBJECT_ID(N'[AalborgZooStoreContainer].[database_firewall_rules]', 'U') IS NOT NULL
    DROP TABLE [AalborgZooStoreContainer].[database_firewall_rules];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'EmployeeSet'
CREATE TABLE [dbo].[EmployeeSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ShoppinglistSet'
CREATE TABLE [dbo].[ShoppinglistSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Shopper_Id] int  NOT NULL
);
GO

-- Creating table 'OrderSet'
CREATE TABLE [dbo].[OrderSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ShoppinglistId] int  NOT NULL,
    [ZookeeperId] int  NOT NULL,
    [DepartmentId] int  NOT NULL
);
GO

-- Creating table 'OrderLineSet'
CREATE TABLE [dbo].[OrderLineSet] (
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

-- Creating table 'database_firewall_rules'
CREATE TABLE [dbo].[database_firewall_rules] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(128)  NOT NULL,
    [start_ip_address] varchar(45)  NOT NULL,
    [end_ip_address] varchar(45)  NOT NULL,
    [create_date] datetime  NOT NULL,
    [modify_date] datetime  NOT NULL
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

-- Creating primary key on [id], [name], [start_ip_address], [end_ip_address], [create_date], [modify_date] in table 'database_firewall_rules'
ALTER TABLE [dbo].[database_firewall_rules]
ADD CONSTRAINT [PK_database_firewall_rules]
    PRIMARY KEY CLUSTERED ([id], [name], [start_ip_address], [end_ip_address], [create_date], [modify_date] ASC);
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

-- Creating foreign key on [ShoppinglistId] in table 'OrderSet'
ALTER TABLE [dbo].[OrderSet]
ADD CONSTRAINT [FK_OrderlistShoppinglist]
    FOREIGN KEY ([ShoppinglistId])
    REFERENCES [dbo].[ShoppinglistSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderlistShoppinglist'
CREATE INDEX [IX_FK_OrderlistShoppinglist]
ON [dbo].[OrderSet]
    ([ShoppinglistId]);
GO

-- Creating foreign key on [OrderlistId] in table 'OrderLineSet'
ALTER TABLE [dbo].[OrderLineSet]
ADD CONSTRAINT [FK_OrderOrderlist]
    FOREIGN KEY ([OrderlistId])
    REFERENCES [dbo].[OrderSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderOrderlist'
CREATE INDEX [IX_FK_OrderOrderlist]
ON [dbo].[OrderLineSet]
    ([OrderlistId]);
GO

-- Creating foreign key on [ZookeeperId] in table 'OrderSet'
ALTER TABLE [dbo].[OrderSet]
ADD CONSTRAINT [FK_OrderlistZookeeper]
    FOREIGN KEY ([ZookeeperId])
    REFERENCES [dbo].[EmployeeSet_Zookeeper]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderlistZookeeper'
CREATE INDEX [IX_FK_OrderlistZookeeper]
ON [dbo].[OrderSet]
    ([ZookeeperId]);
GO

-- Creating foreign key on [ProductId] in table 'OrderLineSet'
ALTER TABLE [dbo].[OrderLineSet]
ADD CONSTRAINT [FK_ProductOrder]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[ProductSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductOrder'
CREATE INDEX [IX_FK_ProductOrder]
ON [dbo].[OrderLineSet]
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

-- Creating foreign key on [DepartmentId] in table 'OrderSet'
ALTER TABLE [dbo].[OrderSet]
ADD CONSTRAINT [FK_DepartmentOrderlist]
    FOREIGN KEY ([DepartmentId])
    REFERENCES [dbo].[DepartmentSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DepartmentOrderlist'
CREATE INDEX [IX_FK_DepartmentOrderlist]
ON [dbo].[OrderSet]
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