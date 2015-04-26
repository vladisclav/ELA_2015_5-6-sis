using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Messaging;
using System.Diagnostics;


namespace sisgui.Controllers
{
    public class HomeController : Controller
    {
        // This action renders the form
        public ActionResult Index()
        {
            return View();
        }

        // This action handles the form POST and the upload
        [HttpPost]
        public ActionResult Process(HttpPostedFileBase file, string height, string width, string degrees, string blackwhite)
        {
            // Verify that the user selected a file
            if (file != null && file.ContentLength > 0)
            {
                Task t = new Task();
                t.height = height;
                t.width = width;
                t.degrees = degrees;
                t.blackwhite = blackwhite;
                Random r = new Random();
                var fileName = (Stopwatch.GetTimestamp() + r.Next()).ToString() + Path.GetExtension(file.FileName);
             
                var path = Path.Combine(Server.MapPath("/uploads"), fileName);
                file.SaveAs(path);
                t.path = path;
                t.name = fileName;
                Message m = new Message(t);
                m.Formatter = new XmlMessageFormatter(new Type[] { typeof(Task) });
                MessageQueue messageQueue = null;
                if (!MessageQueue.Exists(@".\Private$\Images"))
                    MessageQueue.Create(@".\Private$\Images");
                using (messageQueue = new MessageQueue(@".\Private$\Images"))
                {
                    messageQueue.Label = "Images";

                    messageQueue.Send(m);
                }
            }

            return RedirectToAction("Index", "Results/Index");
        }
    }
}