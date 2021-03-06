IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[app_Configs]') AND type in (N'U'))
BEGIN
	
	CREATE TABLE [__MigrationHistory](
		[MigrationId] [nvarchar](150) NOT NULL,
		[ContextKey] [nvarchar](300) NOT NULL,
		[Model] [varbinary](max) NOT NULL,
		[ProductVersion] [nvarchar](32) NOT NULL,
	 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
	(
		[MigrationId] ASC,
		[ContextKey] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

	/****** Object:  Table [app_configs]    Script Date: 2016/04/01 12:41:32 ******/
	IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[app_Configs]') AND type in (N'U'))
	BEGIN
	CREATE TABLE [app_Configs](
		[GroupId] [int] NOT NULL,
		[ItemId] [int] NOT NULL,
		[ItemName] [varchar](200) NULL,
		[ItemDesc] [varchar](500) NULL,
		[IsActive] [bit] NOT NULL DEFAULT ((1)),
		[ShortDesc] [varchar](300) NULL,
		[ItemCode] [varchar](100) NULL,
	PRIMARY KEY CLUSTERED 
	(
		[groupid] ASC,
		[itemid] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
	END

	/****** Object:  Table [AspNetRoles]    Script Date: 2016/04/01 12:41:32 ******/

	IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[AspNetRoles]') AND type in (N'U'))
	BEGIN
	CREATE TABLE [AspNetRoles](
		[Id] [nvarchar](128) NOT NULL,
		[Name] [nvarchar](256) NOT NULL,
	 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
	END
	/****** Object:  Table [AspNetUserClaims]    Script Date: 2016/04/01 12:41:32 ******/

	IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[AspNetUserClaims]') AND type in (N'U'))
	BEGIN
	CREATE TABLE [AspNetUserClaims](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[UserId] [nvarchar](128) NOT NULL,
		[ClaimType] [nvarchar](max) NULL,
		[ClaimValue] [nvarchar](max) NULL,
	 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
	END
	/****** Object:  Table [AspNetUserLogins]    Script Date: 2016/04/01 12:41:32 ******/

	IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[AspNetUserLogins]') AND type in (N'U'))
	BEGIN
	CREATE TABLE [AspNetUserLogins](
		[LoginProvider] [nvarchar](128) NOT NULL,
		[ProviderKey] [nvarchar](128) NOT NULL,
		[UserId] [nvarchar](128) NOT NULL,
	 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
	(
		[LoginProvider] ASC,
		[ProviderKey] ASC,
		[UserId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
	END
	/****** Object:  Table [AspNetUserRoles]    Script Date: 2016/04/01 12:41:32 ******/

	IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[AspNetUserRoles]') AND type in (N'U'))
	BEGIN
	CREATE TABLE [AspNetUserRoles](
		[UserId] [nvarchar](128) NOT NULL,
		[RoleId] [nvarchar](128) NOT NULL,
	 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
	(
		[UserId] ASC,
		[RoleId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
	END
	/****** Object:  Table [AspNetUsers]    Script Date: 2016/04/01 12:41:32 ******/

	IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[AspNetUsers]') AND type in (N'U'))
	BEGIN
	CREATE TABLE [AspNetUsers](
		[Id] [nvarchar](128) NOT NULL,
		[Email] [nvarchar](256) NULL,
		[EmailConfirmed] [bit] NOT NULL,
		[PasswordHash] [nvarchar](max) NULL,
		[SecurityStamp] [nvarchar](max) NULL,
		[PhoneNumber] [nvarchar](max) NULL,
		[PhoneNumberConfirmed] [bit] NOT NULL,
		[TwoFactorEnabled] [bit] NOT NULL,
		[LockoutEndDateUtc] [datetime] NULL,
		[LockoutEnabled] [bit] NOT NULL,
		[AccessFailedCount] [int] NOT NULL,
		[UserName] [nvarchar](256) NOT NULL,
	 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
	END
	/****** Object:  Table [cms_PageFieldValues]    Script Date: 2016/04/01 12:41:32 ******/

	IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[cms_PageFieldValues]') AND type in (N'U'))
	BEGIN
	CREATE TABLE [cms_PageFieldValues](
		[PageId] [int] NOT NULL,
		[TemplateId] [int] NOT NULL,
		[TemplateFieldId] [int] NOT NULL,
		[FieldValue] [ntext] NULL,
	PRIMARY KEY CLUSTERED 
	(
		[PageId] ASC,
		[TemplateId] ASC,
		[TemplateFieldId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
	END
	/****** Object:  Table [cms_PageHTMLModules]    Script Date: 2016/04/01 12:41:32 ******/

	IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[cms_PageHTMLModules]') AND type in (N'U'))
	BEGIN
	CREATE TABLE [cms_PageHTMLModules](
		[PageHTMLModuleId] [int] IDENTITY(1,1) NOT NULL,
		[PageID] [int] NOT NULL,
		[HTMLModuleId] [int] NOT NULL,
		[OrderIndex] [int] NOT NULL,
		[CreatedDate] [datetime] NULL,
		[CreatedByUserId] [uniqueidentifier] NULL,
		[ModifiedDate] [datetime] NULL,
		[ModifiedByUserId] [uniqueidentifier] NULL,
	 CONSTRAINT [PK_cms_PageHTMLModules] PRIMARY KEY CLUSTERED 
	(
		[PageHTMLModuleId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
	END
	/****** Object:  Table [cms_PageRoles]    Script Date: 2016/04/01 12:41:32 ******/

	IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[cms_PageRoles]') AND type in (N'U'))
	BEGIN
	CREATE TABLE [cms_PageRoles](
		[PageId] [int] NOT NULL,
		[RoleId] [nvarchar](128) NOT NULL,
	 CONSTRAINT [PK_cms_PageRoles] PRIMARY KEY CLUSTERED 
	(
		[PageId] ASC,
		[RoleId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
	END
	/****** Object:  Table [cms_Pages]    Script Date: 2016/04/01 12:41:32 ******/

	IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[cms_Pages]') AND type in (N'U'))
	BEGIN
	CREATE TABLE [cms_Pages](
		[PageID] [int] IDENTITY(1,1) NOT NULL,
		[ParentPageID] [int] NULL,
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
		[PageLayoutPath] [varchar](500) NULL,
	PRIMARY KEY CLUSTERED 
	(
		[PageID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
	END

	/****** Object:  Table [cms_PageTemplate]    Script Date: 2016/04/01 12:41:32 ******/

	IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[cms_PageTemplate]') AND type in (N'U'))
	BEGIN
	CREATE TABLE [cms_PageTemplate](
		[PageId] [int] NOT NULL,
		[TemplateId] [int] NOT NULL,
	PRIMARY KEY CLUSTERED 
	(
		[PageId] ASC,
		[TemplateId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
	END
	/****** Object:  Table [cms_TemplateFields]    Script Date: 2016/04/01 12:41:32 ******/

	IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[cms_TemplateFields]') AND type in (N'U'))
	BEGIN
	CREATE TABLE [cms_TemplateFields](
		[TemplateFieldId] [int] IDENTITY(1,1) NOT NULL,
		[FieldName] [nvarchar](500) NULL,
		[FieldTypeId] [int] NULL,
		[FieldDisplayOrder] [int] NULL,
		[DefaultValue] [nvarchar](max) NULL,
		[TemplateId] [int] NULL,
		[CreatedDate] [datetime] NULL,
		[CreatedByUserId] [uniqueidentifier] NULL,
		[ModifiedDate] [datetime] NULL,
		[ModifiedByUserId] [uniqueidentifier] NULL,
	PRIMARY KEY CLUSTERED 
	(
		[TemplateFieldId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
	END
	/****** Object:  Table [cms_Templates]    Script Date: 2016/04/01 12:41:32 ******/

	IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[cms_Templates]') AND type in (N'U'))
	BEGIN
	CREATE TABLE [cms_Templates](
		[TemplateId] [int] IDENTITY(1,1) NOT NULL,
		[TemplateName] [nvarchar](500) NOT NULL,
		[TemplateCode] [nvarchar](100) NOT NULL,
		[Description] [nvarchar](512) NULL,
		[CreatedDate] [datetime] NULL,
		[CreatedByUserId] [uniqueidentifier] NULL,
		[ModifiedDate] [datetime] NULL,
		[ModifiedByUserId] [uniqueidentifier] NULL,
	PRIMARY KEY CLUSTERED 
	(
		[TemplateId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
	END

	/****** Object:  Table [HtmlModule]    Script Date: 2016/04/01 12:41:32 ******/

	IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[HtmlModule]') AND type in (N'U'))
	BEGIN
	CREATE TABLE [HtmlModule](
		[HtmlModuleCode] [varchar](50) NOT NULL,
		[HtmlModuleName] [varchar](100) NOT NULL,
		[HtmlModuleDescription] [varchar](300) NOT NULL,
		[HtmlModuleHTML] [ntext] NOT NULL,
		[PageName] [varchar](100) NULL,
		[CreatedDate] [datetime] NULL,
		[ModifiedDate] [datetime] NULL,
		[CreatedUserName] [nvarchar](256) NULL,
		[ModifiedUserName] [nvarchar](256) NULL,
		[HTMLModuleId] [int] IDENTITY(1,2) NOT NULL,
		[ViewControlPath] [varchar](500) NULL,
	 CONSTRAINT [PK_HtmlModule] PRIMARY KEY CLUSTERED 
	(
		[HtmlModuleCode] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
	END

	IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[AddAspNetUserRoles]') AND type in (N'P', N'PC'))
	BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROC [AddAspNetUserRoles]
	@userid nvarchar(128),
	@roleid nvarchar(128)
	AS
	BEGIN
		INSERT INTO [AspNetUserRoles] VALUES (@userid,@roleid) 
	END
	' 
	END

	/****** Object:  StoredProcedure [DeleteAspNetUserRoles]    Script Date: 2016/04/01 12:41:31 ******/

	IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DeleteAspNetUserRoles]') AND type in (N'P', N'PC'))
	BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROC [DeleteAspNetUserRoles]
	@userid nvarchar(128)
	AS
	BEGIN
		DELETE FROM [AspNetUserRoles] WHERE UserId = @userid
	END
	' 
	END
	/****** Object:  StoredProcedure [GetAspNetUserRoles]    Script Date: 2016/04/01 12:41:31 ******/

	IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[GetAspNetUserRoles]') AND type in (N'P', N'PC'))
	BEGIN
	EXEC dbo.sp_executesql @statement = N'CREATE PROC [GetAspNetUserRoles]
	@userid nvarchar(128)
	AS
	BEGIN
		SELECT * from [AspNetUserRoles] where UserId=@userid
	END
	' 
	END


	IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[AspNetUserClaims]'))
	ALTER TABLE [AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
	REFERENCES [AspNetUsers] ([Id])
	ON DELETE CASCADE

	IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[AspNetUserClaims]'))
	ALTER TABLE [AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]

	IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[AspNetUserLogins]'))
	ALTER TABLE [AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
	REFERENCES [AspNetUsers] ([Id])
	ON DELETE CASCADE

	IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[AspNetUserLogins]'))
	ALTER TABLE [AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]

	IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]') AND parent_object_id = OBJECT_ID(N'[AspNetUserRoles]'))
	ALTER TABLE [AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
	REFERENCES [AspNetRoles] ([Id])
	ON DELETE CASCADE

	IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]') AND parent_object_id = OBJECT_ID(N'[AspNetUserRoles]'))
	ALTER TABLE [AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]

	IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[AspNetUserRoles]'))
	ALTER TABLE [AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
	REFERENCES [AspNetUsers] ([Id])
	ON DELETE CASCADE

	IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[AspNetUserRoles]'))
	ALTER TABLE [AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]

	IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK__cms_PageF__PageI__11158940]') AND parent_object_id = OBJECT_ID(N'[cms_PageFieldValues]'))
	ALTER TABLE [cms_PageFieldValues]  WITH CHECK ADD FOREIGN KEY([PageId])
	REFERENCES [cms_Pages] ([PageID])

	IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK__cms_PageF__Templ__1209AD79]') AND parent_object_id = OBJECT_ID(N'[cms_PageFieldValues]'))
	ALTER TABLE [cms_PageFieldValues]  WITH CHECK ADD FOREIGN KEY([TemplateId])
	REFERENCES [cms_Templates] ([TemplateId])

	IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK__cms_PageF__Templ__12FDD1B2]') AND parent_object_id = OBJECT_ID(N'[cms_PageFieldValues]'))
	ALTER TABLE [cms_PageFieldValues]  WITH CHECK ADD FOREIGN KEY([TemplateFieldId])
	REFERENCES [cms_TemplateFields] ([TemplateFieldId])

	IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK__cms_PageT__PageI__0D44F85C]') AND parent_object_id = OBJECT_ID(N'[cms_PageTemplate]'))
	ALTER TABLE [cms_PageTemplate]  WITH CHECK ADD FOREIGN KEY([PageId])
	REFERENCES [cms_Pages] ([PageID])

	IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK__cms_PageT__Templ__0E391C95]') AND parent_object_id = OBJECT_ID(N'[cms_PageTemplate]'))
	ALTER TABLE [cms_PageTemplate]  WITH CHECK ADD FOREIGN KEY([TemplateId])
	REFERENCES [cms_Templates] ([TemplateId])

	IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK__cms_Templ__Templ__0A688BB1]') AND parent_object_id = OBJECT_ID(N'[cms_TemplateFields]'))
	ALTER TABLE [cms_TemplateFields]  WITH CHECK ADD FOREIGN KEY([TemplateId])
	REFERENCES [cms_Templates] ([TemplateId])


	IF NOT EXISTS(SELECT * FROM [__MigrationHistory] WHERE [MigrationId]= '201602011127554_InitialCreate')
	BEGIN
		INSERT [__MigrationHistory] ([MigrationId], [ContextKey], [Model], [ProductVersion]) VALUES (N'201602011127554_InitialCreate', N'HaptiX.BOBROA.Web.Models.ApplicationDbContext', 0x1F8B0800000000000400DD5C5B6FE3B6127E3FC0F90F829E7A0E522B97EE621BD82DB24ED2066773C13ADBF66D414BB443AC4469252A4D70D05FD687F393CE5FE850A26489175D6CC5768A05161139FC66381C92C3E1D0FFFFF37FE31F9F02DF7AC47142423AB18F4687B685A91B7A842E2776CA16DFBEB37FFCE19FFF185F78C193F54B4177C2E9A0254D26F60363D1A9E324EE030E50320A881B8749B86023370C1CE485CEF1E1E1F7CED1918301C2062CCB1A7F4C292301CE3EE0731A5217472C45FE75E8613F11E55033CB50AD1B14E024422E9ED83FA38891DF46EF6FDF7FBC3D1BFD8AE7A3BC8D6D9DF904813C33EC2F6C0B511A32C440DAD34F099EB138A4CB590405C8BF7F8E30D02D909F60D18BD31579D70E1D1EF30E39AB8605949B262C0C7A021E9D080D3972F3B5F46C971A041D5E80AED933EF75A6C7897DE5E1ACE863E883026486A7533FE6C413FBBA6471964437988D8A86A31CF23206B8DFC3F8CBA88A7860756E77505AD4F1E890FF3BB0A6A9CFD2184F284E598CFC03EB2E9DFBC4FD0F7EBE0FBF603A39399A2F4EDEBD798BBC93B7DFE19337D59E425F81AE560045777118E11864C38BB2FFB6E5D4DB3972C3B259A54DAE15B025981CB6758D9E3E60BA640F306D8EDFD9D62579C25E51228CEB13253097A0118B53F8BC497D1FCD7D5CD63B8D3CF9FF0D5C8FDFBC1D84EB0D7A24CB6CE825FE30716298571FB19FD5260F24CAA7576DBC3F0BB2CB380CF877DDBEF2DACFB3308D5DDE99D048728FE2256675E9C6CECA783B9934871ADEAC0BD4FD376D2EA96ADE5A52DEA1756642C162DBB3A190F765F976B6B8B32882C1CB4C8B6BA4C9E04C5BD648C238B014CA95191D7535230ADDFB3BAF8A170122FE00CB62072EE0972C481CE0B297EF433042447BCB7C8792045605EF67943C34880E7F0E20FA0CBB690CC63A6328885E9CDBDD4348F14D1ACCF91CD81EAFC186E6FEF7F012B92C8C2F286FB531DE87D0FD12A6EC827AE788E14FCC2D00F9E73D09BA030C22CE99EBE224B90463C6DE3404B7BB00BCA2ECE4B8371C5FA676ED964C7D4402BD5F222DA89F0BD2956FA2A750FC130399CE476912F543B824B49BA805A959D49CA2555441D657540ED64D524169163423689533A71ACCEBCB466878B72F83DD7FBF6FB3CDDBB41654D438831512FF84298E6119F3EE106338A6AB11E8B26EECC259C8868F337DF1BD29E3F40BF2D3A159AD351BB24560F8D990C1EEFF6CC8C484E247E271AFA4C361A82006F84EF4FA7356FB9C9324DBF674A87573DBCCB7B30698A6CB5992842EC96681260C26821875F9C187B3DA231A796FE4A808740C0C9DF02D0F4AA06FB66C54B7F41CFB9861EBCCCDC3845394B8C853D5081DF27A0856ECA81AC156D191BA70FF567882A5E3983742FC1094C04C2594A9D382509744C86FD592D4B2E316C6FB5EF2906BCE71842967D8AA892ECCF5C1102E40C9471A94360D8D9D8AC5351BA2C16B358D799B0BBB1A772546B1159B6CF19D0D7629FCB71731CC668D6DC1389B55D2450063606F17062ACE2A5D0D403EB8EC9B814A272683810A976A2B065AD7D80E0CB4AE925767A0F911B5EBF84BE7D57D33CFFA4179FBDB7AA3BA76609B357DEC9969E6BE27B461D002C7AA799ECF79257E629AC319C829CE678970756513E1E033CCEA219B95BFABF5439D6610D9889A005786D6022AAE0415206542F510AE88E5354A27BC881EB045DCAD1156ACFD126CC50654ECEAD56885D07C812A1B67A7D347D9B3D21A1423EF7458A8E0680C425EBCEA1DEFA014535C56554C175FB88F375CE998188C0605B578AE0625159D195C4B8569B66B49E790F571C936D292E43E19B4547466702D091B6D5792C629E8E1166CA4A2FA163ED0642B221DE56E53D68D9D3C6F4A148C1D4382D5F81A4511A1CB4AC29528B16679B6D5F4DB59FF04A420C770DC449387544A5B7262618C9658AA05D620E9258913768E189A231EE7997A8142A6DD5B0DCB7FC1B2BA7DAA8358EC030535FF3B6F61BCC8AFEDB8AA4B22902EA19F01F76BB260BAC60AF4CD2D9E06877C146BE2F7D3D04F036A76B3CCADF35BBC6AFBBC4445183B92FC8A1BA5E84C7176EB03D06978D4A931E85095BECCFAC365863029BDF044AB6A3779A76694225855453105B076367C26A7668D2193BDC6FE23D68AF032734CA4AA540144514F8C4AB6830256A9EB8E5A4F48A962D66BBA234A59275548A9AA8794D5DC929A90D58AB5F00C1AD55374E7A0669354D1D5DAEEC89ABC922AB4A67A0D6C8DCC725D77544DEA49155853DD1D7B9587222FA57BBC8B19CF321B6E63F9A177B37DCC80F132EBE230DB60E56EBF0A5429EE89256EEF153051BE9736653CF96D685379C463339B32609857A1DADD787D116ABCD03763D62EBC6B0B7DD385BF19AF9FE5BEA87D28C73F99A4E45E1E03A5E3DE581CBDDA1FDD2867B19CC4B60A35C226FF9C301C8C38C168F6D59FFA04F325BD20B846942C70C2F2240FFBF8F0E8587AB1B33FAF679C24F17CCDD1D5F484A63E665BC8D7A28F28761F50AC664F6CF0C26405AA04A6AFA8879F26F67FB356A7598C83FF95151F5857C9274ABEA650711FA7D8FA43CD061D26E3BEF9D8B5A7EF23BA6BF5EAB7CF79D303EB368619736A1D4ABA5C6784EBAF267A499337DD409AB5DF52BCDE09557B9CA0459526C4FA6F11E6840DF20EA190F29B003DFDABAF68DAB7061B216ADE130C8537880A4DEF05D6C132BE15F0E093656F05FA7556FF76601DD18CEF0608ED0F26BF1AE8BE0C152D77B8D5684E46DB5892323DB7665D6F9482B9EBBD4949CEDE68A2AB09D83DE03648B25EC3325E597EF260BBA326FD7830EC5D9AF68BE71CEF4B9AF12A0164B7D9C5DB4C286EB828FA5BE511EF41E69B269367F7D9C2DBB635533477CF532EFBE504EF99B189FCAEDD67FE6EDBD84C61DE3D37B65EF9BD7B666BBBDA3F776C699DB7D09D67EBAA8947865B195D2CB82D1B370F9CC3097F1E8211E41E65FE88529FFED594BADAC2704562666ACE3B93192B1347E1AB5034B3EDD757B1E1377656D034B335646B36F116EB7F236F41D3CCDB9003B98B3C626D16A22EB7BB651D6B4A8B7A4D79C3B59EB4A4A9B7F9AC8D57ECAF294D7810A5D4668FE18EF8F564050FA29221A74E8F2C60F5BA17F6CECA2F31C2FE9D90E50A82FF2E23C56E6DD72C69AEE8222C366F49A282448AD05C63863CD852CF624616C86550CD63CCD92BF02C6EC76F3AE6D8BBA2B7298B52065DC6C1DCAF05BCB813D0C43F4B75AECB3CBE8DB21F3419A20B2026E1B1F95BFA3E25BE57CA7DA989091920B8772122BA7C2C198FEC2E9F4BA49B90760412EA2B9DA27B1C443E8025B774861EF13AB281F97DC04BE43EAF22802690F681A8AB7D7C4ED0324641223056EDE1136CD80B9E7EF80B3F6F0EC490540000, N'6.1.0-30225')


		INSERT [app_configs] ([groupid], [itemid], [ItemName], [ItemDesc], [IsActive], [ShortDesc], [ItemCode]) VALUES (1, 0, N'AppSettings', N'Application Settings', 1, N'Application Settings', N'AppSettings')

		INSERT [app_configs] ([groupid], [itemid], [ItemName], [ItemDesc], [IsActive], [ShortDesc], [ItemCode]) VALUES (1, 1, N'AccesDeniedPageUrl', N'AcessDenied', 1, N'Custom Access Denied Page', N'AccesDeniedPageUrl')

		INSERT [app_configs] ([groupid], [itemid], [ItemName], [ItemDesc], [IsActive], [ShortDesc], [ItemCode]) VALUES (1, 2, N'LoginPageUrl', N'LoginPage', 1, N'Custom Login Page ', N'LoginPageUrl')

		INSERT [app_configs] ([groupid], [itemid], [ItemName], [ItemDesc], [IsActive], [ShortDesc], [ItemCode]) VALUES (1, 3, N'PageNotFoundUrl', N'PageNotFound', 1, N'Custom Page Not Found Url', N'PageNotFoundUrl')

		INSERT [app_configs] ([groupid], [itemid], [ItemName], [ItemDesc], [IsActive], [ShortDesc], [ItemCode]) VALUES (2, 0, N'FieldTypes', N'FieldTypes', 1, N'FieldTypes', N'FieldTypes')

		INSERT [app_configs] ([groupid], [itemid], [ItemName], [ItemDesc], [IsActive], [ShortDesc], [ItemCode]) VALUES (2, 1, N'SingleLine', N'SingleLine', 1, N'SingleLine', N'SingleLine')

		INSERT [app_configs] ([groupid], [itemid], [ItemName], [ItemDesc], [IsActive], [ShortDesc], [ItemCode]) VALUES (2, 2, N'number', N'number', 1, N'number', N'number')
		INSERT [app_configs] ([groupid], [itemid], [ItemName], [ItemDesc], [IsActive], [ShortDesc], [ItemCode]) VALUES (2, 3, N'date', N'date', 1, N'date', N'date')

		INSERT [app_configs] ([groupid], [itemid], [ItemName], [ItemDesc], [IsActive], [ShortDesc], [ItemCode]) VALUES (2, 4, N'RichText', N'RichText', 1, N'RichText', N'RichText')

		INSERT [app_configs] ([groupid], [itemid], [ItemName], [ItemDesc], [IsActive], [ShortDesc], [ItemCode]) VALUES (2, 5, N'MultiLine', N'MultiLine', 1, N'MultiLine', N'MultiLine')

		INSERT [AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'8975eace-021d-4628-9a43-d18699bf1fb9', N'ashish.haptix@gmail.com', 0, N'APaRevsE9eOsOycf4HnItTOPzxWlhacSrhXu+8RdRAyi8UpXmXI13bXeX2ltInEosQ==', N'2b79e45f-efb4-452d-8925-2ec7b3c2f59c', NULL, 0, 0, NULL, 0, 0, N'admin@ramhx.com')

		INSERT [AspNetRoles] ([Id], [Name]) VALUES (N'5e58a050-4651-447e-aab5-dc684b2525df', N'Admin')

		INSERT [AspNetRoles] ([Id], [Name]) VALUES (N'fa448ae2-7fa3-4468-9e83-00298adeebc8', N'Editor')

		INSERT [AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'8975eace-021d-4628-9a43-d18699bf1fb9', N'5e58a050-4651-447e-aab5-dc684b2525df')
	
		INSERT [cms_Pages] ([ParentPageID], [PageOrder], [PageName], [PageCode], [Description], [PageUrl], [CreatedDate], [CreatedByUserId], [ModifiedDate], [ModifiedByUserId], [PageTitle], [PageMetaKeywords], [PageMetaDescription], [PageLayoutPath]) VALUES (NULL, 1, N'Home', N'HOME_PAGE', N'Home page', N'Home', NULL, NULL, CAST(0x0000A5CE00E12F1D AS DateTime), NULL, N'Home Page', N'Home', N'Default Home', null)

		INSERT [cms_Templates] ([TemplateName], [TemplateCode], [Description], [CreatedDate], [CreatedByUserId], [ModifiedDate], [ModifiedByUserId]) VALUES ( N'News', N'News', N'News', CAST(0x0000A5DB00000000 AS DateTime), NULL, NULL, NULL)

		INSERT [cms_Templates] ( [TemplateName], [TemplateCode], [Description], [CreatedDate], [CreatedByUserId], [ModifiedDate], [ModifiedByUserId]) VALUES ( N'Event', N'Event', N'Event', NULL, NULL, NULL, NULL)

		INSERT [cms_TemplateFields] ([FieldName], [FieldTypeId], [FieldDisplayOrder], [DefaultValue], [TemplateId], [CreatedDate], [CreatedByUserId], [ModifiedDate], [ModifiedByUserId]) VALUES (N'NewsTitle', 1, 1, N'dc', 1, NULL, NULL, NULL, NULL)

		INSERT [cms_TemplateFields] ([FieldName], [FieldTypeId], [FieldDisplayOrder], [DefaultValue], [TemplateId], [CreatedDate], [CreatedByUserId], [ModifiedDate], [ModifiedByUserId]) VALUES ( N'NewsDate', 3, 2, N'', 1, NULL, NULL, NULL, NULL)

		INSERT [cms_TemplateFields] ([FieldName], [FieldTypeId], [FieldDisplayOrder], [DefaultValue], [TemplateId], [CreatedDate], [CreatedByUserId], [ModifiedDate], [ModifiedByUserId]) VALUES ( N'NewsStory', 4, 3, NULL, 1, NULL, NULL, NULL, NULL)

		INSERT [cms_TemplateFields] ([FieldName], [FieldTypeId], [FieldDisplayOrder], [DefaultValue], [TemplateId], [CreatedDate], [CreatedByUserId], [ModifiedDate], [ModifiedByUserId]) VALUES (N'EventTitle', 1, 1, NULL, 2, NULL, NULL, NULL, NULL)

		INSERT [cms_TemplateFields] ([FieldName], [FieldTypeId], [FieldDisplayOrder], [DefaultValue], [TemplateId], [CreatedDate], [CreatedByUserId], [ModifiedDate], [ModifiedByUserId]) VALUES (N'EventStartDate', 3, 2, NULL, 2, NULL, NULL, NULL, NULL)

		INSERT [cms_TemplateFields] ([FieldName], [FieldTypeId], [FieldDisplayOrder], [DefaultValue], [TemplateId], [CreatedDate], [CreatedByUserId], [ModifiedDate], [ModifiedByUserId]) VALUES ( N'EventEndDate', 3, 3, NULL, 2, NULL, NULL, NULL, NULL)

		INSERT [cms_TemplateFields] ([FieldName], [FieldTypeId], [FieldDisplayOrder], [DefaultValue], [TemplateId], [CreatedDate], [CreatedByUserId], [ModifiedDate], [ModifiedByUserId]) VALUES (N'EventDetails', 5, 4, NULL, 2, NULL, NULL, NULL, NULL)

	END

	/* Rakesh Angre - 01-Apr-2016 04:00 PM - Added new Field Typed */
	IF NOT EXISTS(Select * from [app_Configs] where GroupId = 2 and ItemId = 6)
	BEGIN
	insert into [app_Configs](GroupId,ItemId,ItemName,ItemDesc,IsActive,ItemCode,ShortDesc) values (2,6,'Time','Time',1,'Time','Time')
	END
	IF NOT EXISTS(Select * from [app_Configs] where GroupId = 2 and ItemId = 7)
	BEGIN
	insert into [app_Configs](GroupId,ItemId,ItemName,ItemDesc,IsActive,ItemCode,ShortDesc) values (2,7,'DateTime','DateTime',1,'DateTime','DateTime')
	END
	IF NOT EXISTS(Select * from [app_Configs] where GroupId = 2 and ItemId = 8)
	BEGIN
	insert into [app_Configs](GroupId,ItemId,ItemName,ItemDesc,IsActive,ItemCode,ShortDesc) values (2,8,'Media','Media',1,'Media','Media')
	END


	/* Rakesh Angre - 01-Apr-2016 06:24 PM - Added two new columns*/
	IF NOT EXISTS(SELECT * FROM sys.columns 
				WHERE Name = N'DisplayName' AND Object_ID = Object_ID(N'cms_TemplateFields'))
	BEGIN
		alter table [cms_TemplateFields]
			add DisplayName varchar(100),Notes varchar(200)
	END

	/* Ashish Patel - 02-Apr-2016 07:30 PM - added new table to maintain upgrade history*/
	IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'cms_UpgradeHistory') AND type in (N'U'))
	BEGIN
		Create Table cms_UpgradeHistory
		(
			Script varchar(500) not null primary key,
			ReleasedDate varchar(50),
			InstalledDate datetime not null,
			ReleasedNote varchar(500)
		)
	END

	/* Rakesh Angre - 04-Apr-2016 02:50 PM - Updated existing column*/
	update [dbo].[app_Configs] set ItemName='CheckBox', ItemDesc='CheckBox',ShortDesc='CheckBox',ItemCode='CheckBox'
	where GroupId = 2 and ItemId = 7
END