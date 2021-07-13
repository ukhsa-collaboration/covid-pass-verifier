using NHSCovidPassVerifier.Enums;

namespace NHSCovidPassVerifier.Models
{
    public class NhsAppResult<T>
    {
        public T Data { get; set; }

        public NhsPageState State { get; set; }

        public void SetState(int statusCode)
        {
            State = (NhsPageState)statusCode;
        }
    }
}
