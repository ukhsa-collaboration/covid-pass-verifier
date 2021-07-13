using System.Threading.Tasks;

namespace NHSCovidPassVerifier.Services.Interfaces
{
    public interface IInternationalEnabledService
    {
        /// <summary>
        /// Get value of internationEnabled flag
        /// </summary>
        /// <returns>Boolean dependent on whether international QR codes are enabled or not</returns>
        public bool GetInternationalEnabled();

        /// <summary>
        /// Set the internationEnabled flag
        /// </summary>
        /// <param name="newValue">The boolean value you wish to set the flag to</param>
        /// <returns>Returns completed task</returns>
        public Task SetInternationalEnabled(bool newValue);
    }
}