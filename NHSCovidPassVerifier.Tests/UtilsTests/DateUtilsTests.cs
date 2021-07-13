using System;
using NHSCovidPassVerifier.Utils;
using I18NPortable;
using NUnit.Framework;

namespace NHSCovidPassVerifier.Tests.UtilsTests
{
    public class DateUtilsTests
    {

        [TestCase("01/08/2021 14:00:00", "8 Jan 2021 at 14:00")]
        [TestCase("05/29/2021", "29 May 2021 at 00:00")]
        [TestCase("1/9/2021 05:50:06", "9 Jan 2021 at 05:50")]
        [TestCase("2021/10/12", "12 Oct 2021 at 00:00")]
        [TestCase("2021-02-28T05:50:06", "28 Feb 2021 at 05:50")]
        public void TestFormatDateTime(DateTime date, string expected)
        {
            var actual = date.FormatDateTime();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestFormatTimeToMidnight()
        {
            //Given 
            var midnight = DateTime.Parse("2021/10/12 00:00:00");
            //When 
            var actual = DateUtils.FormatTimeToMidnightMidday(midnight);
            //Then
            Assert.AreEqual("MIDNIGHT_TEXT".Translate(), actual);
        }

        [Test]
        public void TestFormatTimeToMidday()
        {
            //Given 
            var midnight = DateTime.Parse("2021/10/12 12:00:00");
            //When 
            var actual = DateUtils.FormatTimeToMidnightMidday(midnight);
            //Then
            Assert.AreEqual("MIDDAY_TEXT".Translate(), actual);
        }
        
        [TestCase("01/08/2021 11:00:00")]
        [TestCase("1/9/2021 05:50:06")]
        [TestCase("2021-02-28T05:50:06")]
        public void TestFormatTimeToMidnightMiddayAm(DateTime date)
        {
            var actual = DateUtils.FormatTimeToMidnightMidday(date);
            Assert.That(actual, Does.EndWith("am"));
        }
        
        [TestCase("01/08/2021 14:00:00")]
        [TestCase("1/9/2021 21:50:06")]
        [TestCase("2021-02-28T15:50:06")]
        public void TestFormatTimeToMidnightMiddayPm(DateTime date)
        {
            var actual = DateUtils.FormatTimeToMidnightMidday(date);
            Assert.That(actual, Does.EndWith("pm"));
        }
    }
}