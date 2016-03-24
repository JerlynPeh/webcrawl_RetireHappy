using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RetireHappy.Models;
using RetireHappy.DAL;

using System.IO;
using System;
using System.Text.RegularExpressions;
using System.Net;

namespace RetireHappy.Controllers
{
    public class AdminController : Controller
    {
        private RetireHappyContext db = new RetireHappyContext();
        private AvgExpenditureGateway avgExpenditureGateway = new AvgExpenditureGateway();

        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase uploadFile)
        {
            if(uploadFile == null || uploadFile.ContentLength == 0)
            {
                ViewBag.Error = "Please select a excel file";
                return View("Upload");
            }
            else
            {
                if(uploadFile.FileName.EndsWith("xls") || uploadFile.FileName.EndsWith("xlsx"))
                {
                    avgExpenditureGateway.UploadDataset(uploadFile); 
                    ViewBag.SuccessMsg = "Data has been updated";
                }
                else
                {
                    ViewBag.Error = "File type is unsupported";
                    return View("Upload");
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Verify(string adminCode)
        {
            if(adminCode == "retireHappy123")
            {
                ViewBag.admin = "true";
            }
            else
            {
                ViewBag.admin = "false";
                ViewBag.AdminCodeError = "Admin code is incorrect, please contact RetireHappy for further assistance.";
            }
            return View("Upload");
        }

        [HttpPost]
        public ActionResult Sync() {
            string url = "http://www.singstat.gov.sg/publications/household-expenditure-survey";
            string dwlLink = "www.singstat.gov.sg";

            //curren dir and file to save
            //string currDir = System.IO.Directory.GetCurrentDirectory();
            //string fname = currDir + "new1.xls"; 
            Response.Clear();


            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.UserAgent = "A .NET Web Crawler";
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            string htmlText = reader.ReadToEnd();

            // this part does the parsing of the website for the href containing the excel.xls

            Regex regex = new Regex("href\\s*=\\s*(?:\"(?<1>[^\"]*)\"|(?<1>\\S+))", RegexOptions.IgnoreCase);
            Match match;
            int count = 0;
            for (match = regex.Match(htmlText); match.Success; match = match.NextMatch())
            {
                //Console.WriteLine("Found a href. Groups: ");
                foreach (Group group in match.Groups)
                {

                    //Console.WriteLine("Group value: {0}", group);
                    if (group.ToString().Contains("excel.xls"))
                    {
                        if (count ==1 ) {
                            Console.WriteLine("FOUND!: {0}", group);
                            dwlLink = dwlLink+ group.ToString();

                            string newLink = dwlLink.Trim();
                            
                            //webClient.DownloadFile(newLink, @"C:\Users\Hp\Desktop\new.xls");
                            using (WebClient client = new WebClient())
                            {
                                try {
                                    //client.DownloadFile(@newLink, "C:\\Users\\Hp\\Desktop\\new1.xls");


                                    client.DownloadFile(newLink, @"C:\Users\\Hp\Desktop\new1.xls");

                                }
                                catch (Exception a) {
                                    
                                }

                               
                               
                            }

                            Console.ReadLine();
                        }
                        count++;
                            
                    }

                }
            }

            return null;
        }
     

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
