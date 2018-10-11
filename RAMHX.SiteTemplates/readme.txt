Steps to create new theme for RAMHX.

Step1: Create new folder in "RAXHX.SiteTemplates" Directory with your theme name.

Step2: Create new folders inside of newly created folder
		1. Database
		2. Package
		3. Site
		4. Theme

		Note: Database - Database folder contails full database backup after theme gets ready.
		  Package - Package folder contails modified files during theme implementation and database package.
		  Sites - Public Site
		  Theme - Origional theme for further referance

Step3: Extrate "site.zip" in  "Site" Folder.

Step4: Create New Database is SQL Server. 

Step5: Open IIS and map "site" folder and complete installation process.

Step6: Create "Assets" folder and put CSS, JS, Image etc. theme related files.

Step7: Add JS, CSS, and Image file folder referance in "\Site\Views\Shared\_Layout.cshtml".

Step8: Login with admin account and create Pages and HTML Module and apply as per theme.