using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entity;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.Model;
using Swashbuckle.AspNetCore;

namespace RestTask.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IDbService _service;

        public StudentsController(IDbService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }
        
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetStudents()
        {
            var students = await _service.GetAll();
            return Ok(students);
        }

        [HttpGet("{id}", Name = "Get")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<StudentEntity>> GetStudent(int id)
        {
            StudentEntity student = await _service.Get(id);
            if (student == null)
            {
                return NotFound("The Student record couldn't be found.");
            }

            return Ok(student);
        }
        
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> PostStudent([FromBody] UpdateStudentRequest student)
        {
            if (student == null)
            {
                return BadRequest(ModelState);
            }

            StudentEntity stud = await _service.Add(student);
            return CreatedAtRoute("Get", new { Id = stud.Id }, stud);
        }
        
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PutStudent(int id, [FromBody] UpdateStudentRequest student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _service.Update(id, student);
            }
            catch (RequestedResourceNotFoundException)
            {
                return NotFound("The Student record couldn't be found.");
            }

            return NoContent();
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                await _service.Delete(id);
            }
            catch (RequestedResourceNotFoundException)
            {
                return NotFound("The Student record couldn't be found.");
            }

            return NoContent();
        }
    }
}
