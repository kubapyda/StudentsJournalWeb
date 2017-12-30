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
    public class AdministratorsController : ApiController
    {
        private JournalWebEntities db = new JournalWebEntities();

        // GET: api/Administrators
        public IQueryable<Administrators> GetAdministrators()
        {
            return db.Administrators;
        }

        // GET: api/Administrators/5
        [ResponseType(typeof(Administrators))]
        public IHttpActionResult GetAdministrators(decimal id)
        {
            Administrators administrators = db.Administrators.Find(id);
            if (administrators == null)
            {
                return NotFound();
            }

            return Ok(administrators);
        }

        // PUT: api/Administrators/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAdministrators(decimal id, Administrators administrators)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != administrators.admin_ID)
            {
                return BadRequest();
            }

            db.Entry(administrators).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdministratorsExists(id))
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

        // POST: api/Administrators
        [ResponseType(typeof(Administrators))]
        public IHttpActionResult PostAdministrators(Administrators administrators)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Administrators.Add(administrators);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = administrators.admin_ID }, administrators);
        }

        // DELETE: api/Administrators/5
        [ResponseType(typeof(Administrators))]
        public IHttpActionResult DeleteAdministrators(decimal id)
        {
            Administrators administrators = db.Administrators.Find(id);
            if (administrators == null)
            {
                return NotFound();
            }

            db.Administrators.Remove(administrators);
            db.SaveChanges();

            return Ok(administrators);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AdministratorsExists(decimal id)
        {
            return db.Administrators.Count(e => e.admin_ID == id) > 0;
        }
    }
}