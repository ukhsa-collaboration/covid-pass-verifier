using System.Threading.Tasks;
using Xamarin.Essentials;

namespace NHSCovidPassVerifier.Utils
{
    public static class PermissionUtils
    {
        public static async Task<bool> CheckAndRequestCameraPermission()
        {
            return await Permissions.CheckStatusAsync<Permissions.Camera>() == PermissionStatus.Granted 
                   || await Permissions.RequestAsync<Permissions.Camera>() == PermissionStatus.Granted;
        }
    }
}
