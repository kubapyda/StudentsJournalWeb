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
    public class DirectionsController : ApiController
    {
        private JournalWebEntities db = new JournalWebEntities();

        // GET: api/Directions
        public IQueryable GetDirection()
        {
            var directions = from d in db.Direction
                             select new
                             {
                                 id = (int)d.direction_id,
                                 name = d.direction_name
                             };
            return directions;
        }

        // GET: api/Directions/5
        [ResponseType(typeof(Direction))]
        public IHttpActionResult GetDirection(decimal id)
        {
            Direction direction = db.Direction.Find(id);
            if (direction == null)
            {
                return NotFound();
            }

            return Ok(direction);
        }

        // PUT: api/Directions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDirection(decimal id, Direction direction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != direction.direction_id)
            {
                return BadRequest();
            }

            db.Entry(direction).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DirectionExists(id))
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

        // POST: api/Directions
        [ResponseType(typeof(Direction))]
        public IHttpActionResult PostDirection(Direction direction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Direction.Add(direction);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = direction.direction_id }, direction);
        }

        // DELETE: api/Directions/5
        [ResponseType(typeof(Direction))]
        public IHttpActionResult DeleteDirection(decimal id)
        {
            Direction direction = db.Direction.Find(id);
            if (direction == null)
            {
                return NotFound();
            }

            db.Direction.Remove(direction);
            db.SaveChanges();

            return Ok(direction);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DirectionExists(decimal id)
        {
            return db.Direction.Count(e => e.direction_id == id) > 0;
        }
    }
}