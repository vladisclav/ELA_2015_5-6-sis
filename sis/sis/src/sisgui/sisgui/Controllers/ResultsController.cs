using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace sisgui.Controllers
{
    public class ResultsController : Controller
    {
        // GET: Results
        public ActionResult Index()
        {
            string[] tasksPaths = Directory.GetFiles(@"D:\sisgui\sisgui\uploads\");
            string[] resultsPaths = Directory.GetFiles(@"D:\sisgui\sisgui\results\");
            List<string> results = new List<string>();
            for (int i = 0; i < tasksPaths.Length;i++ )
            {
                for(int j = 0 ; j<resultsPaths.Length;j++)
                    if (Path.GetFileName(tasksPaths.ElementAt(i)).Equals(Path.GetFileName(resultsPaths.ElementAt(j)))) 
                    {
                        results.Add(Path.GetFileName(resultsPaths.ElementAt(j)));
                    }
            }
            ViewBag.res = results;
            return View();
        }
    }
}