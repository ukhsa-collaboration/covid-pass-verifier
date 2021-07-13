using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NHSCovidPassVerifier.Services.Interfaces
{
    public interface INavigationService
    {
        /// <summary>
        /// Clear navigation stack and push landing page
        /// </summary>
        void OpenLandingPage();
        /// <summary>
        /// Push desired page onto navigation stack
        /// </summary>
        /// <param name="page">The content page you wish to push onto the stack</param>
        /// <param name="animated">A boolean used to specify if page push should be animated (default is true)</param>
        /// <param name="data">Pass object data to pushed page which can be accessed using initialise async command</param>
        /// <returns>Completed Task</returns>
        Task PushPage(Page page, bool animated = true, object data = null);
        /// <summary>
        /// Push desired modal page onto navigation stack
        /// </summary>
        /// <param name="page">The modal content page you wish to push onto the stack</param>
        /// <param name="animated">A boolean used to specify if page push should be animated (default is true)</param>
        /// <param name="data">Pass object data to pushed page which can be accessed using initialise async command</param>
        /// <returns>Completed Task</returns>
        Task PushModal(Page page, bool animated = true, object data = null);
        /// <summary>
        /// Pop top page off of navigation stack
        /// </summary>
        /// <param name="animated">A boolean used to specify if page pop should be animated (default is true)</param>
        /// <returns>Completed Task</returns>
        Task PopPage(bool animated = true);
        /// <summary>
        /// Pop top page off of navigation stack
        /// </summary>
        /// <returns>Completed Task</returns>
        Task PopPage();
        /// <summary>
        /// Pop top page off of navigation stack passes object to returning page
        /// </summary>
        /// <param name="animated">A boolean used to specify if page pop should be animated (default is true)</param>
        /// <param name="data">Pass object data to returning page which can be accessed using on return command</param>
        /// <returns></returns>
        Task PopPageWithResult(bool animated = true, object data = null);
        /// <summary>
        /// If the top page is non-modal replace the it with a non-modal page. Otherwise, do nothing.
        /// </summary>
        /// <param name="pageToPush">Specify page to be pushed onto navigation stack</param>
        /// <param name="animated">A boolean used to specify if page pop should be animated (default is true)</param>
        /// <param name="data">Pass object data to pushed page which can be accessed using on initialise async command</param>
        /// <returns></returns>
        Task ReplaceTopPage(Page pageToPush, bool animated = false, object data = null);
        /// <summary>
        /// Pushes dynamic error page onto navigation stack
        /// </summary>
        /// <param name="title">Title string for error page</param>
        /// <param name="description">Description string for error page</param>
        /// <param name="buttonText">String to indicate button text</param>
        /// <param name="buttonCallback">Button function</param>
        /// <returns></returns>
        Task GoToErrorPage(string title = null, string description = null, string buttonText = null, Func<Task> buttonCallback = null);

        /// <summary>
        /// Find what page is at the top of the stack
        /// </summary>
        /// <returns>The page at the top of the stack</returns>
        Page FindCurrentPage();
    }
}