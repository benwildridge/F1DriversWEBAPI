using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {

        private readonly DataContext _context;

        public DriverController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Driver>>> Get()
        {
            var allDrivers = await _context.DriverInformation.ToListAsync();
            if (allDrivers.Count == 0 ){
                return BadRequest("No Drivers Found");
            }
            return Ok(await _context.DriverInformation.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Driver>> Get(int id)
        {
            var findDriver = await _context.DriverInformation.FindAsync(id);
            if (findDriver == null)
                return BadRequest("Driver not found");
            return Ok(findDriver);
        }
        
        [HttpPost]
        public async Task<ActionResult<List<Driver>>> AddDriver([FromBody]Driver driver)
        {
            _context.DriverInformation.Add(driver);
            await _context.SaveChangesAsync();
            return Ok(await _context.DriverInformation.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Driver>>> UpdateHero(Driver request)
        {
            var updateDriver = await _context.DriverInformation.FindAsync(request.Id);
            if (updateDriver == null)
                return BadRequest("Driver was not found");

            updateDriver.Name = request.Name;
            updateDriver.Team = request.Team;
            updateDriver.Race_Wins = request.Race_Wins;
            await _context.SaveChangesAsync();

            return Ok(await _context.DriverInformation.ToListAsync());
                  
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Driver>>> DeleteHero(int id)
        {
            var deleteDriver = await _context.DriverInformation.FindAsync(id);
            if (deleteDriver == null)
                return BadRequest("Driver was not found");
            _context.DriverInformation.Remove(deleteDriver);
            await _context.SaveChangesAsync();
            return Ok(await _context.DriverInformation.ToListAsync());

        }
    }
}
