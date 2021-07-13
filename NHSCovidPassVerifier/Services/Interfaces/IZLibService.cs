namespace NHSCovidPassVerifier.Services.Interfaces
{
    public interface IZLibService
    {
        public byte[] DecompressData(byte[] inData);
    }
}