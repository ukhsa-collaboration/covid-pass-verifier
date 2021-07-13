using System;
using System.Collections.Generic;
using System.Linq;

namespace NHSCovidPassVerifier.Models.Cose
{


    /// @author Henrik Bengtsson (henrik@sondaica.se)
    /// @author Martin Lindstr�m (martin@idsec.se)
    /// @author Henric Norlander (extern.henric.norlander@digg.se)
    /// <summary>
    /// ASN.1 encoding support.
    /// </summary>
    public class Asn1Utils
    {
        private static byte[] SEQUENCE_TAG = new byte[] { 0x30 };

        /// <summary>
        /// Converts the supplied bytes into the ASN.1 DER encoding for an unsigned integer.
        /// </summary>
        /// <param name="i">the byte array to convert</param>
        /// <returns>the DER encoding</returns>
        public static byte[] ToUnsignedInteger(byte[] i)
        {
            var offset = 0;
            while (offset < i.Length && i[offset] == 0)
            {
                offset++;
            }
            if (offset == i.Length)
            {
                return new byte[] { 0x02, 0x01, 0x00 };
            }
            var pad = 0;
            if ((i[offset] & 0x80) != 0)
            {
                pad++;
            }

            var length = i.Length - offset;
            var der = new byte[2 + length + pad];
            der[0] = 0x02;
            der[1] = (byte)(length + pad);
            Array.Copy(i, offset, der, 2 + pad, length);

            return der;
        }
        /// <summary>
        /// Convert the supplied input to an ASN.1 Sequence.
        /// </summary>
        /// <param name="seq">the data in the sequence</param>
        /// <returns>the DER encoding</returns>
        public static byte[] ToSequence(List<byte[]> seq)
        {
            var seqBytes = ToBytes(seq);
            var seqList = new List<byte[]>
            {
                SEQUENCE_TAG,
                seqBytes.Length <= 127 ? new[] {(byte) seqBytes.Length} : new byte[] {0x81, (byte) seqBytes.Length},
                seqBytes
            };

            return ToBytes(seqList);
        }

        private static byte[] ToBytes(List<byte[]> bytes)
        {
            var len = bytes.Sum(r => r.Length);

            var b = new byte[len];
            len = 0;
            foreach (var r in bytes)
            {
                Array.Copy(r, 0, b, len, r.Length);
                len += r.Length;
            }

            return b;
        }
    }
}