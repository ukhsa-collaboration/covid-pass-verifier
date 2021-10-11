using System;
using NHSCovidPassVerifier.Utils;
using I18NPortable;
using NUnit.Framework;

namespace NHSCovidPassVerifier.Tests.UtilsTests
{
    public class SetDashIfNoValueUtilTests
    {

        [TestCase("TestString")]
        [TestCase("Test String")]
        [TestCase("T")]
        public void TestSetDashIfNoValueString(string expected)
        {
            //given 
            var returnedValue = SetDashIfNoValueUtil.SetDashIfNoValue(expected);

            //When
            Assert.AreEqual(expected, returnedValue);

        }

        [TestCase("01/08/2021 14:00:00")]
        [TestCase("12/25/2021 14:00:00")]
        [TestCase("01/01/2021 23:59:59")]
        public void TestSetDashIfNoValueDate(DateTime InputDate)
        {
            //Given 
            var returnedValue = SetDashIfNoValueUtil.SetDashIfNoValue(InputDate);

            //When
            var expected = DateUtils.FormatDate(InputDate);

            //Then
            Assert.AreEqual(expected, returnedValue);

        }

        [TestCase("")]
        public void TestSetDashIfNoValueEmptyString(string expected)
        {
            //given 
            var returnedValue = SetDashIfNoValueUtil.SetDashIfNoValue(expected);       

            //When
            expected = "-";

            //Then
            Assert.AreEqual(expected, returnedValue);
            Assert.AreEqual(expected.Translate(), returnedValue.Translate());

        }
    }
}