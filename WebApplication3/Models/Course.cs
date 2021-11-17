using System.Collections.Generic;
using System.Web.Mvc;

namespace WebApplication3.Models
{
  public class Course
  {
    public string Name { get; set; }
    public List<Topic> Topics { get; set; }
  }
}