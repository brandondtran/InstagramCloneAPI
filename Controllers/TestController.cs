using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InstagramCloneAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public class TestController(ILogger<TestController> logger) : ControllerBase
    {

        // GET: api/Test
        [HttpGet]
        public IActionResult Get()
        {
            if (Request.Headers.ContainsKey("Authorization"))
            {
                var token = Request.Headers["Authorization"].ToString();
                if (token.StartsWith("Bearer ", System.StringComparison.OrdinalIgnoreCase))
                {
                    logger.LogInformation($"Bearer Token: {token.Substring("Bearer ".Length)}");
                }
            }
            return Ok(new { message = "GET request received" });
        }

        // GET: api/Test/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(new { message = $"GET request received for id: {id}" });
        }

        // POST: api/Test
        [HttpPost]
        public IActionResult Post([FromBody] object value)
        {
            return CreatedAtAction(nameof(Post), new { message = "POST request received", data = value });
        }

        // PUT: api/Test/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] object value)
        {
            return Ok(new { message = $"PUT request received for id: {id}", data = value });
        }

        // DELETE: api/Test/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(new { message = $"DELETE request received for id: {id}" });
        }

        // PATCH: api/Test/5
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] object value)
        {
            return Ok(new { message = $"PATCH request received for id: {id}", data = value });
        }

        // OPTIONS: api/Test
        [HttpOptions]
        public IActionResult Options()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE, PATCH, OPTIONS");
            return Ok();
        }
    }
}