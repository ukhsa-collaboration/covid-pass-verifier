using System;
using System.Linq;

namespace NHSCovidPassVerifier.Models
{
    public class ApiResponse
    {
        public string Endpoint { get; }
        public string ResponseText { get; set; }
        public int StatusCode { get; set; }
        public Exception Exception { get; set; }

        public ApiResponse(string url)
        {
            Endpoint = url.Split(new string[]{"/"}, StringSplitOptions.None).Last();
        }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public T Data { get; set; }

        public ApiResponse(string url) : base(url)
        {
            Data = default(T);
        }
    }
}
