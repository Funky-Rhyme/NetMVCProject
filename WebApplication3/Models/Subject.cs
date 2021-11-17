using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace WebApplication3.Models
{
    public class Subject
    {
        private string _name;
        private string _disciplineId;
        public List<string> Competence { get; set; }
        public List<string> CompetenceList = new List<string>();

        public readonly List<string> Num = new List<string>();
        public readonly List<string> Lectures = new List<string>();
        public readonly List<string> Practice = new List<string>();
        public readonly List<string> Laboratory = new List<string>();

        public string Name
        {
            get => _name;
            set => _name = Name;
        }

        public string DisciplineId
        {
            get => _disciplineId;
            set => _disciplineId = DisciplineId;
        }

        public Subject() { }

        public Subject(string name, string disciplineId, List<string> comp)
        {
            _name = name;
            _disciplineId= disciplineId;
            Competence = comp;
        }

        public Subject(string name, string disciplineId, List<string> comp, List<string> compList)
        {
            _name = name;
            _disciplineId= disciplineId;
            Competence = comp;
            CompetenceList = compList;
        }
    }
}