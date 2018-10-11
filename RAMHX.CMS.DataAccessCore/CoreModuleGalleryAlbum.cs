using System;
using System.Collections.Generic;

namespace RAMHX.CMS.DataAccessCore
{
    public partial class CoreModuleGalleryAlbum
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public string ThumbnailPath { get; set; }
        public bool? IsActive { get; set; }
        public int AlbumType { get; set; }
        public int DisplayOrder { get; set; }
    }
}
