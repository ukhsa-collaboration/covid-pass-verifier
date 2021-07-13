using NHSCovidPassVerifier.Configuration;
using NHSCovidPassVerifier.Utils;
using NUnit.Framework;
using Xamarin.Forms;
using Xamarin.Forms.Mocks;

namespace NHSCovidPassVerifier.Tests.UtilsTests
{
    public class NhsColourTest
    {
        [TestCase(NhsColour.NhsLinkColour, "#005eb8")]
        [TestCase(NhsColour.NhsErrorColour, "#d5281b")]
        [TestCase(NhsColour.NhsButtonColour, "#007f3b")]
        [TestCase(NhsColour.NhsButtonShadowColour, "#00401E")]
        [TestCase(NhsColour.NhsBackgroundColour, "#f0f4f5")]
        [TestCase(NhsColour.NhsDarkBackground, "#262626")]
        [TestCase(NhsColour.NhsWhite, "#ffffff")]
        [TestCase(NhsColour.NhsLinkColour, "#005eb8")]
        public void TestColourEnum(NhsColour actualColor, string resourceValue)
        {
            MockForms.Init();
            Application.Current = new App();
            
            Color actual = actualColor.Color();

            Assert.AreEqual(Color.FromHex(resourceValue), actual);
        }
    }
}
