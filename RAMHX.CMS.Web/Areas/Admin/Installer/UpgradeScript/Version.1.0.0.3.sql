IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_NAME = 'cms_PackageInstallations'))
BEGIN
   CREATE TABLE [dbo].[cms_PackageInstallations](
		[PackageId] uniqueidentifier NOT NULL PRIMARY KEY,
		[PackagePath] [varchar](500) NULL,
		[CreatedDate] [datetime] NULL,
		[CreatedBy] [varchar](50) NOT NULL,
		[Status] [varchar](50) NULL,
		[StartDate] [datetime] NULL,
		[ModifiedDate] [datetime] NULL,
		[ComplatedDate] [datetime] NULL,
		[TotalPages] [int] NULL,
		[TotalModules] [int] NULL,
		[TotalTemplateFields] [int] NULL,
		[ProcPages] [int] NULL,
		[ProcModules] [int] NULL,
		[ProcTemplateFields] [int] NULL,
		[IsValidPackage] [bit] NOT NULL,
		[ValidationErrors] [text] NULL
	 )
	
END
GO
DECLARE @itemid int
IF not exists(select * FROM [app_configs] where [ItemCode] = 'RAMHX.Package.InstallPath')
BEGIN
	SELECT @itemid = MAX(itemid) + 1 from [app_configs] WHERE [groupid] = 1
	INSERT [app_configs] ([groupid], [itemid], [ItemName], [ItemDesc], [IsActive], [ShortDesc], [ItemCode]) VALUES (1, @itemid, N'RAMHX.Package.InstallPath', N'/Areas/Admin/Data/InstallPackages/', 1, N'', N'RAMHX.Package.InstallPath')
END

if not exists(select * FROM [app_configs] where [ItemCode] = 'RAMHX.Package.APIKey')
BEGIN
	SELECT @itemid = MAX(itemid) + 1 from [app_configs] WHERE [groupid] = 1
	INSERT [app_configs] ([groupid], [itemid], [ItemName], [ItemDesc], [IsActive], [ShortDesc], [ItemCode]) VALUES (1, @itemid, N'RAMHX.Package.APIKey', N'testingkey', 1, N'', N'RAMHX.Package.APIKey')
END
IF (EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_NAME = 'cms_PackageInstallations'))
BEGIN
	ALTER TABLE [dbo].[cms_PackageInstallations] DROP COLUMN [ValidationErrors]
END
GO
IF (EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_NAME = 'cms_PackageInstallations'))
BEGIN
	ALTER TABLE [dbo].[cms_PackageInstallations] ADD [ValidationErrors] [text] NULL
END
GO