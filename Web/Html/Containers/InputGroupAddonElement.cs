using System.Web.Mvc;

namespace Web.Html.Containers
{
    public class InputGroupAddonElement : SpanContainerElement
    {
        public InputGroupAddonElement(HtmlHelper htmlHelper) : this(htmlHelper, null)
        {
        }

        public InputGroupAddonElement(HtmlHelper htmlHelper, ContainerElement parentContainer) : base("input-group-addon", htmlHelper, parentContainer)
        {
        }
    }
}