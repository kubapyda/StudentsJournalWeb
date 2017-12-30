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
    public class LeadersController : ApiController
    {
        private JournalWebEntities db = new JournalWebEntities();

        // GET: api/Leaders
        public IQueryable<Leaders> GetLeaders()
        {
            return db.Leaders;
        }

        // GET: api/Leaders/5
        [ResponseType(typeof(Leaders))]
        public IHttpActionResult GetLeaders(decimal id)
        {
            Leaders leaders = db.Leaders.Find(id);
            if (leaders == null)
            {
                return NotFound();
            }

            return Ok(leaders);
        }

        // PUT: api/Leaders/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLeaders(decimal id, Leaders leaders)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != leaders.lead_ID)
            {
                return BadRequest();
            }

            db.Entry(leaders).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeadersExists(id))
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

        // POST: api/Leaders
        [ResponseType(typeof(Leaders))]
        public IHttpActionResult PostLeaders(Leaders leaders)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Leaders.Add(leaders);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = leaders.lead_ID }, leaders);
        }

        // DELETE: api/Leaders/5
        [ResponseType(typeof(Leaders))]
        public IHttpActionResult DeleteLeaders(decimal id)
        {
            Leaders leaders = db.Leaders.Find(id);
            if (leaders == null)
            {
                return NotFound();
            }

            db.Leaders.Remove(leaders);
            db.SaveChanges();

            return Ok(leaders);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LeadersExists(decimal id)
        {
            return db.Leaders.Count(e => e.lead_ID == id) > 0;
        }
    }
}