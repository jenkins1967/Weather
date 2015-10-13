using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Web.Extensions
{
    public static class ModelMetadataExtensions
    {
        public static void LoadAttributes(this ModelMetadata metadata, IDictionary<string, object> htmlAttributes)
        {
            if(!string.IsNullOrEmpty(metadata.Watermark))
            {
                htmlAttributes.Add("placeholder", metadata.Watermark);
            }
        }
    }
}