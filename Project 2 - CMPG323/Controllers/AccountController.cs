using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_2___CMPG323.Models;
using System.Net.Mail;
using System.Web.Security;

namespace Project_2___CMPG323.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        //Registration post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(UserDetail user)
		{
            bool Status = false;
            string message = String.Empty;

            //Model validation
            if(ModelState.IsValid)
			{
                //Email already exist
                var isExist = IsEmailExist(user.Email);
                if(isExist)
				{
                    ModelState.AddModelError("Email", "Email already exist");
                    return View(user);
				}
                //Password hashing
                user.Password = Crypto.Hash(user.Password);
                user.ConfirmPassword = Crypto.Hash(user.ConfirmPassword);



                using (AlbumEntities1 DB = new AlbumEntities1())
                {
                    DB.UserDetails.Add(user);
                    DB.SaveChanges();

                    message = "Account successfully added!";
                    Status = true;
                }

            }
            else
			{
                message = "Invalid request!";
			}


            return View(user);
		}


        [NonAction]
        public Boolean IsEmailExist(string email)
		{
            using(AlbumEntities1 DB = new AlbumEntities1())
			{
                var tmp = DB.UserDetails.Where(a => a.Email == email).FirstOrDefault();
                return tmp == null ? false : true; 
			}
		}

        //Login

        [HttpGet]
        public ActionResult Login()
		{
            return View();
		}
        [HttpPost]
        public ActionResult Login(UserLogin log, string returnUrl)
		{
            string msg = String.Empty;
            using(AlbumEntities1 DB = new AlbumEntities1())
			{
                var tmp = DB.UserDetails.Where(a => a.Email == log.Email).FirstOrDefault();
                if(tmp==null)
				{
                    msg = "Invalid email";
				}
                else
				{
                    if(string.Compare(Crypto.Hash(log.Password), tmp.Password) != 0)
					{
                        msg = "Invalid Password";
                    }
                    else
					{
                        int timeout = log.RememberMe ? 525000 : 10;
                        var tckt = new FormsAuthenticationTicket(log.Email, log.RememberMe, timeout);
                        string encrypted = FormsAuthentication.Encrypt(tckt);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        cookie.Expires = DateTime.Now.AddMinutes(timeout);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);

                        if (Url.IsLocalUrl(returnUrl))
                            return Redirect(returnUrl);
                        else
                            return RedirectToAction("Index", "Home");
					}
				}
			}

            ViewBag.Message = msg;
            return View();
		}
        
        //Logout
        [Authorize]
        public ActionResult Logout()
		{
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
		}
    }

}