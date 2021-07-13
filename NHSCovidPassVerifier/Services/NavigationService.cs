using NHSCovidPassVerifier.Services.Interfaces;
using NHSCovidPassVerifier.ViewModels.Base;
using NHSCovidPassVerifier.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NHSCovidPassVerifier.Services
{
    public class NavigationService : INavigationService
    {
        public static Stack<Page> ModalPages = new Stack<Page>();

        public void OpenLandingPage()
        {
            ModalPages = new Stack<Page>();
            Application.Current.MainPage = new NavigationPage(new LandingPage());
        }

        public async Task PushPage(Page page, bool animated = false, object data = null)
        {
            await page.Initialize(data);
            await FindCurrentPage().Navigation.PushAsync(page, animated);
        }

        public async Task PopPage(bool animated)
        {
            var pageToPop = FindCurrentPage();
            if (pageToPop.IsModal())
            {
                await pageToPop.Navigation.PopModalAsync(animated);
                ModalPages.Pop();
            }
            else
            {
                await pageToPop.Navigation.PopAsync(animated);
            }
        }

        public async Task PopPage()
        {
            await PopPage(true);
        }

        public async Task PopPageWithResult(bool animated = true, object data = null)
        {
            await PopPage(animated);
            await FindCurrentPage().ExecuteOnReturn(data);
        }

        public async Task ReplaceTopPage(Page pageToPush, bool animated = false, object data = null)
        {
            var currentPage = FindCurrentPage();
            if (currentPage.IsModal()) { return; }

            await PushPage(pageToPush, animated, data);
            
            if (currentPage.IsModal())
            {
                currentPage.Navigation.RemovePage(currentPage);
                ModalPages.Pop();
            }
            else
            {
                currentPage.Navigation.RemovePage(currentPage);
            }
        }
        
        public async Task PushModal(Page page, bool animated = true, object data = null)
        {
            var currentPage = FindCurrentPage();

            await page.Initialize(data);
            await currentPage.Navigation.PushModalAsync(new NavigationPage(page), animated);
            ModalPages.Push(page);
        }

        public async Task GoToErrorPage(string title = null, string description = null, string buttonText = null, Func<Task> buttonCallback = null)
        {
            var param = new object[] { title, description, buttonText, buttonCallback };

            var lastModal = FindCurrentPage().Navigation.ModalStack.LastOrDefault();
            if (lastModal == null || lastModal.GetType() != typeof(ErrorPage))
            {
                await PushModal(new ErrorPage(), true, param);
            }
        }

        public Page FindCurrentPage()
        {
            if (ModalPages.Any())
            {
                var modalRoot = ModalPages.Peek();
                if (modalRoot.Navigation.NavigationStack.Count > 1)
                {
                    return modalRoot.Navigation.NavigationStack.Last();
                }

                return modalRoot is NavigationPage modalNavPage ? modalNavPage.CurrentPage : modalRoot;
            }

            var rootPage = Application.Current.MainPage;
            return rootPage is NavigationPage navPage ? navPage.CurrentPage : rootPage;
        }
    }

    static class PageNavigationExtensions
    {
        public static bool IsModal(this Page page)
        {
            return NavigationService.ModalPages.Any() && NavigationService.ModalPages.Peek() == page;
        }

        public static async Task Initialize(this Page page, object data)
        {
            if (data != null)
            {
                await ((BaseViewModel)page.BindingContext).InitializeAsync(data);
            }
        }

        public static async Task ExecuteOnReturn(this Page page, object data)
        {
            if (data != null)
            {
                await ((BaseViewModel)page.BindingContext).ExecuteOnReturn(data);
            }
        }
    }
}