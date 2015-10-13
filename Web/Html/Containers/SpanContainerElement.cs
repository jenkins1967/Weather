using System.Web.Mvc;

namespace Web.Html.Containers
{
    public  class SpanContainerElement : ContainerElement
    {
        public SpanContainerElement(string classes, HtmlHelper htmlHelper) : this(classes, htmlHelper, null)
        {
        }

        public SpanContainerElement(string classes, HtmlHelper htmlHelper, ContainerElement parentContainer) : base("span", classes, htmlHelper, parentContainer)
        {
        }
    }
}