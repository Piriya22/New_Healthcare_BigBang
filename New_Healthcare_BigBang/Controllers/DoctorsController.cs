using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using New_Healthcare_BigBang.Models;
using New_Healthcare_BigBang.Repository;

namespace New_Healthcare_BigBang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorsRepo doc;

        public DoctorsController(IDoctorsRepo doc)
        {
            this.doc = doc;
        }

        [HttpGet]
        public IEnumerable<Doctors>? Get()
        {

            return doc.GetDoctor();
        }

        [HttpGet("{doctorid}")]
        public Doctors? DoctorbyId(int doctorid)
        {

            return doc.DoctorById(doctorid);


        }
        [HttpPost]
        public async Task<ActionResult<Doctors>> Post([FromForm] Doctors doctor, IFormFile imageFile)
        {

            try
            {
                var createdCourse = await doc.CreateDoctor(doctor, imageFile);
                return CreatedAtAction("Get", new { id = createdCourse.Doctor_Id }, createdCourse);

            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPut("{doctorid}")]
        public async Task<ActionResult<Doctors>> Put(int doctorid, [FromForm] Doctors doctor, IFormFile imageFile)
        {
            try
            {
                var updatedCake = await doc.UpdateDoctor(doctorid, doctor, imageFile);
                if (updatedCake == null)
                {
                    return NotFound();
                }

                return Ok(updatedCake);
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }


        [HttpDelete("{doctorid}")]
        public Doctors? DeleteDoctor(int doctorid)
        {
            return doc.DeleteDoctor(doctorid);
        }

    }
}

