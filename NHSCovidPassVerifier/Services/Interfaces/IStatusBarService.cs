using Xamarin.Forms;

namespace NHSCovidPassVerifier.Services.Interfaces
{
    public interface IStatusBarService
    {
        /// <summary>
        /// Sets the phones status bare color.
        /// </summary>
        /// <param name="color">Color to change the status bar to.</param>
        void SetStatusBarColor(Color color);

        /// <summary>
        /// Sets the status bar to the default app color.
        /// </summary>
        void SetDefaultStatusBar();

        /// <summary>
        /// Removes and updated the statusbar on iOS, does nothing on Android.
        /// </summary>
        void RemoveStatusBar();
    }
}
