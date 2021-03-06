
CREATE TABLE [dbo].[CoreModule_FAQCategory](
	[Id] [int] IDENTITY(1,1) primary key NOT NULL,
	[FAQCategoryName] [varchar](500) NULL,
	[DisplayOrder] [int] Not NULL default(1)
)
Go
 
CREATE TABLE [dbo].[CoreModule_FAQMaster](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Question] [varchar](max) NULL,
	[Answer] [varchar](max) NULL,
	[CategoryId] [int] NULL,
	[DisplayOrder] [int] Not NULL default(1)
	)
GO
CREATE TABLE [dbo].[CoreModule_GalleryAlbum](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Name] [varchar](100) NULL,
	[CategoryId] [int] NULL,
	[ThumbnailPath] [varchar](1000) NULL,
	[IsActive] [bit] NULL,
	[AlbumType] [int] NOT NULL,
	[DisplayOrder] [int] Not NULL default(1)
) 
GO

CREATE TABLE [dbo].[CoreModule_GalleryAlbumItem](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Name] [varchar](100) NULL,
	[AlbumId] [int] NULL,
	[ItemPath] [varchar](1000) NULL,
	[IsActive] [bit] NULL,
	[AccessType] [int] NOT NULL,
	[DisplayOrder] [int] Not NULL default(1)

) 
GO

CREATE TABLE [dbo].[CoreModule_GalleryCategory]
(
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Name] [varchar](100) NULL,
	[IsActive] [bit] NULL,
	[DisplayOrder] [int] Not NULL default(1)
) 
GO

CREATE TABLE [dbo].[CoreModule_JobCategory](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[JobCategoryName] [varchar](500) NULL,
	[DisplayOrder] [int] Not NULL default(1)
)
GO

CREATE TABLE [dbo].[CoreModule_JobMaster](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[JobTitle] [varchar](500) NULL,
	[Qualification] [varchar](500) NULL,
	[EXPERIENCE] [varchar](max) NULL,
	[SPECIALIZATION] [varchar](500) NULL,
	[CategoryId] [int] NULL,
	[DisplayOrder] [int] Not NULL default(1)
)
GO

CREATE TABLE [dbo].[CoreModule_PackageCategory](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[PackCategoryName] [varchar](500) NULL,
	[DisplayOrder] [int] Not NULL default(1)
) 
GO

CREATE TABLE [dbo].[CoreModule_PackageMaster](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[PackName] [varchar](500) NULL,
	[ShortDesc] [varchar](500) NULL,
	[LongDesc] [varchar](max) NULL,
	[Usage] [varchar](500) NULL,
	[MRP] [numeric](10, 2) NULL,
	[OfferPrice] [numeric](10, 2) NULL,
	[ImageUrl] [varchar](500) NULL,
	[CategoryId] [int] NULL,
	[DisplayOrder] [int] Not NULL default(1)
) 
GO

CREATE TABLE [dbo].[CoreModule_ProductCategory](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[ProdCategoryName] [varchar](500) NULL,
	[DisplayOrder] [int] Not NULL default(1)

) 
GO


CREATE TABLE [dbo].[CoreModule_ProductMaster](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[ProdName] [varchar](500) NULL,
	[ShortDesc] [varchar](500) NULL,
	[LongDesc] [varchar](max) NULL,
	[Usage] [varchar](500) NULL,
	[MRP] [numeric](10, 2) NULL,
	[OfferPrice] [numeric](10, 2) NULL,
	[ImageUrl] [varchar](500) NULL,
	[CategoryId] [int] NULL,
	[DisplayOrder] [int] Not NULL default(1)
) 
GO

CREATE TABLE [dbo].[CoreModule_ProjectCategory](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[ProjCategoryName] [varchar](500) NULL,
	[DisplayOrder] [int] Not NULL default(1)
) 
GO

