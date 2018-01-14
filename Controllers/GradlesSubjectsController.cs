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
    public class GradlesSubjectsController : ApiController
    {
        private JournalWebEntities db = new JournalWebEntities();

        // GET: api/GradlesSubjects/5
        [ResponseType(typeof(GradlesSubjects))]
        public IHttpActionResult GetGradlesSubjects(decimal id, string isLeader)
        {
            var studentGradle = from d in db.GradlesSubjects
                                where d.student_ID == id
                                select new
                                {
                                    type = d.ClassessType.type_name,
                                    leader = d.Leaders.lead_name + " " + d.Leaders.lead_surname,
                                    gradle = d.gradle_subject,
                                    date = d.gradle_subject_date
                                };

            var leadGradle = from d in db.GradlesSubjects
                             where d.lead_ID == id
                             select new
                             {
                                 type = d.ClassessType.type_name,
                                 student = d.Students.student_name + " " + d.Students.student_surname,
                                 gradle = d.gradle_subject,
                                 date = d.gradle_subject_date
                             };

            if (isLeader.Equals("true"))
            {
                if (leadGradle == null)
                {
                    return NotFound();
                }

                return Ok(leadGradle);
            }
            else
            {
                if (studentGradle == null)
                {
                    return NotFound();
                }

                return Ok(studentGradle);
            }
        }

        // PUT: api/GradlesSubjects/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGradlesSubjects(decimal id, GradlesSubjects gradlesSubjects)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gradlesSubjects.student_ID)
            {
                return BadRequest();
            }

            db.Entry(gradlesSubjects).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GradlesSubjectsExists(id))
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

        // POST: api/GradlesSubjects
        [ResponseType(typeof(GradlesSubjects))]
        public IHttpActionResult PostGradlesSubjects(GradlesSubjects gradlesSubjects)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.GradlesSubjects.Add(gradlesSubjects);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (GradlesSubjectsExists(gradlesSubjects.student_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = gradlesSubjects.student_ID }, gradlesSubjects);
        }

        // DELETE: api/GradlesSubjects/5
        [ResponseType(typeof(GradlesSubjects))]
        public IHttpActionResult DeleteGradlesSubjects(decimal id)
        {
            GradlesSubjects gradlesSubjects = db.GradlesSubjects.Find(id);
            if (gradlesSubjects == null)
            {
                return NotFound();
            }

            db.GradlesSubjects.Remove(gradlesSubjects);
            db.SaveChanges();

            return Ok(gradlesSubjects);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GradlesSubjectsExists(decimal id)
        {
            return db.GradlesSubjects.Count(e => e.student_ID == id) > 0;
        }
    }
}