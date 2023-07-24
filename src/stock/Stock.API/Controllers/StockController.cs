using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stock.Infrastructure.Context;

namespace Stock.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public StockController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet("GetAllStocks")]
        public async Task<IActionResult> GetAllStocks()
        {

            return Ok(await _appDbContext.OrderStocks.ToListAsync());
        }
    }
}
