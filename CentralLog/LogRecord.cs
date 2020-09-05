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

    public LogRecord(LogLevel level, string message, int? eventId = null)
    {
      EventId = eventId;
      Message = message;
      LogLevel = level;
    }

    public override string ToString()
    {
      return $"{LogLevel} - Message: {Message} - EventId: {EventId}";
    }
  }
}
