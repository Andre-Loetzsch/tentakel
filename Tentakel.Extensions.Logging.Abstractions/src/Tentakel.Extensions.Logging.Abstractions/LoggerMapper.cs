﻿using Microsoft.Extensions.Logging;

namespace Tentakel.Extensions.Logging.Abstractions
{
    public class LoggerMapper : ILogger
    {
        public ILogger Logger { get; set; }
        private static readonly Func<FormattedLogValues, Exception?, string> messageFormatter = MessageFormatter;

        private static string MessageFormatter(FormattedLogValues state, Exception? error)
        {
            return state.ToString() ?? "";
        }

        public Dictionary<string, object?> AdditionalData { get; set; } = new();

        public IDisposable BeginScope<TState>(TState state)
        {
            return this.Logger.BeginScope(state);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return this.Logger.IsEnabled(logLevel);
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            var values = new List<KeyValuePair<string, object?>>();

            if (state is IReadOnlyList<KeyValuePair<string, object?>> readOnlyList)
            {
                values.AddRange(readOnlyList);
            }

            if (this.AdditionalData != null) values.AddRange(this.AdditionalData);


            var formattedLogValues = new FormattedLogValues(state?.ToString() ?? "", values);

            this.Logger.Log(logLevel, eventId, formattedLogValues, exception, messageFormatter);
        }
    }
}