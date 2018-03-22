using System.Web.Mvc;

namespace Web
{
    public class ImageBindAttribute : CustomModelBinderAttribute
    {
        private IModelBinder _binder;
        public ImageBindAttribute(string fieldName)
        {
            _binder = new ImageModelBinder(fieldName);
        }
        public override IModelBinder GetBinder() { return _binder; }
    }

}