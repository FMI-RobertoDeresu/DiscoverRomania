﻿using System;
using JRS.DR.Contracts;
using Microsoft.AspNetCore.Http;
using NLog;

namespace JRS.DR.Logging
{
    public class RequestHistoryLogger : IRequestHistoryLogger
    {
        private const string LoggerName = "RequestHistoryLogger";

        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RequestHistoryLogger(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = LogManager.GetLogger(LoggerName);
        }

        public void LogRequest()
        {
            var eventLog = new LogEventInfo(LogLevel.Info, LoggerName, string.Empty);
            eventLog.Properties["Created"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            eventLog.Properties["Ip"] = LogProperties.GetIp(_httpContextAccessor.HttpContext);
            eventLog.Properties["Username"] = LogProperties.GetUsername(_httpContextAccessor.HttpContext);
            eventLog.Properties["HttpMethod"] = LogProperties.GetHttpMethod(_httpContextAccessor.HttpContext);
            eventLog.Properties["Url"] = LogProperties.GetUrl(_httpContextAccessor.HttpContext);
            eventLog.Properties["UrlReferrer"] = LogProperties.GetUrlReferer(_httpContextAccessor.HttpContext);
            _logger.Log(eventLog);
        }
    }
}