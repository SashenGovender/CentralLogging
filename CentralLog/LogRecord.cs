using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CentralLog
{
  public class LogRecord
  {
    public int? EventId { get; set; }
    public string Message { get; set; }

    public LogLevel LogLevel { get; set; }
    public DateTime LogTime { get; set; }
    public Exception Exception { get; set; }

    public LogRecord(LogLevel level, string message, int? eventId = null, Exception except = null)
    {
      EventId = eventId;
      Message = message;
      LogLevel = level;
      LogTime = DateTime.UtcNow;
      Exception = except;
    }

    public override string ToString()
    {
      return $"{LogTime} - {LogLevel} - Message: {Message} - EventId: {EventId}";
    }
  }
}
