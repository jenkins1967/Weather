using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Html.Containers
{
    public class DefaultButtonContainer:ContainerElement
    {
        public DefaultButtonContainer(HtmlHelper htmlHelper) : this(htmlHelper, null)
        {
        }

        public DefaultButtonContainer(HtmlHelper htmlHelper, ContainerElement parentContainer) : base("button", "btn btn-default", htmlHelper, parentContainer)
        {
            
        }
    }
}