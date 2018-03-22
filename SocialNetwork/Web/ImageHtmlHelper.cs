using System.Web.Mvc;
using System.Web.Routing;

namespace Web
{
    public static class ImageHtmlHelper
    {
        public static MvcHtmlString ImageUpload(this HtmlHelper helper, string name)
        {
            return ImageUpload(helper, name, null);
        }

        public static MvcHtmlString ImageUpload(this HtmlHelper helper, string name, object htmlAttributes)
        {
            var tagBuilder = new TagBuilder("input");
            tagBuilder.GenerateId(name);

            tagBuilder.Attributes["name"] = name;
            tagBuilder.Attributes["type"] = "file";
            tagBuilder.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            return MvcHtmlString.Create(tagBuilder.ToString());
        }

        public static MvcHtmlString Image(this HtmlHelper helper, string name, string id)
        {
            return Image(helper, name, id, null);
        }

        public static MvcHtmlString Image(this HtmlHelper helper, string name, string id, object htmlAttributes)
        {
            var tagBuilder = new TagBuilder("img");
            UrlHelper urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

            tagBuilder.Attributes["src"] = urlHelper.Action(name, null, new { id = id });
            tagBuilder.Attributes["alt"] = string.Format("{0} of {1}", name, id);
            tagBuilder.Attributes["width"] = "200";
            tagBuilder.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            return MvcHtmlString.Create(tagBuilder.ToString());
        }


    }
}