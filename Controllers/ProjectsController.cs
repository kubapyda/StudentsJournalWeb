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
    public class ProjectsController : ApiController
    {
        private JournalWebEntities db = new JournalWebEntities();

        // GET: api/Projects
        public IQueryable GetProjects()
        {
            var projects = from d in db.Projects
                           select new
                           {
                               id = (int)d.project_ID,
                               title = d.project_title,
                               studentNumber = d.project_students_number,
                               leader = d.Leaders.lead_name + " " + d.Leaders.lead_surname,
                               subject = d.Subjects.subject_name
                           };
            return projects;
        }

        // GET: api/Projects/5
        [ResponseType(typeof(Projects))]
        public IHttpActionResult GetProjects(decimal id)
        {
            Projects projects = db.Projects.Find(id);
            if (projects == null)
            {
                return NotFound();
            }

            return Ok(projects);
        }

        // PUT: api/Projects/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProjects(decimal id, Projects projects)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != projects.project_ID)
            {
                return BadRequest();
            }

            Projects proj = new Projects()
            {
                project_ID = projects.project_ID,
                subject_ID = projects.subject_ID,
                lead_ID = projects.lead_ID,
                project_title = projects.project_title,
                project_students_number = projects.project_students_number
            };

            db.Entry(proj).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectsExists(id))
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

        // POST: api/Projects
        [ResponseType(typeof(Projects))]
        public IHttpActionResult PostProjects(Projects projects)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Projects.Add(projects);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = projects.project_ID }, projects);
        }

        // DELETE: api/Projects/5
        [ResponseType(typeof(Projects))]
        public IHttpActionResult DeleteProjects(decimal id)
        {
            Projects projects = db.Projects.Find(id);
            if (projects == null)
            {
                return NotFound();
            }

            db.Projects.Remove(projects);
            db.SaveChanges();

            return Ok(projects);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProjectsExists(decimal id)
        {
            return db.Projects.Count(e => e.project_ID == id) > 0;
        }
    }
}