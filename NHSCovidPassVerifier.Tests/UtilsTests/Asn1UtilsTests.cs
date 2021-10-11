using System;
using NHSCovidPassVerifier.Models.Cose;
using NHSCovidPassVerifier.Services.Utils;
using NHSCovidPassVerifier.Utils;
using I18NPortable;
using NUnit.Framework;
using PeterO.Cbor;

namespace NHSCovidPassVerifier.Tests.UtilsTests
{
    public class Asn1UtilsTests
    {
        [TestCase((byte)0x02 , 2)]
        [TestCase((byte)0x22, 34)]
        public void TestToUnsignedInteger(byte inputByte, int Result)
        {
            //Given
            byte[] bytes = new byte[] {
                inputByte,
                };

            //When
            var returnedArray = Asn1Utils.ToUnsignedInteger(bytes);

            //Then
            Assert.AreEqual(returnedArray[0], 2);
            Assert.AreEqual(returnedArray[1], 1);
            Assert.AreEqual(returnedArray[2], Result);
        }

        [TestCase(0)]
        public void TestToUnsignedIntegerEmptyArray( int Result)
        {
            //Given
            byte[] bytes = new byte[] {
                
                };

            //When
            var returnedArray = Asn1Utils.ToUnsignedInteger(bytes);

            //Then
            Assert.AreEqual(returnedArray[0], 2);
            Assert.AreEqual(returnedArray[1], 1);
            Assert.AreEqual(returnedArray[2], Result);
        }

    }
}