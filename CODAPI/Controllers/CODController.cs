using COD;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CODAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CODController : ControllerBase
    {
        public WeaponRepository _WeaponRepository { get; set; }
        
        public CODController(WeaponRepository weaponRepository)
        {
            _WeaponRepository = weaponRepository;                        
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        // GET: api/<CODController>
        [HttpGet]
        public ActionResult<IEnumerable<Weapon>> Get()
        {
            IEnumerable<Weapon> weapons = _WeaponRepository.GetAll();
            if (weapons.Any())
            {
                return Ok(weapons);
            }else
            {
                return NoContent();
            }
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // GET api/<CODController>/5
        [HttpGet ]
        [Route("{id}")]
        public ActionResult<Weapon> GetById([FromHeader] int id)
        {
            Weapon? weapon = _WeaponRepository.GetById(id);
            if (weapon == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(weapon);
            }
        }

        // POST api/<CODController>
        [HttpPost]
        public ActionResult<Weapon> Post([FromBody] Weapon newWeapon)
        {
            try
            {
                Weapon addedWeapon = _WeaponRepository.Add(newWeapon);
                return Created("/" + addedWeapon.Id, addedWeapon);
            }
            catch (ArgumentNullException ex) { return BadRequest(ex.Message); }


        }

        // PUT api/<CODController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        //DELETE api/<CODController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
