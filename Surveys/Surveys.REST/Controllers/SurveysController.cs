using Microsoft.AspNetCore.Mvc;
using Surveys.Application.DTOs.Surveys;

namespace Surveys.REST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveysController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<string>> HelloWorld()
        {
            return await Task.FromResult("hello world");
        }
    }
}
