using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using CentralLogging.Models;
using CentralLogging.Processor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;



namespace CentralLogging.Controllers
{
  [Route("website")]
  [ApiController]
  public class WebsiteController : ControllerBase
  {
    private static readonly HttpClient _client = new HttpClient(); // how do I make this once per application?
    private readonly IWordCounter _wordCounter;
    public WebsiteController(IWordCounter wordCounter)
    {
      _wordCounter = wordCounter;
    }

    [HttpGet]
    public IEnumerable<string> Get()
    {
      return new string[] { "value1", "value2" };
    }



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

    [HttpPost("count")]
    public async Task<IActionResult> QueryWebsiteCount([FromBody] MessageRequest request)
    {
      PrintThreadIdToConsole("start");

      if (string.IsNullOrWhiteSpace(request.Website))
      {
        return BadRequest();
      }

      var response = await _client.GetAsync(request.Website);
      PrintThreadIdToConsole("got response");
      if (response.StatusCode != HttpStatusCode.OK)
      {
        return BadRequest(response);
      }

      var html = await response.Content.ReadAsStringAsync();
      PrintThreadIdToConsole("got content");

      var letterCounts = await Task.Run(() => _wordCounter.CountPerLetter(html));

      PrintThreadIdToConsole("after word count");
      var jsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(letterCounts);
      return Ok(jsonResult);
    }

    private void PrintThreadIdToConsole(string message)
    {
      Console.WriteLine($"{message} - ThreadId - {Thread.CurrentThread.ManagedThreadId.ToString()} ");
    }
  }
}
