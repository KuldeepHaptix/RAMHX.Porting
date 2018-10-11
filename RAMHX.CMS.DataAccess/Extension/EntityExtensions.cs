using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RAMHX.CMS.DataAccess.Extension
{
    public static class EntityExtensions
    {
        public static List<cms_PageFieldValues> FieldValues(this cms_Pages page)
        {
            foreach (var fv in page.cms_PageFieldValues)
            {
                fv.FieldName = fv.cms_TemplateFields.FieldName;
            }            
            return page.cms_PageFieldValues.ToList();
        }

        public static string FieldValue(this cms_Pages page, string FieldName)
        {
            var fls = FieldValues(page);
            if (fls != null && fls.Count > 0)
            {
                foreach (var fv in fls)
                {
                    if(fv.FieldName.ToLower() == FieldName.ToLower())
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
