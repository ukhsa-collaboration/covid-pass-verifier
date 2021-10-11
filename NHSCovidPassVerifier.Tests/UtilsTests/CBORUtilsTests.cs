using System;
using NHSCovidPassVerifier.Services.Utils;
using NHSCovidPassVerifier.Utils;
using I18NPortable;
using NUnit.Framework;
using PeterO.Cbor;

namespace NHSCovidPassVerifier.Tests.UtilsTests
{
    public class CBORUtilsTests
    {
        [TestCase((byte)0x02 , "2")]
        [TestCase((byte)0x22, "-3")]
        public void TestCBORToJson(byte inputByte, string result)
        {
            //Given
            byte[] bytes = new byte[] {
                inputByte,
                };

            //When
            string returnedJsonString = CborUtils.ToJson(bytes);

            //Then
            Assert.AreEqual(returnedJsonString, result);
        }
       
    }
}