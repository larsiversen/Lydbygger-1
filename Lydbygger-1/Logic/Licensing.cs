using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lydbygger_1.Logic
{
  public class Licensing
  {
    public bool CheckLicenseKey(string key)
    {

      ////// Rules:  all ascii values of pos 1 should give: 97+54+56+114=321
      //////         values of pos 2 4 2 3 shuold give 379
      if (string.IsNullOrEmpty(key))
      {
        return false;
      }

      string[] keySegments = key.Split('-');
      if (keySegments.Count() != 4)
      {
        return false;
      }
 
      string segment1 = keySegments[0];
      byte[] Segment1Bytes = Encoding.ASCII.GetBytes(segment1);
      string segment2 = keySegments[1];
      byte[] Segment2Bytes = Encoding.ASCII.GetBytes(segment2);
      string segment3 = keySegments[2];
      byte[] Segment3Bytes = Encoding.ASCII.GetBytes(segment3);
      string segment4 = keySegments[3];
      byte[] Segment4Bytes = Encoding.ASCII.GetBytes(segment4);

      if ((int)Segment1Bytes[0] + (int)Segment2Bytes[0] + (int)Segment3Bytes[0] + (int)Segment4Bytes[0] != 321)
      {
        return false;
      }

      if ((int)Segment1Bytes[1] + (int)Segment2Bytes[3] + (int)Segment3Bytes[1] + (int)Segment4Bytes[2] != 379)
      {
        return false;
      }

      return true;



      
    }


  }
}
