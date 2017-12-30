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
            var selectize = from d in db.Leaders
                            select new
                            {
                                id = (int)d.lead_ID,
                                name = d.lead_name + " " + d.lead_surname
                            };
            return selectize;
        }
    }
}