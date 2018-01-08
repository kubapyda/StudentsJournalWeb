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
    [Authorize(Roles = "Administrator")]
    public class DeanGroupsController : ApiController
    {
        private JournalWebEntities db = new JournalWebEntities();

        // GET: api/DeanGroups
        public IQueryable GetDeanGroup()
        {
            var deanGroups = from d in db.DeanGroup
                             select new
                             {
                                 id = (int)d.group_id,
                                 name = d.group_name,
                                 departmentName = d.Department.department_name,
                                 directionName = d.Department.Direction.direction_name
                             };
            return deanGroups;
        }

        // GET: api/DeanGroups/5
        [ResponseType(typeof(DeanGroup))]
        public IHttpActionResult GetDeanGroup(decimal id)
        {
            DeanGroup deanGroup = db.DeanGroup.Find(id);
            if (deanGroup == null)
            {
                return NotFound();
            }

            return Ok(deanGroup);
        }

        // PUT: api/DeanGroups/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDeanGroup(decimal id, DeanGroup deanGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != deanGroup.group_id)
            {
                return BadRequest();
            }

            db.Entry(deanGroup).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeanGroupExists(id))
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

        // POST: api/DeanGroups
        [ResponseType(typeof(DeanGroup))]
        public IHttpActionResult PostDeanGroup(DeanGroup deanGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DeanGroup.Add(deanGroup);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = deanGroup.group_id }, deanGroup);
        }

        // DELETE: api/DeanGroups/5
        [ResponseType(typeof(DeanGroup))]
        public IHttpActionResult DeleteDeanGroup(decimal id)
        {
            DeanGroup deanGroup = db.DeanGroup.Find(id);
            if (deanGroup == null)
            {
                return NotFound();
            }

            db.DeanGroup.Remove(deanGroup);
            db.SaveChanges();

            return Ok(deanGroup);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DeanGroupExists(decimal id)
        {
            return db.DeanGroup.Count(e => e.group_id == id) > 0;
        }
    }
}