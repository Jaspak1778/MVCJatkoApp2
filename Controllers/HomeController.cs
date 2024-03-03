using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Mvc;
using MVCJatkoApp2.Models;


namespace MVCJatkoApp2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.LoginError = 0; //Ei pakoteta modaalia login-ruutua tässä kohden, vaan pelkästään, jos on yritetty kirjautua ja kirjautuminen on epäonnistunut
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Tietoja Northwind-yhtiöstä ja Careeriasta";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Yhteystiedot";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Authorize(Logins LoginModel)
        {
            NorthwindEntity db = new NorthwindEntity();
            //Haetaan käyttäjän/Loginin tiedot annetuilla tunnustiedoilla tietokannasta LINQ -kyselyllä
            var LoggedUser = db.Logins.SingleOrDefault(x => x.UserName == LoginModel.UserName && x.PassWord == LoginModel.PassWord);
            if (LoggedUser != null)
            {
                ViewBag.LoginMessage = "Successfull login";
                ViewBag.LoggedStatus = "In";
                ViewBag.LoginError = 0; //Ei virhettä...
                Session["UserName"] = LoggedUser.UserName;
                Session["LoginID"] = LoggedUser.LoginId;
                //Session["AccessLevel"] = LoggedUser.AccessLevel;
                return RedirectToAction("Index", "Home"); //Tässä määritellään mihin onnistunut kirjautuminen johtaa --> Home/Index
            }
            else
            {
                ViewBag.LoginMessage = "Login unsuccessfull";
                ViewBag.LoggedStatus = "Out";
                ViewBag.LoginError = 1; //Pakotetaan modaali login-ruutu uudelleen koska kirjautumisyritys on epäonnistunut
                LoginModel.LoginErrorMessage = "Tuntematon käyttäjätunnus tai salasana.";
                //return View("Login", LoginModel);
                return View("Index", LoginModel);
            }
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            ViewBag.LoggedStatus = "Out";
            return RedirectToAction("Index", "Home"); //Uloskirjautumisen jälkeen pääsivulle
        }

    }

}