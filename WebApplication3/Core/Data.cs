using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace WebApplication3.Core
{
    public static class Data
    {
        public static List<Models.Subject> subjects = new List<Models.Subject>();
        public static void Init()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("D:\\Pasha_local\\Programming\\VKR\\WebApplication3\\Resources\\xml\\02.03.02-17-1234_4k.plm.xml");
            XmlElement xRoot = doc.DocumentElement;

            XmlNodeList childNodeList = xRoot.SelectNodes("//СтрокиПлана//Строка");


            foreach (XmlNode node in childNodeList)
            {
                XmlNode discipline = node.Attributes.GetNamedItem("Дис");
                XmlNode disciplineId = node.Attributes.GetNamedItem("ИдетификаторДисциплины");
                XmlNode competence = node.Attributes.GetNamedItem("Компетенции");
                if (discipline != null)
                {
                    List<string> competenceList = new List<string>();
                    var str =
                        $"Дисциплина: {discipline.Value}, ИД: {disciplineId.Value}";
                    competenceList = competence.Value.Replace(" ", "").Split(',').ToList();

                    subjects.Add(new Models.Subject(discipline.Value, disciplineId.Value, competenceList));
                }

                foreach (XmlNode childNode in node.ChildNodes)
                {
                    XmlNode num = childNode.Attributes.GetNamedItem("Ном");
                    XmlNode lectures = childNode.Attributes.GetNamedItem("Лек");
                    XmlNode practice = childNode.Attributes.GetNamedItem("Пр");
                    XmlNode laboratory = childNode.Attributes.GetNamedItem("Лаб");
                    if (lectures != null || practice != null || laboratory != null)
                    {
                        var temp = "";
                        var sub = subjects.Last();
                        sub.Num.Add(CheckNull(num));
                        sub.Lectures.Add(CheckNull(lectures));
                        sub.Practice.Add(CheckNull(practice));
                        sub.Laboratory.Add(CheckNull(laboratory));
                    }
                }
            }

            childNodeList = xRoot.SelectNodes("//Компетенции/Строка");

            foreach (var sub in subjects)
            {
                foreach (XmlNode node in childNodeList)
                {
                    XmlNode indices = node.Attributes.GetNamedItem("Индекс");
                    if (sub.Competence.Contains(indices.Value))
                    {
                        XmlNode comp = node.Attributes.GetNamedItem("Содержание");
                        string compStr = comp.Value;
                        sub.CompetenceList.Add(compStr);
                    }
                }
            }
        }

        private static string CheckNull(XmlNode node)
        {
            return node == null ? "0" : node.Value;
        }
    }
}