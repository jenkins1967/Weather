using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;

namespace Web.Attributes
{
    public class PlaceholderAttribute:Attribute, IMetadataAware
    {
        private readonly string _text;

        public PlaceholderAttribute(string text)
        {
            _text = text;
        }

        public void OnMetadataCreated(ModelMetadata metadata)
        {
            metadata.Watermark = _text;
        }
    }
}