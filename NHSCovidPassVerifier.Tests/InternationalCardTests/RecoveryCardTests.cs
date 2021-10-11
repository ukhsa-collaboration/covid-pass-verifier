using System;
using NHSCovidPassVerifier.Utils;
using I18NPortable;
using NUnit.Framework;
using NHSCovidPassVerifier.Models.International.Cards;
using NHSCovidPassVerifier.Models.Interfaces;
using NHSCovidPassVerifier.Tests.TestData;

namespace NHSCovidPassVerifier.Tests.UtilsTests
{

    public class RecoveryCardTests
    {
        [Test]
        public void TestNullObjectEquals()
        {
            RecoveryCard mockRecoveryCard = InternationalCardData.GetRecoveryCard();

            int? nullValue = null;

            bool returnedBool = mockRecoveryCard.Equals(nullValue);

            Assert.AreEqual(false, returnedBool);
        }

        [Test]
        public void TestInvalidObject()
        {
            RecoveryCard mockRecoveryCard = InternationalCardData.GetRecoveryCard();

            bool actual = true;

            bool returnedBool = mockRecoveryCard.Equals(actual);

            Assert.AreEqual(false, returnedBool);
        }

        [Test]
        public void TestEqualsTestResultCard()
        {
            RecoveryCard mockRecoveryCard = InternationalCardData.GetRecoveryCard();

            bool returnedBool = mockRecoveryCard.Equals(mockRecoveryCard);

            Assert.AreEqual(true, returnedBool);
        }

        [Test]
        public void TestEqualsNullTestResultCard()
        {
            RecoveryCard mockRecoveryCard = InternationalCardData.GetRecoveryCard();

            RecoveryCard? nullValue = null;

            bool returnedBool = mockRecoveryCard.Equals(nullValue);

            Assert.AreEqual(false, returnedBool);

        }

        [Test]
        public void TestCompareto_Object()
        {
            RecoveryCard mockRecoveryCard = InternationalCardData.GetRecoveryCard();

            VaccinationCard VaccCardTest = InternationalCardData.GetVaccinationCard();

            int compareToInt = mockRecoveryCard.CompareTo(VaccCardTest);

            Assert.AreEqual(compareToInt, 0);


        }


        [Test]
        public void TestCompareto_ObjectNull()
        {

            RecoveryCard mockRecoveryCard = InternationalCardData.GetRecoveryCard();

            int? nullValue = null;

            int compareToInt = mockRecoveryCard.CompareTo(nullValue);

            Assert.AreEqual(compareToInt, 1);

        }

    }
}