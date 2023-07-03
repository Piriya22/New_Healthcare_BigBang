using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using New_Healthcare_BigBang.Models;
using New_Healthcare_BigBang.Repository;

namespace New_Healthcare_BigBang.Controllers
{
    //[Authorize(Roles = "Patients")]
    //[Authorize(Roles = "Doctors")]
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientsRepo _patientRepository;

        public PatientsController(IPatientsRepo patientRepository)
        {
            _patientRepository = patientRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patients>>> GetAllPatients()
        {
            try
            {
                var patients = await _patientRepository.GetAllPatients();
                return Ok(patients);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving patients: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Patients>> GetPatientById(int id)
        {
            try
            {
                var patient = await _patientRepository.GetPatientById(id);
                if (patient == null)
                {
                    return NotFound();
                }
                return Ok(patient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving patient: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Patients>> PostPatient(Patients patient)
        {
            var createdPatient = await _patientRepository.PostPatient(patient);

            if (createdPatient == null)
            {
                return Problem("Failed to create patient.");
            }

            //return CreatedAtAction("GetPatient", new { id = createdPatient.patient_id }, createdPatient);
            //return CreatedAtAction("Get", new { id = createdCourse.doctor_id }, createdCourse);
            return Created("Get", createdPatient);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<int>> UpdatePatient(int id, Patients patient)
        {
            try
            {
                if (id != patient.Patient_Id)
                {
                    return BadRequest("Invalid patient ID.");
                }

                var result = await _patientRepository.UpdatePatient(id, patient);
                if (result == 0)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating patient: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeletePatient(int id)
        {
            try
            {
                var result = await _patientRepository.DeletePatient(id);
                if (result == 0)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting patient: {ex.Message}");
            }
        }
    }
}

