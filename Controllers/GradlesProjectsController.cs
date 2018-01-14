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
    [Authorize(Roles = "Leader, User")]
    public class GradlesProjectsController : ApiController
    {
        private JournalWebEntities db = new JournalWebEntities();

        // GET: api/GradlesProjects/5
        [ResponseType(typeof(GradlesProjects))]
        public IHttpActionResult GetGradlesProjects(decimal id, string isLeader)
        {
            var studentGradle = from d in db.GradlesProjects
                                where d.student_ID == id
                                select new
                                {
                                    project = d.Projects.project_title,
                                    leader = d.Leaders.lead_name + " " + d.Leaders.lead_surname,
                                    gradle = d.gradle_value,
                                    date = d.gradle_date
                                };

            var leadGradle = from d in db.GradlesProjects
                             where d.lead_ID == id
                             select new
                             {
                                 project = d.Projects.project_title,
                                 student = d.Students.student_name + " " + d.Students.student_surname,
                                 gradle = d.gradle_value,
                                 date = d.gradle_date
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

        // PUT: api/GradlesProjects/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGradlesProjects(decimal id, GradlesProjects gradlesProjects)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gradlesProjects.lead_ID)
            {
                return BadRequest();
            }

            db.Entry(gradlesProjects).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GradlesProjectsExists(id))
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

        // POST: api/GradlesProjects
        [ResponseType(typeof(GradlesProjects))]
        public IHttpActionResult PostGradlesProjects(GradlesProjects gradlesProjects)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.GradlesProjects.Add(gradlesProjects);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (GradlesProjectsExists(gradlesProjects.lead_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = gradlesProjects.lead_ID }, gradlesProjects);
        }

        // DELETE: api/GradlesProjects/5
        [ResponseType(typeof(GradlesProjects))]
        public IHttpActionResult DeleteGradlesProjects(decimal id)
        {
            GradlesProjects gradlesProjects = db.GradlesProjects.Find(id);
            if (gradlesProjects == null)
            {
                return NotFound();
            }

            db.GradlesProjects.Remove(gradlesProjects);
            db.SaveChanges();

            return Ok(gradlesProjects);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GradlesProjectsExists(decimal id)
        {
            return db.GradlesProjects.Count(e => e.lead_ID == id) > 0;
        }
    }
}