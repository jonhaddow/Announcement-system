using System;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace Coursework.Helpers
{
    public static class HelperExtensions
    {
        // I am using this static method to create an ajax action link which allows inner html elements within the linkText.
        public static MvcHtmlString RawActionLink(this AjaxHelper ajaxHelper, string linkText, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            var repID = Guid.NewGuid().ToString();
            var lnk = ajaxHelper.ActionLink(repID, actionName, controllerName, routeValues, ajaxOptions, htmlAttributes);
            return MvcHtmlString.Create(lnk.ToString().Replace(repID, linkText));
        }
    }
}

