using PeterO.Cbor;

namespace NHSCovidPassVerifier.Services.Utils
{
    public class CborUtils
    {
        public static string ToJson(byte[] cborDataFormatBytes)
        {
            var cborObjectFromBytes = CBORObject.DecodeFromBytes(cborDataFormatBytes);
            var jsonString = cborObjectFromBytes.ToJSONString();

            return jsonString;
        }
    }
}