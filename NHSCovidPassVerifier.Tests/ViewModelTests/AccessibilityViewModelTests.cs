using NHSCovidPassVerifier.Configuration;
using NHSCovidPassVerifier.Services.Interfaces;
using NHSCovidPassVerifier.Tests.MockServices;
using NHSCovidPassVerifier.ViewModels;
using I18NPortable;
using NUnit.Framework;

namespace NHSCovidPassVerifier.Tests.ViewModelTests
{
    [TestFixture]
    public class AccessibilityViewModelTests
    {
        private readonly AccessiblityViewModel _viewModel;

        public AccessibilityViewModelTests()
        {
            IoCContainer.RegisterSingleton<ISettingsService, MockSettingsService>();
            _viewModel = new AccessiblityViewModel();
        }

        [Test]
        public void TextIsCorrect()
        {
            Assert.AreEqual(_viewModel.AccessibilityTitle, "ACCESSIBILITY_TITLE".Translate());
            Assert.AreEqual(_viewModel.AccessibilityParagraph1, "ACCESSIBILITY_PARAGRAPH_1".Translate());

            Assert.AreEqual(_viewModel.AccessibilityTitle2, "ACCESSIBILITY_TITLE_2".Translate());
            Assert.AreEqual(_viewModel.AccessibilityParagraph2, "ACCESSIBILITY_PARAGRAPH_2".Translate());

            Assert.AreEqual(_viewModel.AccessibilityParagraph2Bullet1, "ACCESSIBILITY_PARAGRAPH_2_BULLET_1".Translate());
            Assert.AreEqual(_viewModel.AccessibilityParagraph2Bullet2, "ACCESSIBILITY_PARAGRAPH_2_BULLET_2".Translate());

            Assert.AreEqual(_viewModel.AccessibilityServiceTitle, "ACCESSIBILITY_ACCESSING_THE_SERVICE_TITLE".Translate());
            Assert.AreEqual(_viewModel.AccessibilityServiceParagraph, "ACCESSIBILITY_ACCESSING_THE_SERVICE_PARAGRAPH".Translate());

            Assert.AreEqual(_viewModel.AccessibilityServiceBullet1, "ACCESSIBILITY_ACCESSING_THE_SERVICE_BULLET_1".Translate());
            Assert.AreEqual(_viewModel.AccessibilityServiceBullet2, "ACCESSIBILITY_ACCESSING_THE_SERVICE_BULLET_2".Translate());

            Assert.AreEqual(_viewModel.AccessibilityServiceBullet3, "ACCESSIBILITY_ACCESSING_THE_SERVICE_BULLET_3".Translate());
            Assert.AreEqual(_viewModel.AccessibilityComplianceTitle, "ACCESSIBILITY_COMPLIANCE_STATUS_TITLE".Translate());

            Assert.AreEqual(_viewModel.AccessibilityComplianceParagraph, "ACCESSIBILITY_COMPLIANCE_STATUS_PARAGRAPH".Translate());
            Assert.AreEqual(_viewModel.AccessibilityReortingTitle, "ACCESSIBILITY_REPORTING_TITLE".Translate());

            Assert.AreEqual(_viewModel.AccessibilityReortingParagraph, "ACCESSIBILITY_REPORTING_PARAGRAPH".Translate());
            Assert.AreEqual(_viewModel.AccessibilityEnforcementTitle, "ACCESSIBILITY_ENFORCEMENT_TITLE".Translate());

            Assert.AreEqual(_viewModel.AccessibilityEnforcementParagraph1, "ACCESSIBILITY_ENFORCEMENT_PARAGRAPH_1".Translate());
            Assert.AreEqual(_viewModel.AccessibilityEnforcementParagraph1Link, "ACCESSIBILITY_ENFORCEMENT_PARAGRAPH_1_LINK".Translate());

            Assert.AreEqual(_viewModel.AccessibilityEnforcementParagraph2, "ACCESSIBILITY_ENFORCEMENT_PARAGRAPH_2".Translate());
            Assert.AreEqual(_viewModel.AccessibilityTestedTitle, "ACCESSIBILITY_HOWWETESTED_TITLE".Translate());

            Assert.AreEqual(_viewModel.AccessibilityTestedParagraph, "ACCESSIBILITY_HOWWETESTED_PARAGRAPH".Translate());
            Assert.AreEqual(_viewModel.AccessibilityImproveTitle, "ACCESSIBILITY_IMPROVE_TITLE".Translate());
            Assert.AreEqual(_viewModel.AccessibilityImproveParagraph, "ACCESSIBILITY_IMPROVE_PARAGRAPH".Translate());
        }
    }
}

