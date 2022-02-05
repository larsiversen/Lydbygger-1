using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace LydByggerLicenseGenerator
{
  public partial class frmLicenseGenerator : Form
  {
    public frmLicenseGenerator()
    {
      InitializeComponent();
      
    }

    private static string RandomString(int length, string chars)
    {
     // const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
      var random = new Random();
      return new string(Enumerable.Repeat(chars, length)
        .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    private static char RandomChar(string charsToChooseFrom)
    {
      Thread.Sleep(30);
      // const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
      var random = new Random();
      return new string(Enumerable.Repeat(charsToChooseFrom, 1)
        .Select(s => s[random.Next(s.Length)]).ToArray()).ToCharArray()[0];
    }

    private void button1_Click(object sender, EventArgs e)
    {
      char[] chars = new char[19];

      
      
      chars[4] = '-';
      chars[9] = '-';
      chars[14] = '-';

      // 1.1
      chars[0] = (RandomChar("OPQR"));
      chars[5] = (RandomChar("OPQR"));
      chars[10] = (RandomChar("OPQR"));
      chars[15] = (char)(321 - (int)chars[0] - (int)chars[5] - (int)chars[10]);

      chars[1] = (RandomChar("abcdefgh"));
      chars[8] = (RandomChar("abcdefgh"));
      chars[11] = (RandomChar("abcdefgh"));
      chars[17] = (char)(379 - (int)chars[1] - (int)chars[8] - (int)chars[11]);


      // abcd-egfh-hred-erts
      // 0123456789012345678
      // 0123456789012345678

      chars[2] = RandomChar("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890");
      chars[3] = RandomChar("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890");
      chars[6] = RandomChar("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890");
      chars[7] = RandomChar("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890");
      chars[12] = RandomChar("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890");
      chars[13] = RandomChar("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890");
      chars[16] = RandomChar("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890");
      chars[18] = RandomChar("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890");


      txtGeneratedKey.Text = new string(chars);
      Console.WriteLine(txtGeneratedKey.Text);

    }
  }
}
