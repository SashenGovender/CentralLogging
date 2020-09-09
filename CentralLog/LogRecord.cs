using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralLog
{
  public class LogRecord
  {
    public int? EventId { get; set; }
    public string Message { get; set; }
    public LogLevel LogLevel { get; set; }
    public DateTime LogTime { get; set; }

    public LogRecord(LogLevel level, string message, int? eventId = null)
    {
      EventId = eventId;
      Message = message;
      LogLevel = level;
      LogTime = DateTime.UtcNow;
    }

    public override string ToString()
    {
      return $"{LogTime} - {LogLevel} - Message: {Message} - EventId: {EventId}";
    }
  }
}
