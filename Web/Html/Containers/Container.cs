using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;

namespace Web.Html.Containers
{
    public abstract class ContainerElement : IDisposable
    {
        private bool _sectionStarted;
        private bool _sectionClosed;
        private readonly HtmlHelper _htmlHelper;
        private readonly string _containerElement;
        private readonly string _classes;
        private readonly ContainerElement _parentContainer;

        protected ContainerElement(string containerElement, string classes, HtmlHelper htmlHelper)
            : this(containerElement, classes, htmlHelper, null)
        {

        }

        protected ContainerElement(string containerElement, string classes, HtmlHelper htmlHelper, ContainerElement parentContainer)
        {
            _parentContainer = parentContainer;
            _containerElement = containerElement;
            _classes = classes;
            _htmlHelper = htmlHelper;
            OtherAttributes = new Dictionary<string, object>();
        }

        public IDictionary<string, object> OtherAttributes { get; set; }

        public void Begin()
        {
            Begin(string.Empty);
        }

        public void Begin(string id)
        {
            using (var writer = new HtmlTextWriter(_htmlHelper.ViewContext.Writer))
            {
                writer.AddAttribute("class", _classes);
                if (!string.IsNullOrEmpty(id))
                    writer.AddAttribute("id", id);

                if (OtherAttributes.Any())
                {
                    OtherAttributes.ToList().ForEach(d => writer.AddAttribute(d.Key, d.Value.ToString()));
                }

                writer.RenderBeginTag(_containerElement);

                writer.Flush();
            }
            _sectionStarted = true;
        }

        public void Dispose()
        {
            if (_sectionStarted && !_sectionClosed)
            {
                End();
                if (_parentContainer != null)
                {
                    _parentContainer.Dispose();
                }
            }

            GC.SuppressFinalize(this);
        }


        private void End()
        {
            using (var writer = new HtmlTextWriter(_htmlHelper.ViewContext.Writer))
            {
                writer.WriteEndTag(_containerElement);
                writer.Flush();
            }

            _sectionClosed = true;
        }

    }
}