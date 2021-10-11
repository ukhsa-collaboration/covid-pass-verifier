using System;
using NHSCovidPassVerifier.Utils;
using I18NPortable;
using NUnit.Framework;
using NHSCovidPassVerifier.Models.International.Cards;
using NHSCovidPassVerifier.Models.Interfaces;
using NHSCovidPassVerifier.Tests.TestData;

namespace NHSCovidPassVerifier.Tests.UtilsTests
{

    public class TestResultCardTests
    {
        [Test]
        public void TestNullObjectEquals()
        {
            TestResultCard mockTestResultCard = InternationalCardData.GetEmptyTestResultCard();

            int? nullValue = null;

            bool returnedBool = mockTestResultCard.Equals(nullValue);

            Assert.AreEqual(false, returnedBool);
        }

        [Test]
        public void TestInvalidObject()
        {
            TestResultCard mockTestResultCard = InternationalCardData.GetEmptyTestResultCard();

            bool actual = true;

            bool returnedBool = mockTestResultCard.Equals(actual);

            Assert.AreEqual(false, returnedBool);
        }

        [Test]
        public void TestEqualsTestResultCard()
        {
            TestResultCard mockTestResultCard = InternationalCardData.GetTestResultCard();

            bool returnedBool = mockTestResultCard.Equals(mockTestResultCard);

            Assert.AreEqual(true, returnedBool);
        }

        [Test]
        public void TestEqualsNullTestResultCard()
        {
            TestResultCard mockTestResultCard = InternationalCardData.GetTestResultCard();

            TestResultCard? nullValue = null;

            bool returnedBool = mockTestResultCard.Equals(nullValue);

            Assert.AreEqual(false, returnedBool);

        }

        [Test]
        public void TestCompareto_Object()
        {
            TestResultCard mockTestResultCard = InternationalCardData.GetTestResultCard();

            VaccinationCard VaccCardTest = InternationalCardData.GetVaccinationCard();

            int compareToInt = mockTestResultCard.CompareTo(VaccCardTest);

            Assert.AreEqual(compareToInt, 0);

        }


        [Test]
        public void TestCompareto_ObjectNull()
        {
            TestResultCard mockTestResultCard = InternationalCardData.GetTestResultCard();

            int? nullValue = null;

            int compareToInt = mockTestResultCard.CompareTo(nullValue);

            Assert.AreEqual(compareToInt, 1);

        }

    }
}