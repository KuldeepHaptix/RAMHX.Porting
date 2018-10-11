IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[cms_PageFieldValues]') AND type in (N'U'))
BEGIN
	drop table cms_PageFieldValues
END
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[cms_TemplateFields]') AND type in (N'U'))
BEGIN
	drop table cms_TemplateFields
END
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[cms_PageTemplate]') AND type in (N'U'))
BEGIN
	drop table cms_PageTemplate
END
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[cms_PageRoles]') AND type in (N'U'))
BEGIN
	drop table cms_PageRoles
END
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[cms_PageHTMLModules]') AND type in (N'U'))
BEGIN
	drop table cms_PageHTMLModules
END
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[cms_Templates]') AND type in (N'U'))
BEGIN
	drop table cms_Templates
END
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[cms_Pages]') AND type in (N'U'))
BEGIN
	drop table cms_Pages
END
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HtmlModule]') AND type in (N'U'))
BEGIN
	drop table HtmlModule
END

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[cms_Pages]') AND type in (N'U'))
BEGIN
CREATE TABLE [cms_Pages](
	[PageID] uniqueidentifier primary  key,
	[ParentPageID] uniqueidentifier FOREIGN KEY REFERENCES cms_Pages([PageId]) ,
	[PageOrder] [int] NOT NULL,
	[PageName] [nvarchar](500) NOT NULL,
	[PageCode] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](512) NULL,
	[PageUrl] [nvarchar](500) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedByUserId] [uniqueidentifier] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedByUserId] [uniqueidentifier] NULL,
	[PageTitle] [nvarchar](1000) NULL,
	[PageMetaKeywords] [nvarchar](max) NULL,
	[PageMetaDescription] [nvarchar](max) NULL,
	[PageLayoutPath] [varchar](500) NULL
	)
END

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HtmlModule]') AND type in (N'U'))
BEGIN
CREATE TABLE [HtmlModule](
	[HTMLModuleId] uniqueidentifier primary  key,
	[HtmlModuleCode] [varchar](50) NOT NULL unique,
	[HtmlModuleName] [varchar](100) NOT NULL,
	[HtmlModuleDescription] [varchar](300) NOT NULL,
	[HtmlModuleHTML] [ntext] NOT NULL,
	[PageName] [varchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[CreatedUserName] [nvarchar](256) NULL,
	[ModifiedUserName] [nvarchar](256) NULL
	)
END

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[cms_Templates]') AND type in (N'U'))
BEGIN
CREATE TABLE [cms_Templates](
	[TemplateId] uniqueidentifier primary key,
	[TemplateName] [nvarchar](500) NOT NULL,
	[TemplateCode] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](512) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedByUserId] [uniqueidentifier] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedByUserId] [uniqueidentifier] NULL
	)
END

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[cms_PageHTMLModules]') AND type in (N'U'))
BEGIN
CREATE TABLE [cms_PageHTMLModules](
	[PageHTMLModuleId] uniqueidentifier primary key,
	[PageID] uniqueidentifier ,
	[HTMLModuleId]  uniqueidentifier,
	[OrderIndex] [int] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedByUserId] [uniqueidentifier] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedByUserId] [uniqueidentifier] NULL
	)
END

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[cms_PageRoles]') AND type in (N'U'))
BEGIN
CREATE TABLE [cms_PageRoles](
	[PageId] uniqueidentifier  ,
	[RoleId] nvarchar(128) ,
	CONSTRAINT [PK_cms_PageRoles] PRIMARY KEY CLUSTERED 
(
	[PageId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[cms_PageTemplate]') AND type in (N'U'))
BEGIN
CREATE TABLE [cms_PageTemplate](
	[PageId] uniqueidentifier not null   FOREIGN KEY REFERENCES cms_Pages([PageId]) ,
	[TemplateId] uniqueidentifier not null   FOREIGN KEY REFERENCES cms_Templates([TemplateId]) ,
PRIMARY KEY CLUSTERED 
(
	[PageId] ASC,
	[TemplateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[cms_TemplateFields]') AND type in (N'U'))
BEGIN
CREATE TABLE [cms_TemplateFields](
	[TemplateFieldId] uniqueidentifier primary key,
	[FieldName] [nvarchar](500) NULL,
	[FieldTypeId] [int] NULL,
	[FieldDisplayOrder] [int] NULL,
	[DefaultValue] [nvarchar](max) NULL,
	[TemplateId] uniqueidentifier  FOREIGN KEY REFERENCES cms_Templates([TemplateId]) ,
	[CreatedDate] [datetime] NULL,
	[CreatedByUserId] [uniqueidentifier] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedByUserId] [uniqueidentifier] NULL
	)
END

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[cms_PageFieldValues]') AND type in (N'U'))
BEGIN
CREATE TABLE [cms_PageFieldValues](
	[PageId] uniqueidentifier  FOREIGN KEY REFERENCES cms_Pages([PageID]) ,
	[TemplateId]  uniqueidentifier  FOREIGN KEY REFERENCES cms_Templates([TemplateId]) ,
	[TemplateFieldId]  uniqueidentifier  FOREIGN KEY REFERENCES cms_TemplateFields([TemplateFieldId]) ,
	[FieldValue] [ntext] NULL,
PRIMARY KEY CLUSTERED 
(
	[PageId] ASC,
	[TemplateId] ASC,
	[TemplateFieldId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END

INSERT [cms_Pages] ([PageId], [ParentPageID], [PageOrder], [PageName], [PageCode], [Description], [PageUrl], [CreatedDate], [CreatedByUserId], [ModifiedDate], [ModifiedByUserId], [PageTitle], [PageMetaKeywords], [PageMetaDescription], [PageLayoutPath]) VALUES ('628CE6A0-BEF9-4505-89B9-A49F76646318',NULL, 1, N'Main', N'MAIN_PAGE', N'Main page', N'Main', NULL, NULL, CAST(0x0000A5CE00E12F1D AS DateTime), NULL, N'Main Page', N'Main', N'Default Main Page', null)
go

alter  table cms_TemplateFields add DisplayName nvarchar(100),Notes nvarchar(200)
go

