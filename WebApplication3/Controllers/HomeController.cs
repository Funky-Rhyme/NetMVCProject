using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using System.Web.Mvc;
using Microsoft.Office.Interop.Excel;
using IOFile = System.IO.File;
using Microsoft.Office.Interop.Word;
using WebApplication3.Models;
using Application = Microsoft.Office.Interop.Word.Application;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            var directions = Core.Data.subjects.Select(
                sub => new SelectListItem
                {
                    Text = sub.Name, Value = sub.DisciplineId
                }).ToList();
            ViewBag.Message = "";

            return View(new Models.Form()
            {
                Discipline = "",
                Directions = directions
            });
        }

        [HttpGet]
        public ActionResult GetCoursesData(string directionName)
        {
            var obj = Core.Data.subjects.Find(x => x.Name == directionName);
            ViewBag.courses = obj.Num;
            if (obj.Num != null)
            {
                return PartialView("_courses");
            }
            else
            {
                return null;
            }
        }

        [HttpPost]
        public string GenerateDocument(Models.DocCreateRequest data)
        {
            var b = new Models.Topic();
            var c = new Models.Form();
            var k = new Models.DocCreateRequest();
            var obj = Core.Data.subjects.Find(x => x.Name == data.DirectionName);
            var rootFolderPath = AppDomain.CurrentDomain.BaseDirectory;
            var resultsFolderPath = Path.Combine(rootFolderPath, "Results");
            var resourcesFolderPath = Path.Combine(rootFolderPath, "Resources");
            var oldFilePath = Path.Combine(resourcesFolderPath, "template.docx");
            var newFileName = Path.ChangeExtension(Path.GetRandomFileName(), Path.GetExtension(oldFilePath));
            var newFilePath = Path.Combine(resultsFolderPath, newFileName);
            IOFile.Copy(oldFilePath, newFilePath, true);

            object fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, newFilePath);
            var wordApp = new Application {Visible = false};
            //wordApp.Documents.Add();
            var doc = wordApp.Documents.Open(fileName, ReadOnly: false, Visible: true);
            //wordApp.Activate();
            FindAndReplace(wordApp, "<disciplineId>", obj.DisciplineId);
            FindAndReplace(wordApp, "<discipline>", obj.Name);

            var rowMerge = 5;
            var rowText = 5;
            var competence = obj.Competence[0];
            var compList = obj.CompetenceList[0];
            var a = doc.Tables[3];
            a.Cell(2, 1).Range.Text = competence + "\n" + compList;

            for (var i = 1; i < obj.Competence.Count; i++)
            {
                doc.Tables[3].Rows.Add();
                doc.Tables[3].Rows.Add();
                doc.Tables[3].Rows.Add();
                competence = obj.Competence[i];
                compList = obj.CompetenceList[i];
                a.Cell(rowText, 1).Range.Text = competence + "\n" + compList;
                a.Cell(rowMerge, 1).Merge(a.Cell(rowMerge + 2, 1));
                a.Cell(rowMerge, 3).Merge(a.Cell(rowMerge + 2, 3));
                rowText += 3;
                rowMerge += 3;
            }

            var labHours = obj.Laboratory.Sum(x => Convert.ToInt32(x));
            var lecHours = obj.Lectures.Sum(x => Convert.ToInt32(x));
            var pracHours = obj.Practice.Sum(x => Convert.ToInt32(x));
            var hoursSum = pracHours + lecHours + labHours;
            FindAndReplace(wordApp, "<sum>", hoursSum.ToString());
            FindAndReplace(wordApp, "<lecsum>", lecHours.ToString());
            FindAndReplace(wordApp, "<pracsum>", pracHours);

            





            doc.Close();
            

            void FindAndReplace(Application docX, object fnText, object replaceWithText)
            {
                //options
                object matchCase = false;
                object matchWholeWord = true;
                object matchWildCards = false;
                object matchSoundsLike = false;
                object matchAllWordForms = false;
                object forward = true;
                object format = false;
                object matchKashida = false;
                object matchDiacritics = false;
                object matchAlefHamza = false;
                object matchControl = false;
                object read_only = false;
                object visible = true;
                object replace = 2;
                object wrap = 1;
                //execute find and replace
                docX.Selection.Find.Execute(ref fnText, ref matchCase, ref matchWholeWord,
                    ref matchWildCards, ref matchSoundsLike, ref matchAllWordForms, ref forward, ref wrap, ref format, ref replaceWithText, ref replace,
                    ref matchKashida, ref matchDiacritics, ref matchAlefHamza, ref matchControl);
            }

            return newFileName;
        }

        public FileResult Download(string fileName)
        {
            byte[] fileBytes = IOFile.ReadAllBytes(Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Results"), fileName));
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


    }
}