using System;
using System.Collections.Generic;
using NHSCovidPassVerifier.Services.Interfaces;

namespace NHSCovidPassVerifier.Services
{
    public class Base45Service : IBase45Service
    {
        private const int BaseSize = 45;
        private const int BaseSizeSquared = 2025;
        private const int ChunkSize = 2;
        private const int EncodedChunkSize = 3;
        private const int ByteSize = 256;

        private static readonly char[] Encoding = {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
            'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
            'U', 'V', 'W', 'X', 'Y', 'Z', ' ', '$', '%', '*',
            '+', '-', '.', '/', ':' };

        private static readonly Dictionary<char, byte> Decoding = new Dictionary<char, byte>(BaseSize);

        public Base45Service()
        {
            for (byte i = 0; i < Encoding.Length; ++i)
            {
                Decoding.Add(Encoding[i], i);
            }
        }
        
        public byte[] Base45Decode(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (value.Length == 0)
            {
                return Array.Empty<byte>();
            }

            var remainderSize = value.Length % EncodedChunkSize;
            if (remainderSize == 1)
            {
                throw new FormatException("Incorrect length.");
            }

            var buffer = new byte[value.Length];
            for (var i = 0; i < value.Length; ++i)
            {
                if (!Decoding.TryGetValue(value[i], out var decoded))
                {
                    throw new FormatException($"Invalid character at position {i}.");
                }
                buffer[i] = decoded;
            }

            var wholeChunkCount = buffer.Length / EncodedChunkSize;
            var result = new byte[wholeChunkCount * ChunkSize + (remainderSize == ChunkSize ? 1 : 0)];
            var resultIndex = 0;
            var wholeChunkLength = wholeChunkCount * EncodedChunkSize;
            for (var i = 0; i < wholeChunkLength;)
            {
                var val = buffer[i++] + BaseSize * buffer[i++] + BaseSizeSquared * buffer[i++];
                result[resultIndex++] = (byte)(val / ByteSize); //result is always in the range 0-255 - % ByteSize omitted.
                result[resultIndex++] = (byte)(val % ByteSize);
            }

            if (remainderSize == 0)
            {
                return result;
            }

            result[result.Length - 1] = (byte)(buffer[buffer.Length - 2] + BaseSize * buffer[buffer.Length - 1]); //result is always in the range 0-255 - % ByteSize omitted.
            return result;
        }
    }
}