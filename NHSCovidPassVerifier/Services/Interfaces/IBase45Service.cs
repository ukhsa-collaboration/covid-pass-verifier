namespace NHSCovidPassVerifier.Services.Interfaces
{
    public interface IBase45Service
    {/// <summary>
     /// Decode Base 45 String to Byte Array
     /// </summary>
     /// <param name="value">Base 45 String</param>
     /// <returns>Decoded Byte Array</returns>
        public byte[] Base45Decode(string value);
    }
}