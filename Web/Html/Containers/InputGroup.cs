using System.Web.Mvc;

namespace Web.Html.Containers
{
    public class InputGroup : DivContainerElement
    {
        public InputGroup(HtmlHelper htmlHelper) : this(htmlHelper, null)
        {
        }

        public InputGroup(HtmlHelper htmlHelper, ContainerElement parentContainer) : base("input-group", htmlHelper, parentContainer)
        {
        }
    }
}