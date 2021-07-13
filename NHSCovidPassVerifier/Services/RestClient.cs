using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using NHSCovidPassVerifier.Configuration;
using NHSCovidPassVerifier.Models;
using NHSCovidPassVerifier.Models.Logging;
using NHSCovidPassVerifier.Services.Interfaces;

namespace NHSCovidPassVerifier.Services
{
    public class RestClient : IRestClient
    {
        private readonly ILoggingService _loggingService = IoCContainer.Resolve<ILoggingService>();
        private readonly ISettingsService _settingsService = IoCContainer.Resolve<ISettingsService>();
        
        private static RestClient _instance;
        private static readonly object Padlock = new object();
        private static object _httpClientHandler;
        
        public HttpClient HttpClient { get; }
        public static object HttpClientHandler 
        {
            get => _httpClientHandler;
            set
            {
                if (_httpClientHandler != null) return;
                if (value is HttpMessageHandler)
                    _httpClientHandler = value;
                else
                    throw new Exception($"{nameof(HttpClientHandler)} incorrect type!");
            }
        }
        
        public static RestClient Instance
        {
            get
            {
                lock (Padlock)
                {
                    return _instance ??= new RestClient();
                }
            }
        }
        
        public RestClient()
        {
            HttpClient = HttpClientHandler != null ?
                new HttpClient((HttpMessageHandler)HttpClientHandler) : new HttpClient();

            HttpClient.Timeout = TimeSpan.FromSeconds(_settingsService.DefaultTimeout);
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/zip"));  
        }

        public virtual async Task<ApiResponse<Stream>> GetFileAsStreamAsync(string url)
        {
            Debug.Print($"Downloading file: {url}");
            var result = new ApiResponse<Stream>(url);
            try
            {
                var response = await Instance.HttpClient.GetAsync(url);

                if (response == default)
                {
                    throw new HttpRequestException($"We couldn't get a response from {url}");
                }

                result.StatusCode = (int)response.StatusCode;
                Debug.Print($"Statuscode: {(int)response.StatusCode} {response.StatusCode.ToString()}");

                if (!response.IsSuccessStatusCode)
                {
                    result.ResponseText = response.ReasonPhrase;
                    return result;
                }

                var content = await response.Content.ReadAsStreamAsync();
                if (content.Length > 0)
                    result.Data = content;

                Debug.WriteLine("Page content: " + content);
            }
            catch (Exception e)
            {
                _loggingService.LogException(LogSeverity.ERROR, e, $"{nameof(RestClient)} GetFileAsStreamAsync -> {url}");
                result.Exception = e;
            }

            return result;
        }
    }
}