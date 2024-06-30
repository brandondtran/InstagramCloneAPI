﻿using Microsoft.AspNetCore.Mvc;

namespace InstagramCloneAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { status = "Healthy" });
        }
    }
}