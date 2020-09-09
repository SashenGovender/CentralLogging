using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using CentralLog;
using GeneralOperationsAPI.Models;
using GeneralOperationsAPI.Processor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GeneralOperationsAPI.Controllers
{
  [Route("website")]
  [ApiController]
  public class WebsiteController : ControllerBase
  {
    private static readonly HttpClient _client = new HttpClient(); // how do I make this once per application?
    private readonly IWordCounter _wordCounter;
    private readonly ILogger<WebsiteController> _logger;

    public WebsiteController(IWordCounter wordCounter, ILogger<WebsiteController> logger)
    {
      _wordCounter = wordCounter;
      _logger = logger;
    }

    /// <summary>
    /// Query the specified website for its html content
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> QueryWebsite([FromBody] MessageRequest request)
    {
      if (string.IsNullOrWhiteSpace(request.Website))
      {
        return BadRequest();
      }

      var response = await _client.GetAsync(request.Website);
      if (response.StatusCode != HttpStatusCode.OK)
      {
        return BadRequest(response);
      }

      var html = response.Content.ReadAsStringAsync();

      return Ok(html);
    }

    /// <summary>
    ///  Query the specified website for the count of each character
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("count")]
    public async Task<IActionResult> QueryWebsiteCount([FromBody] MessageRequest request)
    {
      PrintThreadIdToConsole("start");
      LogContext.Context.AddLog(LogLevel.Information, $"Received Message Request - {JsonSerializer.Serialize(request)}", 0);

      if (string.IsNullOrWhiteSpace(request.Website))
      {
        LogContext.Context.AddLog(LogLevel.Warning, $"Invalid Url - '{request.Website}'", 0);
        return BadRequest();
      }

      var response = await _client.GetAsync(request.Website);
      PrintThreadIdToConsole("got response");

      if (response.StatusCode != HttpStatusCode.OK)
      {
        LogContext.Context.AddLog(LogLevel.Warning, $"'{request.Website}' returned back a {response.StatusCode}", 0);
        return BadRequest(response);
      }

      var html = await response.Content.ReadAsStringAsync();
      PrintThreadIdToConsole("got content");
      if (html.Length > 50000)
      {
        LogContext.Context.AddLog(LogLevel.Warning, $"'{request.Website}' returned back a very large html page of length {html.Length}", 0);
        //throw new Exception("html page too large");
      }

      var letterCounts = await Task.Run(() => _wordCounter.CountPerLetter(html));
      PrintThreadIdToConsole("after word count");

      var jsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(letterCounts);

      LogContext.Context.AddLog(LogLevel.Information, $"Processing Complete -about to return ok response", 0);

      var logs = LogContext.Context.GetLogs(LogLevel.Error);
      WriteLogs(logs);
      //LogContext.Context.WriteToFile($@"C:\MyLogs\GeneralOperationsAPI_V1-{DateTime.Now.ToString("yyy-MM-dd")}.log");
      

      return Ok(jsonResult);
    }

    private void PrintThreadIdToConsole(string message)
    {
      Console.WriteLine($"{message} - ThreadId - {Thread.CurrentThread.ManagedThreadId} ");
    }

    private void WriteLogs(IReadOnlyList<LogRecord> logs)
    {
      var jsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(logs, Newtonsoft.Json.Formatting.Indented);

        _logger.Log(LogLevel.Information, jsonResult);
    }
  }
}
