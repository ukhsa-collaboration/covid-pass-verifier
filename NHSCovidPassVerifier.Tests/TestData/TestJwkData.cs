using NHSCovidPassVerifier.Enums;
using NHSCovidPassVerifier.Models;
using NHSCovidPassVerifier.Models.Dtos;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace NHSCovidPassVerifier.Tests.TestData
{
    public static class TestJwkData
    {

        public static JwkDto GetJwkTestData(string data)
        {
            return new JwkDto
            {
                Jwk = data,
                TimeReceived = DateTime.UtcNow.AddHours(-1)
            };
        }

        public static NhsAppResult<JwkDto> GetNhsAppResultTestData(int statusCode, JwkDto data)
        {
            return new NhsAppResult<JwkDto>
            {
                Data = data,
                State = (NhsPageState)statusCode
            };
        }

        public static NhsAppResult<Stream> GetNhsStreamAppResultTestData(int statusCode, Stream stream)
        {
            return new NhsAppResult<Stream>
            {
                Data = stream,
                State = (NhsPageState)statusCode
            };
        }

        public static JwkDto GetValidJwk()
        {
            return new JwkDto
            {
                Jwk = GetValidJwkString(),
                TimeReceived = DateTime.UtcNow.AddHours(-1)
            };
        }

        public static Task<ApiResponse<Stream>> GetJwkStreamWithStatusCode(int statusCode = 200)
        {
            return GetJwkStreamWithStatusCodeAndData(GetValidJwkString(), statusCode);
        }

        public static Task<ApiResponse<Stream>> GetJwkStreamWithStatusCodeAndData(string data, int statusCode = 200)
        {
            return GetJwkStreamWithStatusCodeAndData(GenerateStreamFromString(data), statusCode);
        }

        public static Task<ApiResponse<Stream>> GetJwkStreamWithStatusCodeAndData(MemoryStream data, int statusCode = 200)
        {
            return Task.FromResult(new ApiResponse<Stream>(string.Empty)
            {
                StatusCode = statusCode,
                Data = data
            });
        }


        public static string GetValidJwkString()
        {
            return @"[
                    {'id':'KEY - 0003',
                    'keyType':{},
                    'keyOps':[{},{}],
                    'n':null,
                    'e':null,
                    'dp':null,
                    'dq':null,
                    'qi':null,
                    'p':null,
                    'q':null,
                    'curveName':{},
                    'x':'tv9sUw39E/poFP7NycT6A9KiQvDP/5cV/lfEZpZK2XU=',
                    'y':'ujs9NrKl + ADsiRwQSEziUxvSMs6Ahp8pKwYYf5WXplI=',
                    'd':null,
                    'k':null,
                    't':null}]";
        }

        private static MemoryStream GenerateStreamFromString(string value)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(value ?? ""));
        }

    }
}
