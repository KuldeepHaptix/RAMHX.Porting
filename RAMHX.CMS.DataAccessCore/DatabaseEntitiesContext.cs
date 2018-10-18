using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RAMHX.CMS.DataAccessCore
{
    public partial class DatabaseEntities : DbContext
    {
        public DatabaseEntities()
        {
        }

        public DatabaseEntities(DbContextOptions<DatabaseEntities> options)
            : base(options)
        {
        }

        public virtual DbSet<AppConfigs> AppConfigs { get; set; }
        public virtual DbSet<AppForgotPasswordToken> AppForgotPasswordToken { get; set; }
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<Cms301redirection> Cms301redirection { get; set; }
        public virtual DbSet<CmsPackageInstallations> CmsPackageInstallations { get; set; }
        public virtual DbSet<CmsPageFieldValues> CmsPageFieldValues { get; set; }
        public virtual DbSet<CmsPageHtmlmodules> CmsPageHtmlmodules { get; set; }
        public virtual DbSet<CmsPageRoles> CmsPageRoles { get; set; }
        public virtual DbSet<CmsPages> CmsPages { get; set; }
        public virtual DbSet<CmsPageTemplate> CmsPageTemplate { get; set; }
        public virtual DbSet<CmsTemplateFields> CmsTemplateFields { get; set; }
        public virtual DbSet<CmsTemplates> CmsTemplates { get; set; }
        public virtual DbSet<CmsUpgradeHistory> CmsUpgradeHistory { get; set; }
        public virtual DbSet<CoreModuleBlogCategory> CoreModuleBlogCategory { get; set; }
        public virtual DbSet<CoreModuleBlogs> CoreModuleBlogs { get; set; }
        public virtual DbSet<CoreModuleEventCategory> CoreModuleEventCategory { get; set; }
        public virtual DbSet<CoreModuleEvents> CoreModuleEvents { get; set; }
        public virtual DbSet<CoreModuleFaqcategory> CoreModuleFaqcategory { get; set; }
        public virtual DbSet<CoreModuleFaqmaster> CoreModuleFaqmaster { get; set; }
        public virtual DbSet<CoreModuleGalleryAlbum> CoreModuleGalleryAlbum { get; set; }
        public virtual DbSet<CoreModuleGalleryAlbumItem> CoreModuleGalleryAlbumItem { get; set; }
        public virtual DbSet<CoreModuleGalleryCategory> CoreModuleGalleryCategory { get; set; }
        public virtual DbSet<CoreModuleJobCategory> CoreModuleJobCategory { get; set; }
        public virtual DbSet<CoreModuleJobMaster> CoreModuleJobMaster { get; set; }
        public virtual DbSet<CoreModuleNews> CoreModuleNews { get; set; }
        public virtual DbSet<CoreModuleNewsCategory> CoreModuleNewsCategory { get; set; }
        public virtual DbSet<CoreModulePackageCategory> CoreModulePackageCategory { get; set; }
        public virtual DbSet<CoreModulePackageMaster> CoreModulePackageMaster { get; set; }
        public virtual DbSet<CoreModuleProductCategory> CoreModuleProductCategory { get; set; }
        public virtual DbSet<CoreModuleProductMaster> CoreModuleProductMaster { get; set; }
        public virtual DbSet<CoreModuleProjectCategory> CoreModuleProjectCategory { get; set; }
        public virtual DbSet<CoreModuleProjectMaster> CoreModuleProjectMaster { get; set; }
        public virtual DbSet<CoreModuleSliderItems> CoreModuleSliderItems { get; set; }
        public virtual DbSet<CoreModuleSliders> CoreModuleSliders { get; set; }
        public virtual DbSet<CoreModuleSmsqueue> CoreModuleSmsqueue { get; set; }
        public virtual DbSet<CoreModuleTestimonialCategory> CoreModuleTestimonialCategory { get; set; }
        public virtual DbSet<CoreModuleTestimonialMaster> CoreModuleTestimonialMaster { get; set; }
        public virtual DbSet<HtmlModule> HtmlModule { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=SOURCEVED01\\MSSQL2016;Database=RAMHX_DB;User ID=sa;Password=123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppConfigs>(entity =>
            {
                entity.HasKey(e => new { e.GroupId, e.ItemId });

                entity.ToTable("app_Configs");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ItemCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ItemDesc)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ItemName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ShortDesc)
                    .HasMaxLength(300)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AppForgotPasswordToken>(entity =>
            {
                entity.HasKey(e => e.TokenId);

                entity.Property(e => e.TokenId).ValueGeneratedNever();

                entity.Property(e => e.CodeToken)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.ExpireDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);
               
                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");
                entity.Ignore(e=>e.AspNetUserClaims);
                entity.Ignore(e => e.AspNetUserLogins);
                entity.Ignore(e => e.AspNetUserRoles);
                entity.Ignore(e => e.AspNetUserTokens);
                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(450);

                entity.Property(e => e.City).HasMaxLength(450);

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.FirstName).HasMaxLength(450);

                entity.Property(e => e.Gender).HasMaxLength(20);

                entity.Property(e => e.LastName).HasMaxLength(450);

                entity.Property(e => e.LockoutEndDateUtc).HasColumnType("datetime");

                entity.Property(e => e.Mobile).HasMaxLength(20);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Cms301redirection>(entity =>
            {
                entity.HasKey(e => e.Rid);

                entity.ToTable("cms_301Redirection");

                entity.HasIndex(e => e.FromUrl)
                    .HasName("UQ__cms_301R__A30616D7639B6A33")
                    .IsUnique();

                entity.Property(e => e.Rid)
                    .HasColumnName("rid")
                    .ValueGeneratedNever();

                entity.Property(e => e.FromUrl)
                    .IsRequired()
                    .HasColumnName("fromUrl")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ToUrl)
                    .IsRequired()
                    .HasColumnName("toUrl")
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CmsPackageInstallations>(entity =>
            {
                entity.HasKey(e => e.PackageId);

                entity.ToTable("cms_PackageInstallations");

                entity.Property(e => e.PackageId).ValueGeneratedNever();

                entity.Property(e => e.ComplatedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PackagePath)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ValidationErrors).HasColumnType("text");
            });

            modelBuilder.Entity<CmsPageFieldValues>(entity =>
            {
                entity.HasKey(e => new { e.PageId, e.TemplateId, e.TemplateFieldId });

                entity.ToTable("cms_PageFieldValues");

                entity.Property(e => e.FieldValue).HasColumnType("ntext");

                entity.HasOne(d => d.Page)
                    .WithMany(p => p.CmsPageFieldValues)
                    .HasForeignKey(d => d.PageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__cms_PageF__PageI__619B8048");

                entity.HasOne(d => d.TemplateField)
                    .WithMany(p => p.CmsPageFieldValues)
                    .HasForeignKey(d => d.TemplateFieldId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__cms_PageF__Templ__6383C8BA");

                entity.HasOne(d => d.Template)
                    .WithMany(p => p.CmsPageFieldValues)
                    .HasForeignKey(d => d.TemplateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__cms_PageF__Templ__628FA481");
            });

            modelBuilder.Entity<CmsPageHtmlmodules>(entity =>
            {
                entity.HasKey(e => e.PageHtmlmoduleId);

                entity.ToTable("cms_PageHTMLModules");

                entity.Property(e => e.PageHtmlmoduleId)
                    .HasColumnName("PageHTMLModuleId")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.HtmlmoduleId).HasColumnName("HTMLModuleId");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PageId).HasColumnName("PageID");
            });

            modelBuilder.Entity<CmsPageRoles>(entity =>
            {
                entity.HasKey(e => new { e.PageId, e.RoleId });

                entity.ToTable("cms_PageRoles");

                entity.Property(e => e.RoleId).HasMaxLength(128);
            });

            modelBuilder.Entity<CmsPages>(entity =>
            {
                entity.HasKey(e => e.PageId);
                entity.Ignore(e=>e.CmsPageFieldValues);
                entity.Ignore(e => e.CmsPageTemplate);
                entity.Ignore(e => e.InverseParentPage);
                entity.Ignore(e => e.CmsTemplates);
                entity.ToTable("cms_Pages");

                entity.Property(e => e.PageId)
                    .HasColumnName("PageID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(512);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PageCode)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PageLayoutPath)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.PageName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.PageTitle).HasMaxLength(1000);

                entity.Property(e => e.PageUrl).HasMaxLength(500);

                entity.Property(e => e.ParentPageId).HasColumnName("ParentPageID");

                entity.Property(e => e.ShowInNavigation)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.ParentPage)
                    .WithMany(p => p.InverseParentPage)
                    .HasForeignKey(d => d.ParentPageId)
                    .HasConstraintName("FK__cms_Pages__Paren__4F7CD00D");
            });

            modelBuilder.Entity<CmsPageTemplate>(entity =>
            {
                entity.HasKey(e => new { e.PageId, e.TemplateId });

                entity.ToTable("cms_PageTemplate");

                entity.HasOne(d => d.Page)
                    .WithMany(p => p.CmsPageTemplate)
                    .HasForeignKey(d => d.PageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__cms_PageT__PageI__5AEE82B9");

                entity.HasOne(d => d.Template)
                    .WithMany(p => p.CmsPageTemplate)
                    .HasForeignKey(d => d.TemplateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__cms_PageT__Templ__5BE2A6F2");
            });

            modelBuilder.Entity<CmsTemplateFields>(entity =>
            {
                entity.HasKey(e => e.TemplateFieldId);

                entity.ToTable("cms_TemplateFields");

                entity.Property(e => e.TemplateFieldId).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DisplayName).HasMaxLength(100);

                entity.Property(e => e.FieldName).HasMaxLength(500);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Notes).HasMaxLength(200);

                entity.HasOne(d => d.Template)
                    .WithMany(p => p.CmsTemplateFields)
                    .HasForeignKey(d => d.TemplateId)
                    .HasConstraintName("FK__cms_Templ__Templ__5EBF139D");
            });

            modelBuilder.Entity<CmsTemplates>(entity =>
            {
                entity.HasKey(e => e.TemplateId);

                entity.Ignore(e=>e.CmsPageFieldValues);

                entity.Ignore(e => e.CmsPageTemplate);

                entity.Ignore(e => e.CmsTemplateFields);

                entity.ToTable("cms_Templates");

                entity.Property(e => e.TemplateId).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(512);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.TemplateCode)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.TemplateName)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<CmsUpgradeHistory>(entity =>
            {
                entity.HasKey(e => e.Script);

                entity.ToTable("cms_UpgradeHistory");

                entity.Property(e => e.Script)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.InstalledDate).HasColumnType("datetime");

                entity.Property(e => e.ReleasedDate)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ReleasedNote)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CoreModuleBlogCategory>(entity =>
            {
                entity.ToTable("CoreModule_BlogCategory");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DisplayOrder).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<CoreModuleBlogs>(entity =>
            {
                entity.ToTable("CoreModule_Blogs");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Author)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.BlogShortDesc)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.BlogTitle)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DisplayOrder).HasDefaultValueSql("((1))");

                entity.Property(e => e.LongDesc).HasColumnType("text");

                entity.Property(e => e.PublishDate).HasColumnType("datetime");

                entity.Property(e => e.Tags)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CoreModuleEventCategory>(entity =>
            {
                entity.ToTable("CoreModule_EventCategory");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.DisplayOrder).HasDefaultValueSql("((1))");

                entity.Property(e => e.EventName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CoreModuleEvents>(entity =>
            {
                entity.ToTable("CoreModule_Events");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.DisplayOrder).HasDefaultValueSql("((1))");

                entity.Property(e => e.EventDateTime).HasColumnType("datetime");

                entity.Property(e => e.EventRegistrationStartsOn).HasColumnType("datetime");

                entity.Property(e => e.Location)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LongDesc).IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.PhotoUrl)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.RegistrationEndOn).HasColumnType("datetime");

                entity.Property(e => e.ShortDesc)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CoreModuleFaqcategory>(entity =>
            {
                entity.ToTable("CoreModule_FAQCategory");

                entity.Property(e => e.DisplayOrder).HasDefaultValueSql("((1))");

                entity.Property(e => e.FaqcategoryName)
                    .HasColumnName("FAQCategoryName")
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CoreModuleFaqmaster>(entity =>
            {
                entity.ToTable("CoreModule_FAQMaster");

                entity.Property(e => e.Answer).IsUnicode(false);

                entity.Property(e => e.DisplayOrder).HasDefaultValueSql("((1))");

                entity.Property(e => e.Question).IsUnicode(false);
            });

            modelBuilder.Entity<CoreModuleGalleryAlbum>(entity =>
            {
                entity.ToTable("CoreModule_GalleryAlbum");

                entity.Property(e => e.DisplayOrder).HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ThumbnailPath)
                    .HasMaxLength(1000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CoreModuleGalleryAlbumItem>(entity =>
            {
                entity.ToTable("CoreModule_GalleryAlbumItem");

                entity.Property(e => e.DisplayOrder).HasDefaultValueSql("((1))");

                entity.Property(e => e.ItemPath)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UploadType).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<CoreModuleGalleryCategory>(entity =>
            {
                entity.ToTable("CoreModule_GalleryCategory");

                entity.Property(e => e.DisplayOrder).HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CoreModuleJobCategory>(entity =>
            {
                entity.ToTable("CoreModule_JobCategory");

                entity.Property(e => e.DisplayOrder).HasDefaultValueSql("((1))");

                entity.Property(e => e.JobCategoryName)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CoreModuleJobMaster>(entity =>
            {
                entity.ToTable("CoreModule_JobMaster");

                entity.Property(e => e.DisplayOrder).HasDefaultValueSql("((1))");

                entity.Property(e => e.Experience)
                    .HasColumnName("EXPERIENCE")
                    .IsUnicode(false);

                entity.Property(e => e.JobTitle)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Qualification)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Specialization)
                    .HasColumnName("SPECIALIZATION")
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CoreModuleNews>(entity =>
            {
                entity.ToTable("CoreModule_News");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.AttachmentPath)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.DisplayOrder).HasDefaultValueSql("((1))");

                entity.Property(e => e.LongDesc).HasColumnType("text");

                entity.Property(e => e.NewsDate).HasColumnType("datetime");

                entity.Property(e => e.NewsShortDesc)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.NewsTitle)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CoreModuleNewsCategory>(entity =>
            {
                entity.ToTable("CoreModule_NewsCategory");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DisplayOrder).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<CoreModulePackageCategory>(entity =>
            {
                entity.ToTable("CoreModule_PackageCategory");

                entity.Property(e => e.DisplayOrder).HasDefaultValueSql("((1))");

                entity.Property(e => e.PackCategoryName)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CoreModulePackageMaster>(entity =>
            {
                entity.ToTable("CoreModule_PackageMaster");

                entity.Property(e => e.DisplayOrder).HasDefaultValueSql("((1))");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.LongDesc).IsUnicode(false);

                entity.Property(e => e.Mrp)
                    .HasColumnName("MRP")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.OfferPrice).HasColumnType("numeric(10, 2)");

                entity.Property(e => e.PackName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ShortDesc)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Usage)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CoreModuleProductCategory>(entity =>
            {
                entity.ToTable("CoreModule_ProductCategory");

                entity.Property(e => e.DisplayOrder).HasDefaultValueSql("((1))");

                entity.Property(e => e.ProdCategoryName)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CoreModuleProductMaster>(entity =>
            {
                entity.ToTable("CoreModule_ProductMaster");

                entity.Property(e => e.DisplayOrder).HasDefaultValueSql("((1))");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.LongDesc).IsUnicode(false);

                entity.Property(e => e.Mrp)
                    .HasColumnName("MRP")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.OfferPrice).HasColumnType("numeric(10, 2)");

                entity.Property(e => e.ProdName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ShortDesc)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Usage)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CoreModuleProjectCategory>(entity =>
            {
                entity.ToTable("CoreModule_ProjectCategory");

                entity.Property(e => e.DisplayOrder).HasDefaultValueSql("((1))");

                entity.Property(e => e.ProjCategoryName)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CoreModuleProjectMaster>(entity =>
            {
                entity.ToTable("CoreModule_ProjectMaster");

                entity.Property(e => e.DisplayOrder).HasDefaultValueSql("((1))");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.LongDesc).IsUnicode(false);

                entity.Property(e => e.ProjName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ShortDesc)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CoreModuleSliderItems>(entity =>
            {
                entity.ToTable("CoreModule_SliderItems");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.DisplayOrder).HasDefaultValueSql("((1))");

                entity.Property(e => e.FirstButtonLink)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.FirstButtonText)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.FirstLine)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ForthLine)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.SecondButtonLink)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.SecondButtonText)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.SecondLine)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.SliderImagePath)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ThirdLine)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CoreModuleSliders>(entity =>
            {
                entity.ToTable("CoreModule_Sliders");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.DisplayOrder).HasDefaultValueSql("((1))");

                entity.Property(e => e.SliderCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SliderName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CoreModuleSmsqueue>(entity =>
            {
                entity.ToTable("CoreModule_SMSQueue");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.SenderName)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SentDate).HasColumnType("datetime");

                entity.Property(e => e.Smsnumber)
                    .HasColumnName("SMSNumber")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Smstext)
                    .HasColumnName("SMSText")
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CoreModuleTestimonialCategory>(entity =>
            {
                entity.ToTable("CoreModule_TestimonialCategory");

                entity.Property(e => e.DisplayOrder).HasDefaultValueSql("((1))");

                entity.Property(e => e.TestimonialCategoryName)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CoreModuleTestimonialMaster>(entity =>
            {
                entity.ToTable("CoreModule_TestimonialMaster");

                entity.Property(e => e.DisplayOrder).HasDefaultValueSql("((1))");

                entity.Property(e => e.Experience)
                    .HasColumnName("EXPERIENCE")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.PhotoPath)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<HtmlModule>(entity =>
            {
                entity.HasIndex(e => e.HtmlModuleCode)
                    .HasName("UQ__HtmlModu__E6577D6B233805F3")
                    .IsUnique();

                entity.Property(e => e.HtmlmoduleId)
                    .HasColumnName("HTMLModuleId")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedUserName).HasMaxLength(256);

                entity.Property(e => e.HtmlModuleCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HtmlModuleDescription)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.HtmlModuleHtml)
                    .IsRequired()
                    .HasColumnName("HtmlModuleHTML")
                    .HasColumnType("ntext");

                entity.Property(e => e.HtmlModuleName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedUserName).HasMaxLength(256);

                entity.Property(e => e.PageName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });
        }
    }
}
