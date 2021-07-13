using PeterO.Cbor;

//MIT License
//Copyright 2021 Myndigheten f�r digital f�rvaltning (DIGG)
namespace NHSCovidPassVerifier.Models.Cose
{

    //@author Henrik Bengtsson (extern.henrik.bengtsson@digg.se)
    //@author Martin Lindstr�m (martin@idsec.se)
    //@author Henric Norlander (extern.henric.norlander@digg.se)
    /// <summary>
    /// Representation of COSE header parameter keys.
    ///https://tools.ietf.org/html/rfc8152#section-3
    /// </summary>
    public class HeaderParameterKey
    {
        public static readonly CBORObject ALGORITHIM = CBORObject.FromObject(1);

        public static readonly CBORObject CRITICAL_HEADER = CBORObject.FromObject(2);

        public static readonly CBORObject CONTENT_TYPE = CBORObject.FromObject(3);

        public static readonly CBORObject KID = CBORObject.FromObject(4);
    }
}