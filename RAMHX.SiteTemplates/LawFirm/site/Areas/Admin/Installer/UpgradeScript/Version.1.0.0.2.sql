IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_NAME = 'cms_301Redirection'))
BEGIN
   
	CREATE TABLE [dbo].[cms_301Redirection](
		[rid] [uniqueidentifier] NOT NULL PRIMARY KEY,
		[fromUrl] [varchar](500) NOT NULL UNIQUE,
		[toUrl] [varchar](500) NOT NULL,
		[Active] [bit] NOT NULL)
	
END
GO