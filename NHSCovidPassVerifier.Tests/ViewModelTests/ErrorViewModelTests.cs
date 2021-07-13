using System;
using System.Threading.Tasks;
using NHSCovidPassVerifier.Configuration;
using NHSCovidPassVerifier.Models.Commands;
using NHSCovidPassVerifier.Services.Interfaces;
using NHSCovidPassVerifier.Tests.MockServices;
using NHSCovidPassVerifier.ViewModels;
using I18NPortable;
using NUnit.Framework;

namespace NHSCovidPassVerifier.Tests.ViewModelTests
{
    public class ErrorViewModelTests
    {
        private ErrorViewModel _viewModel;
        private const string ExpectedTitle = "Test title";
        private const string ExpectedDescription = "Test description";
        private const string ExpectedButtonText = "Test description";
        private readonly Func<Task> _buttonFunction;
        private readonly AsyncCommand _expectedButtonCommand;

        public ErrorViewModelTests()
        {
            IoCContainer.RegisterSingleton<ISettingsService, MockSettingsService>();

            _buttonFunction = async () => {};
            _expectedButtonCommand = new AsyncCommand(_buttonFunction);
            _viewModel = new ErrorViewModel();
        }

        [SetUp]
        public void SetUp()
        {
            _viewModel = new ErrorViewModel();
        }
        
        [Test]
        public void TestButtonTextIsInitialised()
        {
            Assert.AreEqual(_viewModel.ButtonText, "GO_TO_SETTINGS".Translate());
        }
        
        [Test]
        public void ButtonCommandIsInitialised()
        {
            Assert.NotNull(_viewModel.ButtonCommand);
        }

        [Test]
        public void TestAllParametersPassed()
        {
            //given
            var parameters = new object[] {ExpectedTitle, ExpectedDescription, ExpectedButtonText, _buttonFunction};
            //when
            _viewModel.InitializeAsync(parameters);
            //then
            Assert.AreEqual(ExpectedTitle, _viewModel.Title);
            Assert.AreEqual(ExpectedDescription, _viewModel.Description);
            Assert.AreEqual(ExpectedButtonText, _viewModel.ButtonText);
            Assert.AreEqual(_expectedButtonCommand, _viewModel.ButtonCommand);
        }

        [Test]
        public void TestNoParametersPassed()
        {
            //given
            var parameters = new object[] {};
            //when
            _viewModel.InitializeAsync(parameters);
            //then
            Assert.AreEqual("GENERIC_ERROR_TITLE".Translate(), _viewModel.Title);
            Assert.AreEqual("GENERIC_ERROR_DESCRIPTION".Translate(), _viewModel.Description);
            Assert.AreEqual("GO_TO_SETTINGS".Translate(), _viewModel.ButtonText);
            Assert.AreEqual(_viewModel.OpenSettingsCommand, _viewModel.ButtonCommand);
        }

        [Test]
        public void TestNullParametersPassed()
        {
            //given
            string[] parameters = null;
            //when
            _viewModel.InitializeAsync(parameters);
            //then
            Assert.AreEqual("GENERIC_ERROR_TITLE".Translate(), _viewModel.Title);
            Assert.AreEqual("GENERIC_ERROR_DESCRIPTION".Translate(), _viewModel.Description);
            Assert.AreEqual("GO_TO_SETTINGS".Translate(), _viewModel.ButtonText);
            Assert.AreEqual(_viewModel.OpenSettingsCommand, _viewModel.ButtonCommand);
        }

        [Test]
        public void TestTitleTextParameterPassed()
        {
            //given
            var parameters = new object[] {ExpectedTitle, ""};
            //when
            _viewModel.InitializeAsync(parameters);
            //then
            Assert.AreEqual(ExpectedTitle, _viewModel.Title);
            Assert.AreEqual("GENERIC_ERROR_DESCRIPTION".Translate(), _viewModel.Description);
            Assert.AreEqual("GO_TO_SETTINGS".Translate(), _viewModel.ButtonText);
            Assert.AreEqual(_viewModel.OpenSettingsCommand, _viewModel.ButtonCommand);
        }

        [Test]
        public void TestDescriptionTextParameterPassed()
        {
            //given
            var parameters = new object[] {"", ExpectedDescription};
            //when
            _viewModel.InitializeAsync(parameters);
            //then
            Assert.AreEqual("GENERIC_ERROR_TITLE".Translate(), _viewModel.Title);
            Assert.AreEqual(ExpectedDescription, _viewModel.Description);
            Assert.AreEqual("GO_TO_SETTINGS".Translate(), _viewModel.ButtonText);
            Assert.AreEqual(_viewModel.OpenSettingsCommand, _viewModel.ButtonCommand);
        }

        [Test]
        public void TestButtonTextParameterPassed()
        {
            //given
            var parameters = new object[] {"", "", ExpectedButtonText};
            //when
            _viewModel.InitializeAsync(parameters);
            //then
            Assert.AreEqual("GENERIC_ERROR_TITLE".Translate(), _viewModel.Title);
            Assert.AreEqual("GENERIC_ERROR_DESCRIPTION".Translate(), _viewModel.Description);
            Assert.AreEqual(ExpectedButtonText, _viewModel.ButtonText);
            Assert.AreEqual(_viewModel.OpenSettingsCommand, _viewModel.ButtonCommand);
        }

        [Test]
        public void TestButtonCommandParameterPassed()
        {
            //given
            var parameters = new object[] {"", "", "", _buttonFunction};
            //when
            _viewModel.InitializeAsync(parameters);
            //then
            Assert.AreEqual("GENERIC_ERROR_TITLE".Translate(), _viewModel.Title);
            Assert.AreEqual("GENERIC_ERROR_DESCRIPTION".Translate(), _viewModel.Description);
            Assert.AreEqual("GO_TO_SETTINGS".Translate(), _viewModel.ButtonText);
            Assert.AreEqual(_expectedButtonCommand, _viewModel.ButtonCommand);
        }
    }
}
