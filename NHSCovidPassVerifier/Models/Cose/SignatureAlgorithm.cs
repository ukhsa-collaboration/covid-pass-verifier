using System;
using System.Linq;
using PeterO.Cbor;

//MIT License
//Copyright 2021 Myndigheten f�r digital f�rvaltning (DIGG)
namespace NHSCovidPassVerifier.Models.Cose
{
    //@author Henrik Bengtsson(extern.henrik.bengtsson @digg.se)
    //@author Martin Lindstr�m(martin @idsec.se)
    //@author Henric Norlander(extern.henric.norlander @digg.se)
    /// <summary>
    /// Representation of the supported signature algorithms
    /// </summary>
    public class SignatureAlgorithm
    {
        public static readonly CBORObject ES256 = CBORObject.FromObject(-7);

        public static readonly CBORObject ES384 = CBORObject.FromObject(-35);

        public static readonly CBORObject ES512 = CBORObject.FromObject(-36);

        public static readonly CBORObject PS256 = CBORObject.FromObject(-37);

        public static readonly CBORObject PS384 = CBORObject.FromObject(-38);

        public static readonly CBORObject PS512 = CBORObject.FromObject(-39);

        public static string GetAlgorithmName(CBORObject cborValue)
        {
            switch (cborValue.AsInt32())
            {
                case -7:
                    return "SHA256withECDSA";
                case -35:
                    return "SHA384withECDSA";
                case -36:
                    return "SHA512withECDSA";
                case -37:
                    return "SHA256withRSA/PSS";
                case -38:
                    return "SHA384withRSA/PSS";
                case -39:
                    return "SHA512withRSA/PSS";
                default:
                    break;
            }
            return null;
        }

        private static CBORObject[] SupportedAlgorithm = {
            ES256,
            PS256
        };
        public static bool IsSupportedAlgorithm(CBORObject cborValue)
        {
            return SupportedAlgorithm.Contains(cborValue);
        }
    }
}