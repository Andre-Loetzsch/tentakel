﻿using System;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Tentakel.Extensions.Logging.LoggerSinks
{
    public class ConsoleColorSink : ILoggerSink
    {
        public ConsoleColorSink()
        {
        }

        public ConsoleColorSink(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }
        public string[] Categories { get; set; }
        public ConsoleColor ForegroundColor { get; set; } = ConsoleColor.Gray;
        public LogLevel LogLevel { get; set; }

        public bool IsEnabled(LogLevel logLevel)
        {
            return this.LogLevel <= logLevel;
        }

        public void Log(LogEntry logEntry)
        {
            var sb = new StringBuilder();

            if (logEntry.Scopes != null)
            {
                foreach (var scope in logEntry.Scopes)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(" => ");
                    }

                    sb.AppendLine().Append(scope);
                }
            }

            var foregroundColor = Console.ForegroundColor;
            Console.ForegroundColor = this.ForegroundColor;
            Console.WriteLine($"{logEntry.DateTime.ToString(logEntry.DateTimeFormat)}|{logEntry.EventId}|{logEntry.LogLevel}|{logEntry.LoggerSinkName}|{logEntry.Source}|{logEntry.Message}|Attributes:{logEntry.Attributes.ToLogString()}|Scope:{sb}");
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine();
        }
    }

    public class ConsoleSink : ILoggerSink
    {
        public string Name { get; set; }
        public string[] Categories { get; set; }
        public ConsoleColor ForegroundColor { get; set; } = ConsoleColor.Gray;
        public LogLevel LogLevel { get; set; }

        public bool IsEnabled(LogLevel logLevel)
        {
            return this.LogLevel <= logLevel;
        }

        public void Log(LogEntry logEntry)
        {
            var sb = new StringBuilder();

            if (logEntry.Scopes != null)
            {
                foreach (var scope in logEntry.Scopes)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(" => ");
                    }

                    sb.AppendLine().Append(scope);
                }
            }

            var foregroundColor = Console.ForegroundColor;
            Console.ForegroundColor = this.ForegroundColor;
            Console.WriteLine($"{logEntry.DateTime.ToString(logEntry.DateTimeFormat)}|{logEntry.EventId}|{logEntry.LogLevel}|{logEntry.LoggerSinkName}|{logEntry.Source}|{logEntry.Message}|Attributes:{logEntry.Attributes.ToLogString()}|Scope:{sb}");
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine();
        }
    }
}