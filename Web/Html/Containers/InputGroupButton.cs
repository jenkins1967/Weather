using System.Web.Mvc;

namespace Web.Html.Containers
{
    public class InputGroupButton : SpanContainerElement
    {
        public InputGroupButton(HtmlHelper htmlHelper)
            : this(htmlHelper, null)
        {
        }

        public InputGroupButton(HtmlHelper htmlHelper, ContainerElement parentContainer)
            : base("input-group-btn", htmlHelper, parentContainer)
        {
        }
    }
}