using System.Web.Mvc;

namespace Web.Html.Containers
{
    public  class DivContainerElement : ContainerElement
    {
        public DivContainerElement(string classes, HtmlHelper htmlHelper) : this(classes, htmlHelper, null)
        {
        }

        public DivContainerElement(string classes, HtmlHelper htmlHelper, ContainerElement parentContainer) : base("div", classes, htmlHelper, parentContainer)
        {
        }
    }
}