using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace RAMHX.CMS.DataAccessCore.Extension
{
    public static class  EntityExtensions
    {
        public static List<CmsPageFieldValues> FieldValues(this CmsPages page)
        {
            foreach (var fv in page.CmsPageFieldValues)
            {
                fv.FieldName = fv.TemplateField.FieldName;
            }
            return page.CmsPageFieldValues.ToList();
        }

        public static string FieldValue(this CmsPages page, string FieldName)
        {
            var fls = FieldValues(page);
            if (fls != null && fls.Count > 0)
            {
                foreach (var fv in fls)
                {
                    if (fv.FieldName.ToLower() == FieldName.ToLower())
                    {
                        return fv.FieldValue;
                    }
                }
            }

            return string.Empty;
        }

        public static string ToXml(this DataSet ds)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (TextWriter streamWriter = new StreamWriter(memoryStream))
                {
                    var xmlSerializer = new XmlSerializer(typeof(DataSet));
                    xmlSerializer.Serialize(streamWriter, ds);
                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }
            }
        }
    }
}
