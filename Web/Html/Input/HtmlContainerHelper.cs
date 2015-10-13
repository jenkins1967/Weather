using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Web.Html.Containers;

namespace Web.Html.Input
{
    public static class HtmlContainerHelper
    {
        public static ContainerElement BeginInputGroup(this HtmlHelper html)
        {
            return BeginContainer(() => new InputGroup(html));
        }

        public static ContainerElement BeginInputGroupAddOn(this HtmlHelper html)
        {
            return BeginContainer(() => new InputGroupAddonElement(html));
        }

        public static ContainerElement BeginInputGroupButton(this HtmlHelper html)
        {
            return BeginContainer(() => new InputGroupButton(html));
        }

        public static ContainerElement BeginSpan(this HtmlHelper html, string classes)
        {
            return BeginContainer(() => new SpanContainerElement(classes, html));
        }

        public static ContainerElement BeginDefaultButton(this HtmlHelper html)
        {
            return html.BeginDefaultButton(null);
        }

        public static ContainerElement BeginDefaultButton(this HtmlHelper html, IDictionary<string, object> htmlattributes)
        {
            return BeginContainer(() =>
            {
                var attribs = htmlattributes ?? new Dictionary<string, object>();
                attribs.Add("type", "button");
                var container = new DefaultButtonContainer(html) {OtherAttributes = attribs};
                return container;
            });
        }

        public static ContainerElement BeginRowContainer(this HtmlHelper html)
        {
            return BeginContainer(() => new DivContainerElement("row", html));
        }

        public static ContainerElement BeginColumnContainer(this HtmlHelper html, Int16 width)
        {
            if (width > 12 || width < 1)
            {
                throw new ArgumentOutOfRangeException("width");
            }
            var className = "col-md-" + width;
            var container = new DivContainerElement(className, html);
            container.Begin();
            return container;
        }

        private static ContainerElement BeginContainer(Func<ContainerElement> create)
        {
            var container = create();
            container.Begin();
            return container;
        }
    }
}