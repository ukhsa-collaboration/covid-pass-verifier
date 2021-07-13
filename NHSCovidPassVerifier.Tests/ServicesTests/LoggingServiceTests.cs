using NHSCovidPassVerifier.Models.Logging;
using NHSCovidPassVerifier.Services;
using NHSCovidPassVerifier.Services.Interfaces;
using NHSCovidPassVerifier.Tests.TestData;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace NHSCovidPassVerifier.Tests.ServicesTests
{
    public class LoggingServiceTests
    {
        private readonly LoggingService _loggingService;
        private readonly Mock<IConsoleService> _consoleService;
        private readonly Mock<IJwkRepository> _jwkRepository;
        private readonly Mock<IAppCenterService> _appCenterService;

        public LoggingServiceTests()
        {
            _consoleService = new Mock<IConsoleService>();
            _jwkRepository = new Mock<IJwkRepository>();
            _appCenterService = new Mock<IAppCenterService>();
            _loggingService = new LoggingService(_consoleService.Object, _appCenterService.Object);
        }

        [TestCase(LogSeverity.INFO)]
        [TestCase(LogSeverity.WARNING)]
        [TestCase(LogSeverity.ERROR)]
        public void LogApiErrorPrintsToConsoleAtLeast3TimesWhenNoAdditionalInfo(LogSeverity severity)
        {
            //given
            _jwkRepository.Setup(x => x.GetJwk()).Returns(TestJwkData.GetJwkStreamWithStatusCode());
            var response = _jwkRepository.Object.GetJwk().Result;
            //when
            _loggingService.LogApiError(severity, response);
            //then
            _appCenterService.Verify(x => x.TrackEvent("API Error", It.IsAny<IDictionary<string, string>>()), Times.AtLeastOnce);
            _consoleService.Verify(x => x.PrintToDebugConsole(It.IsAny<string>()), Times.AtLeast(3));
        }


        [TestCase(LogSeverity.INFO)]
        [TestCase(LogSeverity.WARNING)]
        [TestCase(LogSeverity.ERROR)]
        public void LogExceptionPrintsToConsoleAtLeast3TimesWhenNoAdditionalInfo(LogSeverity severity)
        {
            //given
            var e = new Exception();
            //when
            _loggingService.LogException(severity, e);
            //then
            _appCenterService.Verify(x => x.TrackError(e, It.IsAny<IDictionary<string, string>>()), Times.AtLeastOnce);
            _consoleService.Verify(x => x.PrintToDebugConsole(It.IsAny<string>()), Times.AtLeast(3));
        }

        [TestCase(LogSeverity.INFO)]
        [TestCase(LogSeverity.WARNING)]
        [TestCase(LogSeverity.ERROR)]
        public void LogMessagePrintsToConsoleAtLeast3TimesWhenNoAdditionalInfo(LogSeverity severity)
        {
            //given
            var message = string.Empty;
            //when
            _loggingService.LogMessage(severity, message);
            //then
            _appCenterService.Verify(x => x.TrackEvent(message, It.IsAny<IDictionary<string, string>>()), Times.AtLeastOnce);
            _consoleService.Verify(x => x.PrintToDebugConsole(It.IsAny<string>()), Times.AtLeast(3));
        }

        [TestCase(LogSeverity.INFO)]
        [TestCase(LogSeverity.WARNING)]
        [TestCase(LogSeverity.ERROR)]
        public void LogApiErrorPrintsToConsoleAtLeast5TimesWhenAdditionalInfo(LogSeverity severity)
        {
            //given
            _jwkRepository.Setup(x => x.GetJwk()).Returns(TestJwkData.GetJwkStreamWithStatusCode());
            var response = _jwkRepository.Object.GetJwk().Result;
            //when
            _loggingService.LogApiError(severity, response, string.Empty);
            //then
            _appCenterService.Verify(x => x.TrackEvent("API Error", It.IsAny<IDictionary<string, string>>()), Times.AtLeastOnce);
            _consoleService.Verify(x => x.PrintToDebugConsole(It.IsAny<string>()), Times.AtLeast(5));
        }


        [TestCase(LogSeverity.INFO)]
        [TestCase(LogSeverity.WARNING)]
        [TestCase(LogSeverity.ERROR)]
        public void LogExceptionPrintsToConsoleAtLeast5TimesWhenAdditionalInfo(LogSeverity severity)
        {
            //given
            var e = new Exception();
            //when
            _loggingService.LogException(severity, e, string.Empty);
            //then
            _appCenterService.Verify(x => x.TrackError(e, It.IsAny<IDictionary<string, string>>()), Times.AtLeastOnce);
            _consoleService.Verify(x => x.PrintToDebugConsole(It.IsAny<string>()), Times.AtLeast(5));
        }
    }
}
