using System;
using System.Collections.Generic;

namespace RAMHX.CMS.DataAccessCore
{
    public partial class CoreModuleGalleryAlbumItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? AlbumId { get; set; }
        public string ItemPath { get; set; }
        public bool? IsActive { get; set; }
        public int AccessType { get; set; }
        public int DisplayOrder { get; set; }
        public int UploadType { get; set; }
    }
}
