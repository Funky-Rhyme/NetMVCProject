using System.Collections.Generic;
using System.Web.Mvc;

namespace WebApplication3.Models
{
  public class DocCreateRequest
  {
    public string DirectionCode { get; set; }
    public string DirectionName { get; set; }
    public string DisciplineName { get; set; }
    public List<List<Course>> Courses { get; set; }
  }
}