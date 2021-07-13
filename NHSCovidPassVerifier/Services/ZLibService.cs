using System.IO;
using NHSCovidPassVerifier.Services.Interfaces;
using Ionic.Zlib;

namespace NHSCovidPassVerifier.Services
{
    public class ZLibService : IZLibService
    {
        public byte[] DecompressData(byte[] compressedBytes)
        {
            using var outMemoryStream = new MemoryStream();
            using var outZStream = new ZlibStream(outMemoryStream, CompressionMode.Decompress);
            using Stream inMemoryStream = new MemoryStream(compressedBytes);
            CopyStream(inMemoryStream, outZStream);
            return outMemoryStream.ToArray();
        }
        
        private static void CopyStream(Stream input, Stream output)
        {
            var buffer = new byte[2000];
            int len;
            while ((len = input.Read(buffer, 0, 2000)) > 0)
            {
                output.Write(buffer, 0, len);
            }
            output.Flush();
        }
    }
}