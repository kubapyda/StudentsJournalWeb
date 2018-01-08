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
    public class LoginController : ApiController
    {
        private JournalWebEntities db = new JournalWebEntities();
        
        // GET: api/Login
        public IHttpActionResult GetLogin(string username, string password)
        {
            if (username != null  && password != null)
            {
                var admin = from d in db.Administrators
                            where d.admin_login == username && d.admin_password == password
                            select new
                            {
                                id = d.admin_ID,
                                name = d.admin_name + " " + d.admin_surname,
                                role = "ADMINISTRATOR"
                            };

                var user = from d in db.Students
                            where d.student_login == username && d.student_password == password
                            select new
                            {
                                id = d.student_ID,
                                name = d.student_name + " " + d.student_surname,
                                role = "USER"
                            };

                var leader = from d in db.Leaders
                            where d.lead_login == username && d.lead_password == password
                            select new
                            {
                                id = d.lead_ID,
                                name = d.lead_name + " " + d.lead_surname,
                                role = "LEADER"
                            };
                if ( admin.Any() )
                {
                    return Ok(admin);
                }
                else if (user.Any())
                {
                    return Ok(user);
                }
                else if (leader.Any())
                {
                    return Ok(leader);
                }
                return NotFound();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