CREATE TABLE [dbo].[CoreModule_ProjectMaster](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[ProjName] [varchar](500) NULL,
	[ShortDesc] [varchar](500) NULL,
	[LongDesc] [varchar](max) NULL,
	[ImageUrl] [varchar](500) NULL,
	[CategoryId] [int] NULL,
	[DisplayOrder] [int] Not NULL default(1)
)
GO

CREATE TABLE [dbo].[CoreModule_TestimonialCategory](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[TestimonialCategoryName] [varchar](500) NULL,
	[DisplayOrder] [int] Not NULL default(1)
) 
GO

CREATE TABLE [dbo].[CoreModule_TestimonialMaster](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[EXPERIENCE] [varchar](500) NULL,
	[Name] [varchar](500) NULL,
	[PhotoPath] [varchar](500) NULL,
	[CategoryId] [int] NULL,
	[DisplayOrder] [int] Not NULL default(1)
) 
GO

ALTER TABLE [dbo].[CoreModule_FAQCategory] ADD  DEFAULT ((1)) FOR [DisplayOrder]
GO
ALTER TABLE [dbo].[CoreModule_FAQMaster] ADD  DEFAULT ((1)) FOR [DisplayOrder]
GO
ALTER TABLE [dbo].[CoreModule_GalleryAlbum] ADD  DEFAULT ((1)) FOR [DisplayOrder]
GO
ALTER TABLE [dbo].[CoreModule_GalleryAlbumItem] ADD  DEFAULT ((1)) FOR [DisplayOrder]
GO
ALTER TABLE [dbo].[CoreModule_JobCategory] ADD  DEFAULT ((1)) FOR [DisplayOrder]
GO
ALTER TABLE [dbo].[CoreModule_JobMaster] ADD  DEFAULT ((1)) FOR [DisplayOrder]
GO
ALTER TABLE [dbo].[CoreModule_PackageCategory] ADD  DEFAULT ((1)) FOR [DisplayOrder]
GO
ALTER TABLE [dbo].[CoreModule_PackageMaster] ADD  DEFAULT ((1)) FOR [DisplayOrder]
GO
ALTER TABLE [dbo].[CoreModule_ProductCategory] ADD  DEFAULT ((1)) FOR [DisplayOrder]
GO
ALTER TABLE [dbo].[CoreModule_ProductMaster] ADD  DEFAULT ((1)) FOR [DisplayOrder]
GO
ALTER TABLE [dbo].[CoreModule_ProjectCategory] ADD  DEFAULT ((1)) FOR [DisplayOrder]
GO
ALTER TABLE [dbo].[CoreModule_ProjectMaster] ADD  DEFAULT ((1)) FOR [DisplayOrder]
GO
ALTER TABLE [dbo].[CoreModule_TestimonialCategory] ADD  DEFAULT ((1)) FOR [DisplayOrder]
GO
ALTER TABLE [dbo].[CoreModule_TestimonialMaster] ADD  DEFAULT ((1)) FOR [DisplayOrder]
GO


SET IDENTITY_INSERT [dbo].[CoreModule_FAQCategory] ON 
INSERT [dbo].[CoreModule_FAQCategory] ([Id], [FAQCategoryName], [DisplayOrder]) VALUES (1, N'General', 3)
SET IDENTITY_INSERT [dbo].[CoreModule_FAQCategory] OFF
GO

