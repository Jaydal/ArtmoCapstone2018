using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtmoWeb.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "ARTMO.";

            return View();
        }
        //public ActionResult DownloadArtmo()
        //{
        //    //string path = AppDomain.CurrentDomain.BaseDirectory + "_Storage/";
        //    byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath("~/_Storage/artmo.apk"));
        //    string fileName = "artmo.apk";
        //    return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        //}
    }
}