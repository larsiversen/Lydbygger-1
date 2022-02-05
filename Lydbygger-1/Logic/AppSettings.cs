using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Lydbygger_1.Logic
{
  public class AppSettings
  {
    public static string ReadSetting(string key)
    {
      try
      {
        Properties.Settings.Default.Upgrade();
        return (string)Properties.Settings.Default[key];
      }
      catch
      {
        return "Error";
      }
    }

    public static void AddUpdateAppSettings(string key, string value)
    {
      Properties.Settings.Default[key] = value;
      Properties.Settings.Default.Save();
      Properties.Settings.Default.Reload();

    }
  }
}
