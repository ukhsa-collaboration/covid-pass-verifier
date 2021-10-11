using System;
using NHSCovidPassVerifier.Utils;
using I18NPortable;
using NUnit.Framework;
using NHSCovidPassVerifier.Models.International.Cards;
using NHSCovidPassVerifier.Models.Interfaces;
using NHSCovidPassVerifier.Tests.TestData;

namespace NHSCovidPassVerifier.Tests.UtilsTests
{

    public class VaccinationCardTests
    {
        [Test]
        public void TestNullObjectEquals()
        {
            VaccinationCard mockVaccinationCard = InternationalCardData.GetVaccinationCard();

            int? nullValue = null;

            bool returnedBool = mockVaccinationCard.Equals(nullValue);

            Assert.AreEqual(false, returnedBool);
        }

        [Test]
        public void TestInvalidObject()
        {
            VaccinationCard mockVaccinationCard = InternationalCardData.GetVaccinationCard();

            bool actual = true;

            bool returnedBool = mockVaccinationCard.Equals(actual);

            Assert.AreEqual(false, returnedBool);
        }

        [Test]
        public void TestEqualsTestResultCard()
        {
            VaccinationCard mockVaccinationCard = InternationalCardData.GetVaccinationCard();

            bool returnedBool = mockVaccinationCard.Equals(mockVaccinationCard);

            Assert.AreEqual(true, returnedBool);
        }

        [Test]
        public void TestEqualsNullTestResultCard()
        {
            VaccinationCard mockVaccinationCard = InternationalCardData.GetVaccinationCard();

            VaccinationCard? nullValue = null;

            bool returnedBool = mockVaccinationCard.Equals(nullValue);

            Assert.AreEqual(false, returnedBool);

        }

        [Test]
        public void TestCompareto_Object()
        {
            VaccinationCard mockVaccinationCard = InternationalCardData.GetVaccinationCard();

            VaccinationCard VaccCardTest = InternationalCardData.GetVaccinationCard();

            int compareToInt = mockVaccinationCard.CompareTo(VaccCardTest);

            Assert.AreEqual(compareToInt, 0);

        }


        [Test]
        public void TestCompareto_ObjectNull()
        {
            VaccinationCard mockVaccinationCard = InternationalCardData.GetVaccinationCard();

            int? nullValue = null;

            int compareToInt = mockVaccinationCard.CompareTo(nullValue);

            Assert.AreEqual(compareToInt, 1);

        }


    }
}