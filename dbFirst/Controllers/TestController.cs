using Microsoft.AspNetCore.Mvc;
using dbFirst.Services;
using dbFirst.Models;

namespace dbFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : Controller
    {
        [HttpGet("addFlight")]
        public async Task< ActionResult> AddFlihgt()
        {
            dbWorker db = new dbWorker();
            db.AddFligt("LED");
            return new OkResult();
        }
    }
}
