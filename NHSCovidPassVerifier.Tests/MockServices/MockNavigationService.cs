using System;
using System.Threading.Tasks;
using NHSCovidPassVerifier.Services.Interfaces;
using Xamarin.Forms;

namespace NHSCovidPassVerifier.Tests.MockServices
{
    public class MockNavigationService : INavigationService
    {
        public Task PopPageWithResult(bool animated = true, object data = null)
        {
            return Task.CompletedTask;
        }

        public Task ReplaceTopPage(Page pageToPush, bool animated = true, object data = null)
        {
            return Task.CompletedTask;
        }

        public Task GoToErrorPage(string title = null, string description = null, string buttonText = null, Func<Task> buttonCallback = null)
        {
            return Task.CompletedTask;
        }

        public void OpenLandingPage()
        {
        }

        public Task PopPage(bool animated)
        {
            return Task.CompletedTask;
        }

        public Task PopPage()
        {
            return Task.CompletedTask;
        }

        public Task PushModal(Page page, bool animated = true, object data = null)
        {
            return Task.CompletedTask;
        }

        public Task PushPage(Page page, bool animated = true, object data = null)
        {
            return Task.CompletedTask;
        }

        public Page FindCurrentPage()
        {
            throw new NotImplementedException();
        }
    }
}