using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using StudentsJournalWeb.Models;

namespace StudentsJournalWeb.Controllers
{
    [Authorize(Roles = "Administrator, Leader")]
    public class StudentsController : ApiController
    {
        private JournalWebEntities db = new JournalWebEntities();

        // GET: api/Students
        public IQueryable GetStudents()
        {
            var students = from s in db.Students select new
                            {
                                id = (int)s.student_ID,
                                name = s.student_name,
                                surname = s.student_surname,
                                indexNumber = s.student_index_number,
                                login = s.student_login,
                                deanGroup = s.DeanGroup.group_name,
                                department = s.DeanGroup.Department.department_name,
                                direction = s.DeanGroup.Department.Direction.direction_name
                            };
            return students;
        }

        // GET: api/Students/5
        [ResponseType(typeof(Students))]
        public IHttpActionResult GetStudents(decimal id)
        {
            Students students = db.Students.Find(id);
            if (students == null)
            {
                return NotFound();
            }

            return Ok(students);
        }

        // PUT: api/Students/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStudents(decimal id, Students students)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != students.student_ID)
            {
                return BadRequest();
            }

            db.Entry(students).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Students
        [ResponseType(typeof(Students))]
        public IHttpActionResult PostStudents(Students students)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Students.Add(students);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = students.student_ID }, students);
        }

        // DELETE: api/Students/5
        [ResponseType(typeof(Students))]
        public IHttpActionResult DeleteStudents(decimal id)
        {
            Students students = db.Students.Find(id);
            if (students == null)
            {
                return NotFound();
            }

            db.Students.Remove(students);
            db.SaveChanges();

            return Ok(students);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudentsExists(decimal id)
        {
            return db.Students.Count(e => e.student_ID == id) > 0;
        }
    }
}