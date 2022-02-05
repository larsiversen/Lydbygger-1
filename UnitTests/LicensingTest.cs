using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lydbygger_1;

namespace UnitTests
{
  [TestClass]
  public class LicensingTest
  {
    [TestMethod]
    public void TestCheckLicense()
    {
      var lic = new Lydbygger_1.Logic.Licensing();

      Assert.IsTrue(lic.CheckLicenseKey("Rh0L-PYGa-QgSe-NqKZ"));
      Assert.IsTrue(lic.CheckLicenseKey("Obtc-Oo1c-PeCv-S8QJ"));
      Assert.IsTrue(lic.CheckLicenseKey("Pdht-Q6Hf-Og1C-QOJa"));
      Assert.IsTrue(lic.CheckLicenseKey("Pg8J-P3Ee-QgQc-PLHX"));
      Assert.IsTrue(lic.CheckLicenseKey("PeL5-QGSg-RheN-NZGl"));
      Assert.IsTrue(lic.CheckLicenseKey("ObPb-OnLc-ReXG-QSQe"));

      Assert.IsTrue(lic.CheckLicenseKey("Qes5-OHzg-PeBN-QZJI"));

      Assert.IsTrue(lic.CheckLicenseKey("Obwf-Pr4d-PeFy-RAPM"));

      Assert.IsTrue(lic.CheckLicenseKey("Ob4b-Onzc-PeCu-S7QI"));
      Assert.IsTrue(lic.CheckLicenseKey("Qg6H-RTfa-QbOa-MmQy"));
      Assert.IsTrue(lic.CheckLicenseKey("Rdlx-Ofrb-Pd4F-PyQA"));
      Assert.IsTrue(lic.CheckLicenseKey("RhjS-Oeqb-Pd3k-PwMV"));
      Assert.IsTrue(lic.CheckLicenseKey("Pg3E-OQch-ObLX-SjJv"));
      Assert.IsTrue(lic.CheckLicenseKey("Qg5G-RSeh-QbNZ-M8JJ"));
      Assert.IsTrue(lic.CheckLicenseKey("ObVh-Rtcd-Obo1-QCSv"));
      Assert.IsTrue(lic.CheckLicenseKey("OcWi-Pu7d-OfeN-SZNl"));

      Assert.IsFalse(lic.CheckLicenseKey("AcWi-Pu7d-OfeN-SZNl"));
      Assert.IsFalse(lic.CheckLicenseKey("ObWi-Pu7d-OfeN-SZNl"));


    }
  }
}
