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
    public class SubjectsController : ApiController
    {
        private JournalWebEntities db = new JournalWebEntities();

        // GET: api/Subjects
        public IQueryable GetSubjects()
        {
            var subjects = from d in db.Subjects
                           select new
                           {
                               id = (int)d.subject_ID,
                               name = d.subject_name,
                               leader = d.Leaders.lead_name + " " + d.Leaders.lead_surname
                           };
            return subjects;
        }

        // GET: api/Subjects/5
        [ResponseType(typeof(Subjects))]
        public IHttpActionResult GetSubjects(decimal id)
        {
            Subjects subjects = db.Subjects.Find(id);
            if (subjects == null)
            {
                return NotFound();
            }

            return Ok(subjects);
        }

        // PUT: api/Subjects/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSubjects(decimal id, Subjects subjects)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != subjects.subject_ID)
            {
                return BadRequest();
            }

            Subjects sub = new Subjects()
            {
                subject_ID = subjects.subject_ID,
                subject_name = subjects.subject_name,
                lead_ID = subjects.lead_ID
            };

            db.Entry(sub).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectsExists(id))
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

        // POST: api/Subjects
        [ResponseType(typeof(Subjects))]
        public IHttpActionResult PostSubjects(Subjects subjects)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Subjects.Add(subjects);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = subjects.subject_ID }, subjects);
        }

        // DELETE: api/Subjects/5
        [ResponseType(typeof(Subjects))]
        public IHttpActionResult DeleteSubjects(decimal id)
        {
            Subjects subjects = db.Subjects.Find(id);
            if (subjects == null)
            {
                return NotFound();
            }

            db.Subjects.Remove(subjects);
            db.SaveChanges();

            return Ok(subjects);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SubjectsExists(decimal id)
        {
            return db.Subjects.Count(e => e.subject_ID == id) > 0;
        }
    }
}