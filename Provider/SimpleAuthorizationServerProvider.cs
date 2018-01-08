using Microsoft.Owin.Security;  
using Microsoft.Owin.Security.OAuth;  
using System.Collections.Generic;  
using System.Linq;  
using System.Security.Claims;  
using System.Threading.Tasks;  
using System.Web.Http.Cors;
using StudentsJournalWeb.Models;

namespace StudentsJournalWeb.Provider
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated(); //   
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            using (var db = new JournalWebEntities())
            {
                if (db != null)
                {
                    //var admin = db.Administrators.ToList();
                    var user = db.Users.ToList();
                    var userRole = user.Where(u => u.user_login == context.UserName && u.user_password == context.Password).FirstOrDefault().user_role;
                    string roles = "";

                    if (userRole.Equals("admin"))
                    {
                        roles = "Administrator";
                    }
                    else if (userRole.Equals("user"))
                    {
                        roles = "User";
                    }
                    else if (userRole.Equals("lead"))
                    {
                        roles = "Leader";
                    }

                    if (user != null && !roles.Equals(""))
                    {
                        if (!string.IsNullOrEmpty(userRole))
                        {
                            identity.AddClaim(new Claim(ClaimTypes.Role, roles));

                            var props = new AuthenticationProperties(new Dictionary<string, string>
                            {
                                {
                                    "userdisplayname", context.UserName
                                },
                                {
                                     "role", roles
                                }
                             });

                            var ticket = new AuthenticationTicket(identity, props);
                            context.Validated(ticket);
                        }
                        else
                        {
                            context.SetError("invalid_grant", "Provided username and password is incorrect");
                            context.Rejected();
                        }
                    }
                }
                else
                {
                    context.SetError("invalid_grant", "Provided username and password is incorrect");
                    context.Rejected();
                }
                return;
            }
        }
    }
}