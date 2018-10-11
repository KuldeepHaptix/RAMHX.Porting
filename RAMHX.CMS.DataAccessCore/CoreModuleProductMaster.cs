using System;
using System.Collections.Generic;

namespace RAMHX.CMS.DataAccessCore
{
    public partial class CoreModuleProductMaster
    {
        public int Id { get; set; }
        public string ProdName { get; set; }
        public string ShortDesc { get; set; }
        public string LongDesc { get; set; }
        public string Usage { get; set; }
        public decimal? Mrp { get; set; }
        public decimal? OfferPrice { get; set; }
        public string ImageUrl { get; set; }
        public int? CategoryId { get; set; }
        public int DisplayOrder { get; set; }
    }
}
