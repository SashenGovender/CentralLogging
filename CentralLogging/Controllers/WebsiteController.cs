using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CentralLogging.Models;
using Microsoft.AspNetCore.Mvc;

namespace CentralLogging.Controllers
{
  [Route("website")]
  [ApiController]
  public class WebsiteController : ControllerBase
  {
    private static readonly HttpClient _client = new HttpClient(); // how do I make this once per application?

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

  }
}
