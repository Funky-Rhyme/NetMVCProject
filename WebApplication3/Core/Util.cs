using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3
{
  public static class Util
  {
    public static int GetColumnNumber(string name)
    {
      int number = 0;
      int pow = 1;
      for (int i = name.Length - 1; i >= 0; i--)
      {
        number += (name[i] - 'A' + 1) * pow;
        pow *= 26;
      }

      return number;
    }
  }
}