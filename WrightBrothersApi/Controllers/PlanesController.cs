using Microsoft.AspNetCore.Mvc;
using WrightBrothersApi.Models;

namespace WrightBrothersApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlanesController : ControllerBase
    {
        private readonly ILogger<PlanesController> _logger;

        public PlanesController(ILogger<PlanesController> logger)
        {
            _logger = logger;
        }

        private static readonly List<Plane> Planes = new List<Plane>
        {
            new Plane
            {
                Id = 1,
                Name = "Wright Flyer",
                Year = 1903,
                Description = "The first successful heavier-than-air powered aircraft.",
                RangeInKm = 12,
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/8/8d/Wright_Flyer.jpg"
            },
            new Plane
            {
                Id = 2,
                Name = "Wright Flyer II",
                Year = 1904,
                Description = "A refinement of the original Flyer with better performance.",
                RangeInKm = 24,
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/8/8d/Wright_Flyer.jpg"
            },
            new Plane
            {
                Id = 3,
                Name = "Wright Flyer III",
                Year = 1905,
                Description = "The first fully controllable Flyer.",
                RangeInKm = 39,
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/8/8d/Wright_Flyer.jpg"
            }
        };

        [HttpGet]
        public ActionResult<List<Plane>> Get()
        {
            Console.WriteLine("GET /planes");
            return Planes;
        }

        [HttpGet("{id}")]
        public ActionResult<Plane> Get(int id)
        {
            Console.WriteLine($"GET /plane with Id = [{id}]");
            var plane = Planes.Find(p => p.Id == id);

            if (plane == null)
            {
                return NotFound();
            }

            return plane;
        }

        [HttpPost]
        public ActionResult<Plane> Post(Plane plane)
        {
            Console.WriteLine($"POST /planes with Id = [{plane.Id}]");
            Planes.Add(plane);

            return CreatedAtAction(nameof(Get), new { id = plane.Id }, plane);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Plane plane)
        {
            if (id != plane.Id)
            {
                return BadRequest();
            }

            var index = Planes.FindIndex(p => p.Id == id);

            if (index == -1)
            {
                return NotFound();
            }

            Planes[index] = plane;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var index = Planes.FindIndex(p => p.Id == id);

            if (index == -1)
            {
                return NotFound();
            }

            Planes.RemoveAt(index);

            return NoContent();
        }

        // calculate the count of planes between two input years
        [HttpGet("count")]
        public ActionResult<int> Count(int startYear, int endYear)
        {
            var count = Planes.Count(p => p.Year >= startYear && p.Year <= endYear);

            return count;
        }
    }
}
