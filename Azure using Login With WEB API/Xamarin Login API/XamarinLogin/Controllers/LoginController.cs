using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XamarinLogin.Models;

namespace XamarinLogin.Controllers
{
    public class LoginController : ApiController
    {
        xamarinloginEntities db = new xamarinloginEntities();

        [HttpPost]
        [ActionName("XAMARIN_REG")]
       // POST: api/Login
        public HttpResponseMessage Xamarin_reg(Login log)
        {
            try
            {
                Login login = new Login();
                login.Username = log.Username;
                login.Password = log.Password;
                db.Logins.Add(login);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Accepted, "Successfully Created");
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.ToString());
            }
           
        }

       
        [HttpGet]
        [ActionName("XAMARIN_Login")]
        // GET: api/Login/5
        public HttpResponseMessage Xamarin_Login(string username,string password)
        {
            var user = db.Logins.Where(x => x.Username == username && x.Password == password).FirstOrDefault();
            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Please Enter valid UserName and Password");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Accepted, "Success");
            }
        }

    }
}
