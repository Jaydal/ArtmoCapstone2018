using ArtmoWeb.WebApplication.Models;
using System;
using System.Collections.Generic;
using System.Data;  
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace ArtmoWeb.WebApplication.Controllers
{
    [Authorize]
    public class ArtmoController : Controller
    {
        // GET: Artmo
        ArtmoModels model = new ArtmoModels();
        readonly string path = "~/_Storage/artmo.xml";
        public ActionResult Index(string searchString,string SortBy)
        {
            List<ArtmoModels> Artmo = new List<ArtmoModels>();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath(path));
            DataView dv;
            dv = ds.Tables[0].DefaultView;
            if(SortBy=="DateAdded" || SortBy == "LastModified")
            {
                dv.Sort = SortBy+" DESC";
            }
            else
            {
                dv.Sort = SortBy;
            }
         
            int ctr = 0;

            if (!String.IsNullOrEmpty(searchString))
            {
                searchString.ToString().ToLower();
                foreach (DataRowView dr in dv)
                {
                    ArtmoModels model = new ArtmoModels();
                    if (Convert.ToString(dr[3]).ToLower().Contains(searchString) || Convert.ToString(dr[4]).ToString().ToLower().Contains(searchString)
                      || Convert.ToString(dr[5]).ToLower().Contains(searchString) || Convert.ToString(dr[6]).ToLower().Contains(searchString)
                       || Convert.ToString(dr[7]).ToLower().Contains(searchString) || Convert.ToString(dr[8]).ToLower().Contains(searchString))
                    {
                        model.Id = Convert.ToInt32(dr[0]);
                        model.Marker = Convert.ToInt32(dr[1]);
                        var b64 = dr[2];
                        model._img64 = b64.ToString();
                        model.Name = Convert.ToString(dr[3]);
                        model.GenericTerm = Convert.ToString(dr[4]);
                        model.Donor = Convert.ToString(dr[5]);
                        model.EngDesc = Convert.ToString(dr[6]);
                        model.IlocoDesc = Convert.ToString(dr[7]);
                        model.DateAcquired = Convert.ToDateTime(dr[8]);
                        model.DateAcquired.ToString("yyyy/MM/dd");
                        model.DateAdded = Convert.ToString(dr[9]);
                        model.LastModified = Convert.ToString(dr[10]);
                        Artmo.Add(model);
                        ctr++;
                    }
                }
                return View("Index", Artmo);
            }
            else
            {
                ctr = 1;
                foreach (DataRowView dr in dv)
                {
                    ArtmoModels model = new ArtmoModels();
                    model.Id = Convert.ToInt32(dr[0]);
                    model.Marker = Convert.ToInt32(dr[1]);
                    var b64 = dr[2];
                    model._img64 = b64.ToString();
                    model.Name = Convert.ToString(dr[3]);
                    model.GenericTerm = Convert.ToString(dr[4]);
                    model.Donor = Convert.ToString(dr[5]);
                    model.EngDesc = Convert.ToString(dr[6]);
                    model.IlocoDesc = Convert.ToString(dr[7]);
                    model.DateAcquired = Convert.ToDateTime(dr[8]);
                    model.DateAcquired.ToString("yyyy/MM/dd");
                    model.DateAdded = Convert.ToString(dr[9]);
                    model.LastModified = Convert.ToString(dr[10]);
                    Artmo.Add(model);
                }
            }
            return View(Artmo);
        }
        public ActionResult _Index(string searchString, string SortBy)
        {
            List<ArtmoModels> Artmo = new List<ArtmoModels>();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath(path));
            DataView dv;
            dv = ds.Tables[0].DefaultView;
            dv.Sort = SortBy;
            int ctr = 0;
            foreach (DataRowView dr in dv)
            {
                ArtmoModels model = new ArtmoModels();
                if (Convert.ToString(dr[3]).ToLower().Contains(searchString) || Convert.ToString(dr[4]).ToString().ToLower().Contains(searchString)
                      || Convert.ToString(dr[5]).ToLower().Contains(searchString) || Convert.ToString(dr[6]).ToLower().Contains(searchString)
                       || Convert.ToString(dr[7]).ToLower().Contains(searchString) || Convert.ToString(dr[8]).ToLower().Contains(searchString))
                {
                    model.Id = Convert.ToInt32(dr[0]);
                    model.Marker = Convert.ToInt32(dr[1]);
                    var b64 = dr[2];
                    model._img64 = b64.ToString();
                    model.Name = Convert.ToString(dr[3]);
                    model.GenericTerm = Convert.ToString(dr[4]);
                    model.Donor = Convert.ToString(dr[5]);
                    model.EngDesc = Convert.ToString(dr[6]);
                    model.IlocoDesc = Convert.ToString(dr[7]);
                    model.DateAcquired = Convert.ToDateTime(dr[8]);
                    model.DateAcquired.ToShortDateString();
                    model.DateAdded = Convert.ToString(dr[9]);
                    model.LastModified = Convert.ToString(dr[10]);
                    Artmo.Add(model);
                    ctr++;
                }
            }
            return PartialView(Artmo);
        }
        [HttpGet]
        public ActionResult Items(string marker)
        {
            ViewBag.marker = marker;
            List<ArtmoModels> Artmo = new List<ArtmoModels>();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath(path));
            DataView dv;
            dv = ds.Tables[0].DefaultView;
            dv.Sort = "Name";
            foreach (DataRowView dr in dv)
            {
                ArtmoModels model = new ArtmoModels();
                if (Convert.ToString(dr[1]).Equals(marker))
                {
                    model.Id = Convert.ToInt32(dr[0]);
                    model.Marker = Convert.ToInt32(dr[1]);
                    model.Name = Convert.ToString(dr[3]);
                    model.GenericTerm = Convert.ToString(dr[4]);
                    model.Donor = Convert.ToString(dr[5]);
                    model.DateAcquired = Convert.ToDateTime(dr[8]);
                    model.DateAcquired.ToShortDateString();
                    Artmo.Add(model);
                }
            }
            return PartialView("Items", Artmo);
        }
        public ActionResult Marker()
        {
            List<string> markers = new List<string>();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath(path));
            DataView dv;
            dv = ds.Tables[0].DefaultView;
            foreach (DataRowView dr in dv)
            {
                if (dr[1].ToString().Length == 1)
                {
                    markers.Add("00"+dr[1].ToString());
                }
                else if (dr[1].ToString().Length == 2)
                {
                    markers.Add("0" + dr[1].ToString());
                }
                else
                {
                    markers.Add(dr[1].ToString());
                }
            }
            ViewBag.marker = markers;
            return View();
        }
        public ActionResult _Details(int? id)
        {
            GetDetails(id);
            ViewBag.Image = string.Format("data:image/gif;base64,{0}", model._img64);
            return PartialView(model);
        }
        public ActionResult AddEdit(int? id, string marker)
        {
            if (id != null)
            {
                GetDetails(id);
                ViewBag.Image = string.Format("data:image/gif;base64,{0}", model._img64);
                model.IsEdit = true;
            }
            else
            {
                ViewBag.marker = marker;
                model.IsEdit = false;
            }
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult AddEdit(ArtmoModels md, HttpPostedFileBase imgf)
        {
            if (imgf != null)
            {
                md.Image = new byte[imgf.ContentLength];
                imgf.InputStream.Read(md.Image, 0, imgf.ContentLength);
                md._img64 = Convert.ToBase64String(md.Image);
            }
            if (md.Id > 0)
            {
                XDocument xmlDoc = XDocument.Load(Server.MapPath(path));
                var items = (from item in xmlDoc.Descendants("Artifact")
                             where item.Element("id").Value == md.Id.ToString()
                             select item).ToList();
                foreach (var item in items)
                {
                    item.Element("id").Value = md.Id.ToString();
                    item.Element("marker").Value = md.Marker.ToString();
                    item.Element("Image").Value = md._img64 ?? "";
                    item.Element("Name").Value = md.Name;
                    item.Element("GTerm").Value = md.GenericTerm ?? "";
                    item.Element("Donor").Value = md.Donor ?? "";
                    item.Element("EngDesc").Value = md.EngDesc ?? "";
                    item.Element("IlocoDesc").Value = md.IlocoDesc ?? "";
                    item.Element("DateAcquired").Value = md.DateAcquired.ToString("yyyy/MM/dd") ?? "";
                    item.Element("LastModified").Value = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Taipei Standard Time").ToString("yyyy/MM/dd") ?? "";
                }
                xmlDoc.Save(Server.MapPath(path));
                GetDetails(md.Id);
            }
            else
            {
                Console.WriteLine("1");
                XmlDocument oXmlDocument = new XmlDocument();
                oXmlDocument.Load(Server.MapPath(path));
                XmlNodeList nodeList = oXmlDocument.GetElementsByTagName("Artifact");
                var x = oXmlDocument.GetElementsByTagName("id");
                int Max = 0;
                foreach (XmlElement item in x)
                {
                    int EId = Convert.ToInt32(item.InnerText.ToString());
                    if (EId > Max)
                    {
                        Max = EId;
                    }
                }
                Max = Max + 1;
                XDocument xmlDoc = XDocument.Load(Server.MapPath(path));
                xmlDoc.Element("Artifacts").Add(new XElement("Artifact",
                   new XElement("id", Max),
                   new XElement("marker", md.Marker),
                    new XElement("Image", md._img64),
                     new XElement("Name", md.Name),
                      new XElement("GTerm", md.GenericTerm),
                       new XElement("Donor", md.Donor),
                        new XElement("EngDesc", md.EngDesc),
                            new XElement("IlocoDesc", md.IlocoDesc),
                               new XElement("DateAcquired", md.DateAcquired.ToString("yyyy/MM/dd")),
                                 new XElement("DateAdded", TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Taipei Standard Time").ToString("yyyy/MM/dd")),
                                    new XElement("LastModified", "")
                   ));
                xmlDoc.Save(Server.MapPath(path));
                GetDetails(Max);
            }
            ViewBag.Image = string.Format("data:image/gif;base64,{0}", model._img64);
            return PartialView("_Details", model);
        }
         
        public void GetDetails(int? id)
        {
            XDocument xDocument = XDocument.Load(Server.MapPath(path));
            var items = (from item in xDocument.Descendants("Artifact")
                         where Convert.ToInt32(item.Element("id").Value) == id
                         select new ArtmoItems
                         {
                             Id = Convert.ToInt32(item.Element("id").Value),
                             Marker = Convert.ToInt32(item.Element("marker").Value),
                             _img64 = Convert.ToString(item.Element("Image").Value),
                             Name = Convert.ToString(item.Element("Name").Value),
                             GenericTerm = Convert.ToString(item.Element("GTerm").Value),
                             Donor = Convert.ToString(item.Element("Donor").Value),
                             EngDesc = Convert.ToString(item.Element("EngDesc").Value),
                             IlocoDesc = Convert.ToString(item.Element("IlocoDesc").Value),
                             DateAcquired = Convert.ToDateTime(item.Element("DateAcquired").Value),
                             DateAdded = Convert.ToString(item.Element("DateAdded").Value),
                             LastModified = Convert.ToString(item.Element("LastModified").Value),
                         }).SingleOrDefault();
            if (items != null)
            {
                model.Id = items.Id;
                model.Marker = items.Marker;
                model._img64 = items._img64;
                model.Name = items.Name;
                model.GenericTerm = items.GenericTerm;
                model.Donor = items.Donor;
                model.EngDesc = items.EngDesc;
                model.IlocoDesc = items.IlocoDesc;
                model.DateAcquired = items.DateAcquired;
                model.DateAdded = items.DateAdded;
                model.LastModified = items.LastModified;
            }
        }
        public class ArtmoItems
        {
            public int Id { get; set; }
            public int Marker { get; set; }
            public byte[] Image { get; set; }
            public string Name { get; set; }
            public string GenericTerm { get; set; }
            public string Donor { get; set; }
            public string EngDesc { get; set; }
            public string IlocoDesc { get; set; }
            public DateTime DateAcquired { get; set; }
            public string DateAdded { get; set; }
            public string LastModified { get; set; }
            public string _img64 { get; set; }
            public ArtmoItems() { }
        }
        [AllowAnonymous]
        public ActionResult DownloadFile()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "_Storage/";
            byte[] fileBytes = System.IO.File.ReadAllBytes(path + "artmo.xml");
            string fileName = "artmo.xml";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
        private string FDir_AppData = "~/_Storage/";
        [AllowAnonymous]
        public FileResult Download()
        {
            var sDocument = Server.MapPath(FDir_AppData + "artmo.apk");
            return File(sDocument, "application/vnd.android.package-archive", "artmo.apk");
        }
        public ActionResult LogFile()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "_Storage/";
            byte[] fileBytes = System.IO.File.ReadAllBytes(path + "log");
            string fileName = "artmo.log";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
        public ActionResult RestoreFile()
        {
            return PartialView("RestoreFile");  
        }
        public ActionResult _RestoreFile(string filename)
        {
            string bak = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Taipei Standard Time").ToString("MM-dd-yyyy");
            System.IO.File.Replace( Server.MapPath("~/_Storage/Backup/" + filename), Server.MapPath(path),null);
            System.IO.File.Copy(Server.MapPath(path), Server.MapPath("~/_Storage/Backup/" + filename));
            insertLog("Backup restore " + filename);
            return RedirectToAction("Index");
        }
        public ActionResult DeleteFile(string filename)
        {
            System.IO.File.Delete(Server.MapPath("~/_Storage/Backup/"+filename));
            insertLog("Backup delete " + filename);
            return PartialView("RestoreFile");
        }
        public ActionResult DeleteItem(int? id)
        {
            if (id > 0)
            {
                GetDetails(id);
                var name = model.Name + "/" + model.GenericTerm;
                XDocument xmlDoc = XDocument.Load(Server.MapPath(path));
                var items = (from item in xmlDoc.Descendants("Artifact") select item).ToList();
                XElement selected = items.Where(a => a.Element("id").Value == id.ToString()).FirstOrDefault();
                selected.Remove();
                xmlDoc.Save(Server.MapPath(path));
                insertLog("Removed Item "+ name);
            }
            return RedirectToAction("Index");
        }
        public ActionResult BackupFile()
        {
            var c = Count();
            string filename = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Taipei Standard Time").ToString("MM-dd-yyyy");
            if (System.IO.File.Exists(Server.MapPath("~/_Storage/Backup/artmo" + filename + "(" + c + ").xml")))
            {
                c++;
            }
            System.IO.File.Copy(Server.MapPath(path), Server.MapPath("~/_Storage/Backup/artmo"+filename+"("+c+").xml"));
            insertLog("Data backup "+filename);
            return RedirectToAction("Index");
        }
        private int Count()
        {
            DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/_Storage/Backup/"));
            int filecount = 0;
            foreach (FileInfo f in dir.GetFiles("*.*"))
            {
               filecount++;
            }
            return filecount;
        }
        private void insertLog(string log)
        {
            using (StreamWriter w = System.IO.File.AppendText(Server.MapPath("~/_Storage/log")))
            {
                Log(log,w);
            }
        }
        public static void Log(string logMessage, TextWriter w)
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine("{0} {1}", TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Taipei Standard Time").ToLongTimeString(),
                TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Taipei Standard Time").ToLongDateString());
            w.WriteLine("  :");
            w.WriteLine("  :{0}", logMessage);
            w.WriteLine("-------------------------------");
        }

    }
}