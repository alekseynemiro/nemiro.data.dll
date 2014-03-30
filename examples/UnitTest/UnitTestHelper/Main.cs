using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace UnitTestHelper
{
  public static class Main
  {

    public static void WriteLine(string value, params object[] args)
    {
      if (value.Trim('.') == "Successfully!")
      {
        Console.ForegroundColor = ConsoleColor.Green;
      }
      else if (value.Trim('.') == "Skip")
      {
        Console.ForegroundColor = ConsoleColor.Magenta;
      }
      else if (value.Trim('.') == "Fail")
      {
        Console.Beep();
        Console.ForegroundColor = ConsoleColor.Red;
      }
      Console.WriteLine(value, args);
      Console.ForegroundColor = ConsoleColor.Gray;
      using (var fs = new FileStream("report.log", FileMode.Append, FileAccess.Write, FileShare.Read))
      {
        using (var sw = new StreamWriter(fs, Encoding.UTF8))
        {
          sw.WriteLine(String.Format(value, args));
        }
      }
    }

    public static void Pause(string prefix = "")
    {
      UnitTestHelper.Main.WriteLine("{0}Pause 1 sec.", prefix);
      Thread.Sleep(1000);
    }

  }
}
