using System.Collections.Generic;
using System.Web.Mvc;

namespace WebApplication3.Models
{
  public class Form
  {
    public string Discipline { get; set; }
    public List<SelectListItem> Directions { get; set; }
  }
}