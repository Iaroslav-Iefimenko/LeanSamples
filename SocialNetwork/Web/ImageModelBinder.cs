using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Web
{
    public class ImageModelBinder : DefaultModelBinder
    {
        private string _fieldName;
        public ImageModelBinder(string fieldName)
        {
            _fieldName = fieldName;
        }

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var obj = base.BindModel(controllerContext, bindingContext);
            ValueProviderResult valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName + "." + _fieldName) ??
                                              bindingContext.ValueProvider.GetValue(_fieldName);

            if (valueResult != null)
            {
                HttpPostedFileBase file = (HttpPostedFileBase)valueResult.ConvertTo(typeof(HttpPostedFileBase));
                if (file != null)
                {
                    byte[] tempImage = new byte[file.ContentLength];
                    file.InputStream.Read(tempImage, 0, file.ContentLength);
                    PropertyInfo imagePoperty = bindingContext.ModelType.GetProperty(_fieldName);
                    imagePoperty.SetValue(obj, tempImage, null);
                }
            }
            return obj;
        }
    }

}