SET IDENTITY_INSERT [dbo].[CoreModule_FAQMaster] ON 
INSERT [dbo].[CoreModule_FAQMaster] ([Id], [Question], [Answer], [CategoryId], [DisplayOrder]) VALUES (1, N'I have a 3 bedroom House/Apartment. What are the services that D''LIFE Home Interiors can provide?', N'<p>We do 100% customized long lasting complete home interiors that include space planning, designing, production and installation of core furnishing and soft furnishing.&nbsp;</p>
', 1, 3)
INSERT [dbo].[CoreModule_FAQMaster] ([Id], [Question], [Answer], [CategoryId], [DisplayOrder]) VALUES (2, N'What is the procedure of getting a work done by D’LIFE?', N'<p>Kindly find step by step procedures in this page link: (Procedures)<br />
<a href="http://www.dlifeinteriors.com/custom-made/procedure/">http://www.dlifeinteriors.com/custom-made/procedure/</a>&nbsp;</p>
', 1, 2)
INSERT [dbo].[CoreModule_FAQMaster] ([Id], [Question], [Answer], [CategoryId], [DisplayOrder]) VALUES (3, N'Where all do you undertake works?', N'<p>We do undertake complete interior design and implementation works across&nbsp;<strong>Kerala and Bangalore</strong>. There are Eight branches in Kerala and one in Bangalore with all facilities of display show room, discussion, finalization, follow ups and service.&nbsp;<br />
&nbsp;</p>
', 1, 1)
SET IDENTITY_INSERT [dbo].[CoreModule_FAQMaster] OFF
GO

SET IDENTITY_INSERT [dbo].[CoreModule_GalleryAlbum] ON 
INSERT [dbo].[CoreModule_GalleryAlbum] ([Id], [Name], [CategoryId], [ThumbnailPath], [IsActive], [AlbumType], [DisplayOrder]) VALUES (1, N'Test Album', 1, N'/distfrontend/images/Blog/10.jpg', 1, 1, 1)
INSERT [dbo].[CoreModule_GalleryAlbum] ([Id], [Name], [CategoryId], [ThumbnailPath], [IsActive], [AlbumType], [DisplayOrder]) VALUES (2, N'interiror', 1, N'/distfrontend/images/Project/1.jpg', 1, 1, 2)
INSERT [dbo].[CoreModule_GalleryAlbum] ([Id], [Name], [CategoryId], [ThumbnailPath], [IsActive], [AlbumType], [DisplayOrder]) VALUES (3, N'today', 1, N'/distfrontend/images/Project/5.jpg', 1, 1, 3)
INSERT [dbo].[CoreModule_GalleryAlbum] ([Id], [Name], [CategoryId], [ThumbnailPath], [IsActive], [AlbumType], [DisplayOrder]) VALUES (4, N'My Home', 3, N'/distfrontend/images/Project/9.jpg', 1, 1, 4)
SET IDENTITY_INSERT [dbo].[CoreModule_GalleryAlbum] OFF
GO

SET IDENTITY_INSERT [dbo].[CoreModule_GalleryAlbumItem] ON 
INSERT [dbo].[CoreModule_GalleryAlbumItem] ([Id], [Name], [AlbumId], [ItemPath], [IsActive], [AccessType], [DisplayOrder]) VALUES (1, N'House', 1, N'/distfrontend/images/Project/9.jpg', 1, 1, 1)
INSERT [dbo].[CoreModule_GalleryAlbumItem] ([Id], [Name], [AlbumId], [ItemPath], [IsActive], [AccessType], [DisplayOrder]) VALUES (2, N'BedRoom', 1, N'/distfrontend/images/Project/8.jpg', 1, 1, 1)
INSERT [dbo].[CoreModule_GalleryAlbumItem] ([Id], [Name], [AlbumId], [ItemPath], [IsActive], [AccessType], [DisplayOrder]) VALUES (4, N'Kitchen', 1, N'/distfrontend/images/Ourstatis/2.jpg', 1, 1, 1)
INSERT [dbo].[CoreModule_GalleryAlbumItem] ([Id], [Name], [AlbumId], [ItemPath], [IsActive], [AccessType], [DisplayOrder]) VALUES (5, N'new', 2, N'/distfrontend/images/Overlay/5.jpg', 1, 1, 1)
INSERT [dbo].[CoreModule_GalleryAlbumItem] ([Id], [Name], [AlbumId], [ItemPath], [IsActive], [AccessType], [DisplayOrder]) VALUES (6, N'New', 2, N'/distfrontend/images/Project/pj-detail-2.jpg', 1, 1, 1)
INSERT [dbo].[CoreModule_GalleryAlbumItem] ([Id], [Name], [AlbumId], [ItemPath], [IsActive], [AccessType], [DisplayOrder]) VALUES (7, N'Today', 3, N'/distfrontend/images/Project/5.jpg', 1, 1, 1)
INSERT [dbo].[CoreModule_GalleryAlbumItem] ([Id], [Name], [AlbumId], [ItemPath], [IsActive], [AccessType], [DisplayOrder]) VALUES (8, N'aaa', 3, N'/distfrontend/images/Blog/12.jpg', 1, 1, 1)
SET IDENTITY_INSERT [dbo].[CoreModule_GalleryAlbumItem] OFF
GO

SET IDENTITY_INSERT [dbo].[CoreModule_GalleryCategory] ON 
INSERT [dbo].[CoreModule_GalleryCategory] ([Id], [Name], [IsActive], [DisplayOrder]) VALUES (1, N'Test Category', 1, 1)
INSERT [dbo].[CoreModule_GalleryCategory] ([Id], [Name], [IsActive], [DisplayOrder]) VALUES (3, N'Home', 1, 2)
SET IDENTITY_INSERT [dbo].[CoreModule_GalleryCategory] OFF
GO

SET IDENTITY_INSERT [dbo].[CoreModule_JobCategory] ON 
INSERT [dbo].[CoreModule_JobCategory] ([Id], [JobCategoryName], [DisplayOrder]) VALUES (1, N'Designs', 2)
INSERT [dbo].[CoreModule_JobCategory] ([Id], [JobCategoryName], [DisplayOrder]) VALUES (2, N'Sales', 3)
INSERT [dbo].[CoreModule_JobCategory] ([Id], [JobCategoryName], [DisplayOrder]) VALUES (3, N'Office', 1)
SET IDENTITY_INSERT [dbo].[CoreModule_JobCategory] OFF
GO

SET IDENTITY_INSERT [dbo].[CoreModule_JobMaster] ON 
INSERT [dbo].[CoreModule_JobMaster] ([Id], [JobTitle], [Qualification], [EXPERIENCE], [SPECIALIZATION], [CategoryId], [DisplayOrder]) VALUES (1, N'Senior Interior Designer	', N'Graduate or Diploma', N'Min 3 Years	', N'Home Interiors	', 3, 2)
INSERT [dbo].[CoreModule_JobMaster] ([Id], [JobTitle], [Qualification], [EXPERIENCE], [SPECIALIZATION], [CategoryId], [DisplayOrder]) VALUES (2, N'Interior Designer	', N'Graduate or Diploma', N'Min 1.5 Years	', N'Home Interiors	', 2, -2)
INSERT [dbo].[CoreModule_JobMaster] ([Id], [JobTitle], [Qualification], [EXPERIENCE], [SPECIALIZATION], [CategoryId], [DisplayOrder]) VALUES (3, N'3D Designer	', N'BE', N'Fresher', N'Home Interiors', 1, 1)
SET IDENTITY_INSERT [dbo].[CoreModule_JobMaster] OFF
GO

SET IDENTITY_INSERT [dbo].[CoreModule_PackageCategory] ON 
INSERT [dbo].[CoreModule_PackageCategory] ([Id], [PackCategoryName], [DisplayOrder]) VALUES (1, N'General', 1)
SET IDENTITY_INSERT [dbo].[CoreModule_PackageCategory] OFF
GO

SET IDENTITY_INSERT [dbo].[CoreModule_PackageMaster] ON 
INSERT [dbo].[CoreModule_PackageMaster] ([Id], [PackName], [ShortDesc], [LongDesc], [Usage], [MRP], [OfferPrice], [ImageUrl], [CategoryId], [DisplayOrder]) VALUES (2, N'Kitchen and Three Wardrobes', N'Kitchen and Three Wardrobes', N'<p>a)&nbsp;KITCHEN</p>

<ul>
	<li>Fully Loaded Overhead and Bottom Storage Spaces<br />
	( 360 X 60cm - 360 X 90 cm)</li>
	<li>Hettich (German Made - 15 year warranty) Accessories-9 No&#39;s</li>
	<li>Hood and Hob - Faber - Italy Made</li>
</ul>

<p>b)&nbsp;WARDROBE</p>

<ul>
	<li>Customizd three Door Wardrobes - 3 No&#39;s<br />
	(120 X 210 cm)</li>
</ul>
', N'on Kitchen and Wardrobe', CAST(470000.00 AS Numeric(10, 2)), CAST(300000.00 AS Numeric(10, 2)), N'/distfrontend/images/Overlay/5.jpg', 1, 3)
INSERT [dbo].[CoreModule_PackageMaster] ([Id], [PackName], [ShortDesc], [LongDesc], [Usage], [MRP], [OfferPrice], [ImageUrl], [CategoryId], [DisplayOrder]) VALUES (3, N'THREE BEDROOMS AND KITCHEN', N'THREE BEDROOMS AND KITCHEN', N'<p>a)&nbsp;KITCHEN</p>

<ul>
	<li>Fully Loaded Overhead and Bottom Storage Spaces<br />
	( 360 X 60cm - 360 X 90 cm)</li>
	<li>Hettich (German Made - 15 year warranty) Accessories-9 No&#39;s*&nbsp;<br />
	( *For details refer&nbsp;<a href="http://www.dlifeinteriors.com/offers/#">Package A</a>)</li>
	<li>Hood and Hob - Faber - Italy Made</li>
</ul>

<p>b)&nbsp;THREE BEDROOMS</p>

<ul>
	<li>Queen Size Cot with Bottom Storage - 3 No&#39;s</li>
	<li>Three Door Wardrobe - 3 No&#39;s&nbsp;<br />
	(120 X 210 cm)</li>
	<li>Cot Side Table - 3 No&#39;s</li>
</ul>

<p>c)&nbsp;DINING ROOM</p>

<ul>
	<li>6 Seater Dining Table With Thick Glass Top</li>
	<li>Artificial Leather Covered Siam Dining Chair - 6 No&#39;s</li>
</ul>

<p>d)&nbsp;LIVING ROOM</p>

<ul>
	<li>Premium LCD Display Unit</li>
	<li>Wall Mounted Shoe Rack</li>
</ul>
', N'On Bedrooms', CAST(10000.00 AS Numeric(10, 2)), CAST(5000.00 AS Numeric(10, 2)), N'/distfrontend/images/Overlay/5.jpg', 1, 1)
INSERT [dbo].[CoreModule_PackageMaster] ([Id], [PackName], [ShortDesc], [LongDesc], [Usage], [MRP], [OfferPrice], [ImageUrl], [CategoryId], [DisplayOrder]) VALUES (4, N'Everything Essential', N'Everything Essential', N'<p>a)&nbsp;KITCHEN</p>

<ul>
	<li>Fully Loaded Overhead and Bottom Storage Spaces<br />
	( 360 X 60cm - 360 X 90 cm)</li>
	<li>Hettich (German Made - 15 year warranty) Accessories-9 No&#39;s&nbsp;<br />
	For details refer&nbsp;<a href="http://www.dlifeinteriors.com/offers/#">Package A</a></li>
	<li>Hood and Hob - Faber - Italy Made</li>
</ul>

<p>b)&nbsp;THREE BEDROOMS</p>

<ul>
	<li>Queen Size cot with Bottom storege - 3 No&#39;s</li>
	<li>Three door wardrobe - 3 No&#39;s</li>
	<li>Cot Side Table - 3 No&#39;s</li>
</ul>
', N'On Everything', CAST(50000.00 AS Numeric(10, 2)), CAST(10000.00 AS Numeric(10, 2)), N'/distfrontend/images/Overlay/5.jpg', 1, 2)
SET IDENTITY_INSERT [dbo].[CoreModule_PackageMaster] OFF
GO

SET IDENTITY_INSERT [dbo].[CoreModule_ProductCategory] ON 
INSERT [dbo].[CoreModule_ProductCategory] ([Id], [ProdCategoryName], [DisplayOrder]) VALUES (3, N'Kitchen', 2)
INSERT [dbo].[CoreModule_ProductCategory] ([Id], [ProdCategoryName], [DisplayOrder]) VALUES (4, N'Bedroom', 1)
INSERT [dbo].[CoreModule_ProductCategory] ([Id], [ProdCategoryName], [DisplayOrder]) VALUES (8, N'Kids Room', 3)
SET IDENTITY_INSERT [dbo].[CoreModule_ProductCategory] OFF
GO

SET IDENTITY_INSERT [dbo].[CoreModule_ProductMaster] ON 
INSERT [dbo].[CoreModule_ProductMaster] ([Id], [ProdName], [ShortDesc], [LongDesc], [Usage], [MRP], [OfferPrice], [ImageUrl], [CategoryId], [DisplayOrder]) VALUES (12, N'L-Shape Kitchen', N'L-Shape Kitchen', N'', N'Kitchen', CAST(15000.00 AS Numeric(10, 2)), CAST(10000.00 AS Numeric(10, 2)), N'/distfrontend/images/Overlay/5.jpg', 3, 1)
INSERT [dbo].[CoreModule_ProductMaster] ([Id], [ProdName], [ShortDesc], [LongDesc], [Usage], [MRP], [OfferPrice], [ImageUrl], [CategoryId], [DisplayOrder]) VALUES (13, N'U- Shape Kitchen', N'U- Shape Kitchen', N'', N'Kitchen', CAST(45000.00 AS Numeric(10, 2)), CAST(25000.00 AS Numeric(10, 2)), N'/distfrontend/images/Overlay/5.jpg', 3, 2)
INSERT [dbo].[CoreModule_ProductMaster] ([Id], [ProdName], [ShortDesc], [LongDesc], [Usage], [MRP], [OfferPrice], [ImageUrl], [CategoryId], [DisplayOrder]) VALUES (14, N'COT', N'COT', N'', N'Bedroom', CAST(75000.00 AS Numeric(10, 2)), CAST(45000.00 AS Numeric(10, 2)), N'/distfrontend/images/Overlay/5.jpg', 4, 3)
INSERT [dbo].[CoreModule_ProductMaster] ([Id], [ProdName], [ShortDesc], [LongDesc], [Usage], [MRP], [OfferPrice], [ImageUrl], [CategoryId], [DisplayOrder]) VALUES (15, N'Wardrobe cum Study Table', N'Wardrobe cum Study Table', N'', N'Kids Room', CAST(65000.00 AS Numeric(10, 2)), CAST(42000.00 AS Numeric(10, 2)), N'/distfrontend/images/Overlay/5.jpg', 8, 4)
SET IDENTITY_INSERT [dbo].[CoreModule_ProductMaster] OFF
GO

SET IDENTITY_INSERT [dbo].[CoreModule_ProjectCategory] ON 
INSERT [dbo].[CoreModule_ProjectCategory] ([Id], [ProjCategoryName], [DisplayOrder]) VALUES (1, N'CORPORATE CLIENTS', 2)
INSERT [dbo].[CoreModule_ProjectCategory] ([Id], [ProjCategoryName], [DisplayOrder]) VALUES (3, N'HOME INTERIOR CLIENTS', 1)
SET IDENTITY_INSERT [dbo].[CoreModule_ProjectCategory] OFF
GO

SET IDENTITY_INSERT [dbo].[CoreModule_ProjectMaster] ON 
INSERT [dbo].[CoreModule_ProjectMaster] ([Id], [ProjName], [ShortDesc], [LongDesc], [ImageUrl], [CategoryId], [DisplayOrder]) VALUES (1, N'Reshma', N'Calicut', N'', N'/distfrontend/images/Project/9.jpg', 1, 3)
INSERT [dbo].[CoreModule_ProjectMaster] ([Id], [ProjName], [ShortDesc], [LongDesc], [ImageUrl], [CategoryId], [DisplayOrder]) VALUES (2, N'Appu Babu    ', N'Vyttila', N'', N'/distfrontend/images/Project/pj-detail-4.jpg', 1, 2)
INSERT [dbo].[CoreModule_ProjectMaster] ([Id], [ProjName], [ShortDesc], [LongDesc], [ImageUrl], [CategoryId], [DisplayOrder]) VALUES (3, N'Madhu A V   ', N'Ambalappuzha', N'', N'/distfrontend/images/Project/6.jpg', 3, 1)
SET IDENTITY_INSERT [dbo].[CoreModule_ProjectMaster] OFF
GO

SET IDENTITY_INSERT [dbo].[CoreModule_TestimonialCategory] ON 
INSERT [dbo].[CoreModule_TestimonialCategory] ([Id], [TestimonialCategoryName], [DisplayOrder]) VALUES (1, N'General', 1)
INSERT [dbo].[CoreModule_TestimonialCategory] ([Id], [TestimonialCategoryName], [DisplayOrder]) VALUES (2, N'Special', 2)
SET IDENTITY_INSERT [dbo].[CoreModule_TestimonialCategory] OFF
GO

SET IDENTITY_INSERT [dbo].[CoreModule_TestimonialMaster] ON 
INSERT [dbo].[CoreModule_TestimonialMaster] ([Id], [EXPERIENCE], [Name], [PhotoPath], [CategoryId], [DisplayOrder]) VALUES (1, N'<p>City Scape Interiior is doing awesome work</p>

<p>I was always apprehensive about choosing a service provider for the interior works as to whether they would deliver the quality and service promised. Upon contracting D&#39;Life, they gave me the confidence to go ahead with the project and they delivered the committed services to the best of my satisfaction.</p>
', N'Sunil Sharma', N'/distfrontend/images/Overlay/5.jpg', 1, 1)
INSERT [dbo].[CoreModule_TestimonialMaster] ([Id], [EXPERIENCE], [Name], [PhotoPath], [CategoryId], [DisplayOrder]) VALUES (2, N'<p>City Scape Interior is Good Inerior Designer in Vadodara.</p>

<p>Better 3D view of the design, And working with D&#39;Life was very pleasant and approachable. Great team work. keep it up</p>
', N'Viral Dhruva', N'/distfrontend/images/Overlay/5.jpg', 1, 4)
INSERT [dbo].[CoreModule_TestimonialMaster] ([Id], [EXPERIENCE], [Name], [PhotoPath], [CategoryId], [DisplayOrder]) VALUES (3, N'<p>Test City Scap Interior</p>
', N'Ashish Patel', N'/distfrontend/images/Overlay/5.jpg', 1, 2)
SET IDENTITY_INSERT [dbo].[CoreModule_TestimonialMaster] OFF
GO

insert into [cms_PackageInstallations]([PackageId],[PackagePath],[CreatedDate],[CreatedBy],[Status],[ProcPages]
,[ProcModules]
,[ProcTemplateFields]
,[IsValidPackage]
,[ValidationErrors]) values ('6BB8C8F1-39BA-4FF0-8180-2FFB17E2C585','/Areas/Admin/Data/InstallPackages/6BB8C8F1-39BA-4FF0-8180-2FFB17E2C585/DefaultSitePages.zip',getdate(),'admin','Pending',0,0,0,1,'')
GO

update app_configs set itemDesc = '/Login' where itemname= 'LoginPageUrl'
Go

Create Table dbo.CoreModule_Sliders
(
	Id int primary key identity(1,1),
	SliderCode varchar(20),
	SliderName varchar(100),
	Active bit not null default(1),
	DisplayOrder int not null Default(1)
)
GO

Create Table dbo.CoreModule_SliderItems
(
	Id int primary key identity(1,1),
	SliderId Int,
	Active bit not null default(1),
	FirstLine varchar(200),
	SecondLine varchar(200),
	ThirdLine varchar(200),
	ForthLine varchar(200),
	FirstButtonText varchar(200),
	SecondButtonText varchar(200),
	FirstButtonLink varchar(1000),
	SecondButtonLink varchar(1000),
	SliderImagePath varchar(1000),
	DisplayOrder int not null Default(1)
)
GO

Create Table dbo.CoreModule_NewsCategory
(
	Id int primary key identity(1,1),
	CategoryName varchar(100),
	Active bit not null default(1),
	DisplayOrder int not null Default(1)
)
GO

Create Table dbo.CoreModule_News
(
	Id int primary key identity(1,1),
	NewsCategoryId int,
	NewsTitle varchar(100),
	NewsShortDesc varchar(1000),
	LongDesc text,
	Active bit not null default(1),
	NewsDate datetime,
	AttachmentPath varchar(1000),
	DisplayOrder int not null Default(1)
)
GO


Create Table dbo.CoreModule_EventCategory
(
	Id int primary key identity(1,1),
	EventName varchar(100),
	Active bit not null default(1),
	DisplayOrder int not null Default(1)
)
GO

CREATE TABLE [dbo].[CoreModule_Events](
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	EventCategoryId int,
	[Name] [varchar](200) NULL,
	[ShortDesc] [varchar](500) NULL,
	[LongDesc] [varchar](max) NULL,
	[EventDateTime] [datetime] NULL,
	[EventRegistrationStartsOn] [datetime] NULL,
	[Active] [bit] NOT NULL DEFAULT(1),
	[PhotoUrl] [varchar](500) NULL,
	[Location] [varchar](200) NULL,
	[RegistrationEndOn] [datetime] NULL,
	DisplayOrder int not null Default(1)
)
GO

CREATE TABLE [dbo].[CoreModule_SMSQueue](
	[Id] [int] IDENTITY(1,1) NOT NULL Primary Key,
	[SMSNumber] [varchar](15) NULL,
	[SMSText] [varchar](500) NULL,
	[IsSent] [bit] NOT NULL DEFAULT(0),
	[CreatedDate] [datetime] NULL,
	[SentDate] [datetime] NULL,
	[SenderName] [varchar](10) NULL
	
)
GO

Create Table dbo.CoreModule_BlogCategory
(
	Id int primary key identity(1,1),
	CategoryName varchar(100),
	Active bit not null default(1),
	DisplayOrder int not null Default(1)
)
GO

Create Table dbo.CoreModule_Blogs
(
	Id int primary key identity(1,1),
	BlogCategoryId int,
	BlogTitle varchar(100),
	BlogShortDesc varchar(1000),
	LongDesc text,
	Active bit not null default(1),
	PublishDate datetime,
	Author varchar(200),
	Tags varchar(500),
	DisplayOrder int not null Default(1)
)
GO
alter table dbo.cms_Pages
add ShowInNavigation bit not null default(1)

Go

insert into [cms_PackageInstallations]([PackageId],[PackagePath],[CreatedDate],[CreatedBy],[Status],[ProcPages]
,[ProcModules]
,[ProcTemplateFields]
,[IsValidPackage]
,[ValidationErrors]) values ('9D273D3C-B6C7-4338-92CF-35746B1BC2E6','/Areas/Admin/Data/InstallPackages/9D273D3C-B6C7-4338-92CF-35746B1BC2E6/package_2018-02-14_105139.zip',getdate(),'admin','Pending',0,0,0,1,'')
GO