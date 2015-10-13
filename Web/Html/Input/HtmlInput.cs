using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.UI;
using System.Xml.Schema;
using Web.Extensions;

namespace Web.Html.Input
{
    public static class HtmlInput
    {
        public static IHtmlString TextInputFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression,
            IDictionary<string, object> htmlAttributes)
        {

            var name = html.NameFor(expression).ToString();
            var value = ModelMetadata.FromLambdaExpression(expression, html.ViewData).Model;
            return html.TextInput(name, value, htmlAttributes);

        }

        public static IHtmlString TextInputFor<TModel, TProperty>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, TProperty>> expression)
        {
            return html.TextInputFor(expression, new Dictionary<string, object>());
        }

        public static IHtmlString TextInput<TModel>(this HtmlHelper<TModel> html, string name,
            object value,
            IDictionary<string, object> htmlAttributes)
        {

            var attribs = htmlAttributes ?? new Dictionary<string, object>();
            HtmlUtility.AddToClasses("form-control", attribs);            
            html.ViewData.ModelMetadata.LoadAttributes(htmlAttributes);
            return html.TextBox(name, value, attribs);
        }

        public static IHtmlString TextInput<TModel>(this HtmlHelper<TModel> html, string name,
            object value)
        {

            return html.TextInput(name, value, null);
        }
    }
}