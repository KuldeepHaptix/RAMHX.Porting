Insert into app_Configs(GroupId,ItemId,ItemName,ItemDesc,IsActive,ShortDesc,ItemCode) values (1,6,'RAMHX.Enabled.UnderMaintanance','0',1,'set 1 to enabled UnderMaintanance and or CommingSoon','RAMHX.Enabled.UnderMaintanance')
Go
Insert into app_Configs(GroupId,ItemId,ItemName,ItemDesc,IsActive,ShortDesc,ItemCode) values (1,7,'RAMHX.Enabled.CommingSoon','0',1,'set 1 to enabled UnderMaintanance and or CommingSoon','RAMHX.Enabled.CommingSoon')
Go

Insert into app_Configs(GroupId,ItemId,ItemName,ItemDesc,IsActive,ShortDesc,ItemCode) values (1,8,'SmtpServer','smtp.gmail.com',1,'Email Configuration','SmtpServer')
Go

Insert into app_Configs(GroupId,ItemId,ItemName,ItemDesc,IsActive,ShortDesc,ItemCode) values (1,9,'SmtpUsername','viral.haptix@gmail.com',1,'Email Configuration','SmtpUsername')
Go

Insert into app_Configs(GroupId,ItemId,ItemName,ItemDesc,IsActive,ShortDesc,ItemCode) values (1,10,'SmtpPassword','Uz88$mZ0o7M0#6T',1,'Email Configuration','SmtpPassword')
Go

Insert into app_Configs(GroupId,ItemId,ItemName,ItemDesc,IsActive,ShortDesc,ItemCode) values (1,11,'SmtpRequiredSSL','true',1,'Email Configuration','SmtpRequiredSSL')
Go

Insert into app_Configs(GroupId,ItemId,ItemName,ItemDesc,IsActive,ShortDesc,ItemCode) values (1,12,'SmtpPort','587',1,'Email Configuration','SmtpPort')
Go

Insert into app_Configs(GroupId,ItemId,ItemName,ItemDesc,IsActive,ShortDesc,ItemCode) values (1,13,'From.EmailID','info@haptix.biz',1,'Email Configuration','From.EmailID')
Go

Insert into app_Configs(GroupId,ItemId,ItemName,ItemDesc,IsActive,ShortDesc,ItemCode) values (1,14,'ContactUs.To.EmailID','info@haptix.biz',1,'Email Configuration','ContactUs.To.EmailID')
Go

Insert into app_Configs(GroupId,ItemId,ItemName,ItemDesc,IsActive,ShortDesc,ItemCode) values (1,15,'SenderName','HAPTIX',1,'Email Configuration','SenderName')
Go

Insert into app_Configs(GroupId,ItemId,ItemName,ItemDesc,IsActive,ShortDesc,ItemCode) values (1,16,'SMS.Enabled','0',1,'Email Configuration','SMS.Enabled')
Go

Insert into app_Configs(GroupId,ItemId,ItemName,ItemDesc,IsActive,ShortDesc,ItemCode) values (1,17,'RAMHX.Cache.Duration','5',1,'Caching duration in minutes, -1 to not cache anything, default it 5 mins','RAMHX.Cache.Duration')
Go

Insert into app_Configs(GroupId,ItemId,ItemName,ItemDesc,IsActive,ShortDesc,ItemCode) values (1,18,'RAMHX.AllowAutoBack','true',1,'AllowAutoBack','RAMHX.AllowAutoBack')
Go

Insert into app_Configs(GroupId,ItemId,ItemName,ItemDesc,IsActive,ShortDesc,ItemCode) values (1,19,'RAMHX.SessionCookiesTimeOutInMinutes','20',1,'Session Cookies Expiration Timeout in Minute : Default-30 mins','RAMHX.SessionCookiesTimeOutInMinutes')
Go

Insert into app_Configs(GroupId,ItemId,ItemName,ItemDesc,IsActive,ShortDesc,ItemCode) values (1,20,'SiteTemplates','http://www.haptix.biz/RAMHX.SiteTemplate.Packages/sitetemplates.xml',1,'Session Cookies Expiration Timeout in Minute : Default-30 mins','SiteTemplates')
Go

Insert into app_Configs(GroupId,ItemId,ItemName,ItemDesc,IsActive,ShortDesc,ItemCode) values (1,21,'FileManager.Edit.SupportExtenstion','doc,docx,png,jpeg,jpg,pdf,html,txt,aspx,ascx,css,js,xml',1,'Filemanager supported extenstion list for edit in file manager.list must be comma separated','FileManager.Edit.SupportExtenstion')
Go

Insert into app_Configs(GroupId,ItemId,ItemName,ItemDesc,IsActive,ShortDesc,ItemCode) values (1,22,'FileManager.Upload.SupportExtenstion','jpg,jpeg,doc,docx,zip,gif,png,pdf,rar,svg,svgz,xls,xlsx,ppt,pps,pptx,js,7z,rar,rtf,wpd,wks,wps,gif,bmp,tif,tiff,*',1,'Filemanager supported extenstion list to ALLOW UPLOAD in file manager.list must be comma separated','FileManager.Upload.SupportExtenstion')
Go

Insert into app_Configs(GroupId,ItemId,ItemName,ItemDesc,IsActive,ShortDesc,ItemCode) values (1,23,'OnlyStrongPassword','1',1,'Filemanager supported extenstion list to ALLOW UPLOAD in file manager.list must be comma separated','OnlyStrongPassword')
Go

Insert into app_Configs(GroupId,ItemId,ItemName,ItemDesc,IsActive,ShortDesc,ItemCode) values (1,25,'RAMHX.AllowLoginWithMobile','0',1,'set 1 to allow login with mobile number and/or email respectively','RAMHX.AllowLoginWithMobile')
Go

Insert into app_Configs(GroupId,ItemId,ItemName,ItemDesc,IsActive,ShortDesc,ItemCode) values (1,24,'RAMHX.AllowLoginWithEmail','0',1,'set 1 to redirect your custom login page','RAMHX.AllowLoginWithEmail')
Go

Insert into app_Configs(GroupId,ItemId,ItemName,ItemDesc,IsActive,ShortDesc,ItemCode) values (1,26,'SMS.Key','5cc3dd3e-3aae-4a9f-b9a5-2a0ffb838d39',1,'SMS WebAPI Key','SMS.Key')
Go
Insert into app_Configs(GroupId,ItemId,ItemName,ItemDesc,IsActive,ShortDesc,ItemCode) values (1,27,'SMS.Username','ashishpatel',1,'SMS Web API Username','SMS.Username')
Go

Insert into app_Configs(GroupId,ItemId,ItemName,ItemDesc,IsActive,ShortDesc,ItemCode) values (1,28,'SMS.WebUrl','http://sms.hspsms.com/sendSMS?',1,'SMS Web API URL','SMS.WebUrl')
Go

Create Table [dbo].AppForgotPasswordToken
(
	TokenId  uniqueidentifier Primary Key,
	CodeToken varchar(6),
	UserId uniqueidentifier,
	ExpireDateTime Datetime,
	CreatedDateTime Datetime
)
Go

ALTER TABLE CoreModule_GalleryAlbumItem
ADD UploadType int not null default(1);
Go
