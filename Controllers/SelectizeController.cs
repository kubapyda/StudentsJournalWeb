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
    public class SelectizeController : ApiController
    {
        private JournalWebEntities db = new JournalWebEntities();

        // GET: api/Selectize
        public IQueryable GetLeaders()
        {
           var subjectLeaders = from d in db.Leaders
                                select new
                                {
                                    id = (int)d.lead_ID,
                                    name = d.lead_name + " " + d.lead_surname
                                };
            return subjectLeaders;
        }

        // GET: api/Selectize/5
        public IHttpActionResult GetLeaderSubjects(decimal id)
        {
            var subject = from d in db.Subjects
                          where id == d.lead_ID
                          select new
                          {
                              id = (int)d.subject_ID,
                              name = d.subject_name
                          };
            if (subject == null)
            {
                return NotFound();
            }

            return Ok(subject);
        }
    }
}