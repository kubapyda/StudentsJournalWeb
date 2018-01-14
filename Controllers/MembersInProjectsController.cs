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
    public class MembersInProjectsController : ApiController
    {
        private JournalWebEntities db = new JournalWebEntities();

        // GET: api/MembersInProjects
        public IQueryable<MembersInProject> GetMembersInProject()
        {
            return db.MembersInProject;
        }

        // GET: api/MembersInProjects/5
        [ResponseType(typeof(MembersInProject))]
        public IHttpActionResult GetMembersInProject(decimal id, string isLeader)
        {
            var leadProjects = from d in db.MembersInProject
                           where d.lead_ID == id
                           select new
                           {
                               project = d.Projects.project_title,
                               projectId = d.project_ID,
                               student = d.Students.student_name + " " + d.Students.student_surname,
                               studentId = d.student_ID,
                               leadId = d.lead_ID,
                               approved = d.members_approved
                           };

            var projects = from d in db.MembersInProject
                           where d.student_ID == id
                           select new
                           {
                               project = d.Projects.project_title,
                               leader = d.Leaders.lead_name + " " + d.Leaders.lead_surname,
                               approved = d.members_approved
                           };

            if (isLeader.Equals("true"))
            {
                if (projects == null)
                {
                    return NotFound();
                }

                return Ok(projects);
            }
            else
            {
                if (projects == null)
                {
                    return NotFound();
                }

                return Ok(leadProjects);
            }
            
        }

        // PUT: api/MembersInProjects/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMembersInProject(MembersInProject membersInProject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entry(membersInProject).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/MembersInProjects
        [ResponseType(typeof(MembersInProject))]
        public IHttpActionResult PostMembersInProject(MembersInProject membersInProject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MembersInProject.Add(membersInProject);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (MembersInProjectExists(membersInProject.lead_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = membersInProject.lead_ID }, membersInProject);
        }

        // DELETE: api/MembersInProjects/5
        [ResponseType(typeof(MembersInProject))]
        public IHttpActionResult DeleteMembersInProject(decimal id)
        {
            MembersInProject membersInProject = db.MembersInProject.Find(id);
            if (membersInProject == null)
            {
                return NotFound();
            }

            db.MembersInProject.Remove(membersInProject);
            db.SaveChanges();

            return Ok(membersInProject);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MembersInProjectExists(decimal id)
        {
            return db.MembersInProject.Count(e => e.lead_ID == id) > 0;
        }
    }
}