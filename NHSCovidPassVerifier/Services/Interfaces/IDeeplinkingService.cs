using System.Threading.Tasks;

namespace NHSCovidPassVerifier.Services.Interfaces
{
    public interface IDeeplinkingService
    {
        /// <summary>
        /// Task which opens native application settings
        /// </summary>
        /// <returns>Successfully completed Task</returns>
        Task GoToAppSettings();
    }
}