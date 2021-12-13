using ECommerce.Web.Settings;

namespace ECommerce.Web.Helpers
{
    public class PhotoHelper
    {
        private readonly ServiceApiSettings _serviceApiSettings;

        public PhotoHelper(ServiceApiSettings serviceApiSettings)
        {
            _serviceApiSettings = serviceApiSettings;
        }

        public string GetPhotoStockUrl(string photoUrl)
        {
            return $"{_serviceApiSettings.PhotoStockUri}/photos/{photoUrl}";
        }
    }
}
