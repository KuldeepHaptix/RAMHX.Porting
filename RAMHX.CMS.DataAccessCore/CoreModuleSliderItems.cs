using System;
using System.Collections.Generic;

namespace RAMHX.CMS.DataAccessCore
{
    public partial class CoreModuleSliderItems
    {
        public int Id { get; set; }
        public int? SliderId { get; set; }
        public bool? Active { get; set; }
        public string FirstLine { get; set; }
        public string SecondLine { get; set; }
        public string ThirdLine { get; set; }
        public string ForthLine { get; set; }
        public string FirstButtonText { get; set; }
        public string SecondButtonText { get; set; }
        public string FirstButtonLink { get; set; }
        public string SecondButtonLink { get; set; }
        public string SliderImagePath { get; set; }
        public int DisplayOrder { get; set; }
    }
}
