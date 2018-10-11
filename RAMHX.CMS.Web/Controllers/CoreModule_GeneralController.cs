using RAMHX.CMS.DataAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RAMHX.CMS.Web.Controllers
{
    public class CoreModule_GeneralController : Controller
    {
        private DatabaseEntities db = new DatabaseEntities();

        [Authorize]
        public JsonResult ChangeProductDisplayOrder(int id, int upDown)
        {
            var product = db.CoreModule_ProductMasters.FirstOrDefault(p => p.Id == id);
            int displayOrder = 1;
            product.DisplayOrder = (upDown == 1) ? product.DisplayOrder + 1 : product.DisplayOrder - 1;
            db.SaveChanges();
            var prods = db.CoreModule_ProductMasters.Where(p => p.Id != product.Id).OrderBy(p => p.DisplayOrder).ToList();
            foreach (var prod in prods)
            {
                if (displayOrder == product.DisplayOrder)
                {
                    displayOrder++;
                }

                prod.DisplayOrder = displayOrder++;
                db.SaveChanges();
            }

            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult ChangeProdCategoryDisplayOrder(int id, int upDown)
        {
            var productCat = db.CoreModule_ProductCategories.FirstOrDefault(p => p.Id == id);
            int displayOrder = 1;
            productCat.DisplayOrder = (upDown == 1) ? productCat.DisplayOrder + 1 : productCat.DisplayOrder - 1;
            db.SaveChanges();
            var prods = db.CoreModule_ProductCategories.Where(p => p.Id != productCat.Id).OrderBy(p => p.DisplayOrder).ToList();
            foreach (var prod in prods)
            {
                if (displayOrder == productCat.DisplayOrder)
                {
                    displayOrder++;
                }

                prod.DisplayOrder = displayOrder++;
                db.SaveChanges();
            }
            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult ChangePackCategoryDisplayOrder(int id, int upDown)
        {
            var packCat = db.CoreModule_PackageCategories.FirstOrDefault(p => p.Id == id);
            int displayOrder = 1;
            packCat.DisplayOrder = (upDown == 1) ? packCat.DisplayOrder + 1 : packCat.DisplayOrder - 1;
            db.SaveChanges();
            var packages = db.CoreModule_PackageCategories.Where(p => p.Id != packCat.Id).OrderBy(p => p.DisplayOrder).ToList();
            foreach (var prod in packages)
            {
                if (displayOrder == packCat.DisplayOrder)
                {
                    displayOrder++;
                }

                prod.DisplayOrder = displayOrder++;
                db.SaveChanges();
            }
            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult ChangePackageDisplayOrder(int id, int upDown)
        {
            var item = db.CoreModule_PackageMasters.FirstOrDefault(p => p.Id == id);
            int displayOrder = 1;
            item.DisplayOrder = (upDown == 1) ? item.DisplayOrder + 1 : item.DisplayOrder - 1;
            db.SaveChanges();
            var items = db.CoreModule_PackageMasters.Where(p => p.Id != item.Id).OrderBy(p => p.DisplayOrder).ToList();
            foreach (var itm in items)
            {
                if (displayOrder == item.DisplayOrder)
                {
                    displayOrder++;
                }

                itm.DisplayOrder = displayOrder++;
                db.SaveChanges();
            }
            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult ChangeProjCategoryDisplayOrder(int id, int upDown)
        {
            var item = db.CoreModule_ProjectCategories.FirstOrDefault(p => p.Id == id);
            int displayOrder = 1;
            item.DisplayOrder = (upDown == 1) ? item.DisplayOrder + 1 : item.DisplayOrder - 1;
            db.SaveChanges();
            var items = db.CoreModule_ProjectCategories.Where(p => p.Id != item.Id).OrderBy(p => p.DisplayOrder).ToList();
            foreach (var itm in items)
            {
                if (displayOrder == item.DisplayOrder)
                {
                    displayOrder++;
                }

                itm.DisplayOrder = displayOrder++;
                db.SaveChanges();
            }
            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult ChangeProjectDisplayOrder(int id, int upDown)
        {
            var item = db.CoreModule_ProjectMasters.FirstOrDefault(p => p.Id == id);
            int displayOrder = 1;
            item.DisplayOrder = (upDown == 1) ? item.DisplayOrder + 1 : item.DisplayOrder - 1;
            db.SaveChanges();
            var items = db.CoreModule_ProjectMasters.Where(p => p.Id != item.Id).OrderBy(p => p.DisplayOrder).ToList();
            foreach (var itm in items)
            {
                if (displayOrder == item.DisplayOrder)
                {
                    displayOrder++;
                }

                itm.DisplayOrder = displayOrder++;
                db.SaveChanges();
            }
            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult ChangeJobCategoryDisplayOrder(int id, int upDown)
        {
            var item = db.CoreModule_JobCategories.FirstOrDefault(p => p.Id == id);
            int displayOrder = 1;
            item.DisplayOrder = (upDown == 1) ? item.DisplayOrder + 1 : item.DisplayOrder - 1;
            db.SaveChanges();
            var items = db.CoreModule_JobCategories.Where(p => p.Id != item.Id).OrderBy(p => p.DisplayOrder).ToList();
            foreach (var itm in items)
            {
                if (displayOrder == item.DisplayOrder)
                {
                    displayOrder++;
                }

                itm.DisplayOrder = displayOrder++;
                db.SaveChanges();
            }
            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult ChangeJobDisplayOrder(int id, int upDown)
        {
            var item = db.CoreModule_JobMasters.FirstOrDefault(p => p.Id == id);
            int displayOrder = 1;
            item.DisplayOrder = (upDown == 1) ? item.DisplayOrder + 1 : item.DisplayOrder - 1;
            db.SaveChanges();
            var items = db.CoreModule_JobMasters.Where(p => p.Id != item.Id).OrderBy(p => p.DisplayOrder).ToList();
            foreach (var itm in items)
            {
                if (displayOrder == item.DisplayOrder)
                {
                    displayOrder++;
                }

                itm.DisplayOrder = displayOrder++;
                db.SaveChanges();
            }
            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult ChangeTestimonialCategoryDisplayOrder(int id, int upDown)
        {
            var item = db.CoreModule_TestimonialCategories.FirstOrDefault(p => p.Id == id);
            int displayOrder = 1;
            item.DisplayOrder = (upDown == 1) ? item.DisplayOrder + 1 : item.DisplayOrder - 1;
            db.SaveChanges();
            var items = db.CoreModule_TestimonialCategories.Where(p => p.Id != item.Id).OrderBy(p => p.DisplayOrder).ToList();
            foreach (var itm in items)
            {
                if (displayOrder == item.DisplayOrder)
                {
                    displayOrder++;
                }

                itm.DisplayOrder = displayOrder++;
                db.SaveChanges();
            }
            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult ChangeTestimonialDisplayOrder(int id, int upDown)
        {
            var item = db.CoreModule_TestimonialMasters.FirstOrDefault(p => p.Id == id);
            int displayOrder = 1;
            item.DisplayOrder = (upDown == 1) ? item.DisplayOrder + 1 : item.DisplayOrder - 1;
            db.SaveChanges();
            var items = db.CoreModule_TestimonialMasters.Where(p => p.Id != item.Id).OrderBy(p => p.DisplayOrder).ToList();
            foreach (var itm in items)
            {
                if (displayOrder == item.DisplayOrder)
                {
                    displayOrder++;
                }

                itm.DisplayOrder = displayOrder++;
                db.SaveChanges();
            }
            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult ChangeFAQCategoryDisplayOrder(int id, int upDown)
        {
            var item = db.CoreModule_FAQCategories.FirstOrDefault(p => p.Id == id);
            int displayOrder = 1;
            item.DisplayOrder = (upDown == 1) ? item.DisplayOrder + 1 : item.DisplayOrder - 1;
            db.SaveChanges();
            var items = db.CoreModule_FAQCategories.Where(p => p.Id != item.Id).OrderBy(p => p.DisplayOrder).ToList();
            foreach (var itm in items)
            {
                if (displayOrder == item.DisplayOrder)
                {
                    displayOrder++;
                }

                itm.DisplayOrder = displayOrder++;
                db.SaveChanges();
            }
            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult ChangeFAQDisplayOrder(int id, int upDown)
        {
            var item = db.CoreModule_FAQMasters.FirstOrDefault(p => p.Id == id);
            int displayOrder = 1;
            item.DisplayOrder = (upDown == 1) ? item.DisplayOrder + 1 : item.DisplayOrder - 1;
            db.SaveChanges();
            var items = db.CoreModule_FAQMasters.Where(p => p.Id != item.Id).OrderBy(p => p.DisplayOrder).ToList();
            foreach (var itm in items)
            {
                if (displayOrder == item.DisplayOrder)
                {
                    displayOrder++;
                }

                itm.DisplayOrder = displayOrder++;
                db.SaveChanges();
            }
            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult ChangeGalleryCategoryDisplayOrder(int id, int upDown)
        {
            var item = db.CoreModule_GalleryCategories.FirstOrDefault(p => p.Id == id);
            int displayOrder = 1;
            item.DisplayOrder = (upDown == 1) ? item.DisplayOrder + 1 : item.DisplayOrder - 1;
            db.SaveChanges();
            var items = db.CoreModule_GalleryCategories.Where(p => p.Id != item.Id).OrderBy(p => p.DisplayOrder).ToList();
            foreach (var itm in items)
            {
                if (displayOrder == item.DisplayOrder)
                {
                    displayOrder++;
                }

                itm.DisplayOrder = displayOrder++;
                db.SaveChanges();
            }
            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult ChangeGalleryAlbumDisplayOrder(int id, int upDown)
        {
            var item = db.CoreModule_GalleryAlbums.FirstOrDefault(p => p.Id == id);
            int displayOrder = 1;
            item.DisplayOrder = (upDown == 1) ? item.DisplayOrder + 1 : item.DisplayOrder - 1;
            db.SaveChanges();
            var items = db.CoreModule_GalleryAlbums.Where(p => p.Id != item.Id).OrderBy(p => p.DisplayOrder).ToList();
            foreach (var itm in items)
            {
                if (displayOrder == item.DisplayOrder)
                {
                    displayOrder++;
                }

                itm.DisplayOrder = displayOrder++;
                db.SaveChanges();
            }
            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult ChangeGalleryAlbumItemsDisplayOrder(int id, int upDown)
        {
            var item = db.CoreModule_GalleryAlbumItems.FirstOrDefault(p => p.Id == id);
            int displayOrder = 1;
            item.DisplayOrder = (upDown == 1) ? item.DisplayOrder + 1 : item.DisplayOrder - 1;
            db.SaveChanges();
            var items = db.CoreModule_GalleryAlbumItems.Where(p => p.Id != item.Id && p.AlbumId == item.AlbumId).OrderBy(p => p.DisplayOrder).ToList();
            foreach (var itm in items)
            {
                if (displayOrder == item.DisplayOrder)
                {
                    displayOrder++;
                }

                itm.DisplayOrder = displayOrder++;
                db.SaveChanges();
            }
            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult ChangeSliderCategoryDisplayOrder(int id, int upDown)
        {
            var item = db.CoreModule_Sliders.FirstOrDefault(p => p.Id == id);
            int displayOrder = 1;
            item.DisplayOrder = (upDown == 1) ? item.DisplayOrder + 1 : item.DisplayOrder - 1;
            db.SaveChanges();
            var items = db.CoreModule_Sliders.Where(p => p.Id != item.Id).OrderBy(p => p.DisplayOrder).ToList();
            foreach (var itm in items)
            {
                if (displayOrder == item.DisplayOrder)
                {
                    displayOrder++;
                }

                itm.DisplayOrder = displayOrder++;
                db.SaveChanges();
            }
            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult ChangeSliderItemsDisplayOrder(int id, int upDown)
        {
            var item = db.CoreModule_SliderItems.FirstOrDefault(p => p.Id == id);
            int displayOrder = 1;
            item.DisplayOrder = (upDown == 1) ? item.DisplayOrder + 1 : item.DisplayOrder - 1;
            db.SaveChanges();
            var items = db.CoreModule_SliderItems.Where(p => p.Id != item.Id).OrderBy(p => p.DisplayOrder).ToList();
            foreach (var itm in items)
            {
                if (displayOrder == item.DisplayOrder)
                {
                    displayOrder++;
                }

                itm.DisplayOrder = displayOrder++;
                db.SaveChanges();
            }
            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult ChangeNewsCategoryDisplayOrder(int id, int upDown)
        {
            var item = db.CoreModule_NewsCategories.FirstOrDefault(p => p.Id == id);
            int displayOrder = 1;
            item.DisplayOrder = (upDown == 1) ? item.DisplayOrder + 1 : item.DisplayOrder - 1;
            db.SaveChanges();
            var items = db.CoreModule_NewsCategories.Where(p => p.Id != item.Id).OrderBy(p => p.DisplayOrder).ToList();
            foreach (var itm in items)
            {
                if (displayOrder == item.DisplayOrder)
                {
                    displayOrder++;
                }

                itm.DisplayOrder = displayOrder++;
                db.SaveChanges();
            }
            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult ChangeNewsMasterDisplayOrder(int id, int upDown)
        {
            var item = db.CoreModule_News.FirstOrDefault(p => p.Id == id);
            int displayOrder = 1;
            item.DisplayOrder = (upDown == 1) ? item.DisplayOrder + 1 : item.DisplayOrder - 1;
            db.SaveChanges();
            var items = db.CoreModule_News.Where(p => p.Id != item.Id).OrderBy(p => p.DisplayOrder).ToList();
            foreach (var itm in items)
            {
                if (displayOrder == item.DisplayOrder)
                {
                    displayOrder++;
                }

                itm.DisplayOrder = displayOrder++;
                db.SaveChanges();
            }
            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }


        [Authorize]
        public JsonResult ChangeEventCategoriesDisplayOrder(int id, int upDown)
        {
            var item = db.CoreModule_EventCategories.FirstOrDefault(p => p.Id == id);
            int displayOrder = 1;
            item.DisplayOrder = (upDown == 1) ? item.DisplayOrder + 1 : item.DisplayOrder - 1;
            db.SaveChanges();
            var items = db.CoreModule_EventCategories.Where(p => p.Id != item.Id).OrderBy(p => p.DisplayOrder).ToList();
            foreach (var itm in items)
            {
                if (displayOrder == item.DisplayOrder)
                {
                    displayOrder++;
                }

                itm.DisplayOrder = displayOrder++;
                db.SaveChanges();
            }
            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult ChangeEventMasterDisplayOrder(int id, int upDown)
        {
            var item = db.CoreModule_Events.FirstOrDefault(p => p.Id == id);
            int displayOrder = 1;
            item.DisplayOrder = (upDown == 1) ? item.DisplayOrder + 1 : item.DisplayOrder - 1;
            db.SaveChanges();
            var items = db.CoreModule_Events.Where(p => p.Id != item.Id).OrderBy(p => p.DisplayOrder).ToList();
            foreach (var itm in items)
            {
                if (displayOrder == item.DisplayOrder)
                {
                    displayOrder++;
                }

                itm.DisplayOrder = displayOrder++;
                db.SaveChanges();
            }
            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult ChangeBlogCategoriesDisplayOrder(int id, int upDown)
        {
            var item = db.CoreModule_BlogCategories.FirstOrDefault(p => p.Id == id);
            int displayOrder = 1;
            item.DisplayOrder = (upDown == 1) ? item.DisplayOrder + 1 : item.DisplayOrder - 1;
            db.SaveChanges();
            var items = db.CoreModule_BlogCategories.Where(p => p.Id != item.Id).OrderBy(p => p.DisplayOrder).ToList();
            foreach (var itm in items)
            {
                if (displayOrder == item.DisplayOrder)
                {
                    displayOrder++;
                }

                itm.DisplayOrder = displayOrder++;
                db.SaveChanges();
            }
            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult ChangeBlogMasterDisplayOrder(int id, int upDown)
        {
            var item = db.CoreModule_Blogs.FirstOrDefault(p => p.Id == id);
            int displayOrder = 1;
            item.DisplayOrder = (upDown == 1) ? item.DisplayOrder + 1 : item.DisplayOrder - 1;
            db.SaveChanges();
            var items = db.CoreModule_Blogs.Where(p => p.Id != item.Id).OrderBy(p => p.DisplayOrder).ToList();
            foreach (var itm in items)
            {
                if (displayOrder == item.DisplayOrder)
                {
                    displayOrder++;
                }

                itm.DisplayOrder = displayOrder++;
                db.SaveChanges();
            }
            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult UploadFile(string dir)
        {
            var filepath = dir;
            if (Request.Files.Count > 0)
            {
                dir = Server.MapPath(dir);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                dir = dir + "\\" + DateTime.Now.ToString("yyyyMM");
                filepath = filepath + "/" + DateTime.Now.ToString("yyyyMM");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                var path = filepath + "/" + Path.GetFileName(Request.Files[0].FileName);
                Request.Files[0].SaveAs(Server.MapPath(path));
                return Json(new { status = "success", path = path }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = "fail", message = "File Not Selected" }, JsonRequestBehavior.AllowGet);
        }
    }
}