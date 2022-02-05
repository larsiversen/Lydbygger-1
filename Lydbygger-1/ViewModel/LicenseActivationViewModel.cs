using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;


namespace Lydbygger_1.ViewModel
{
  /// <summary>
  /// This class contains properties that a View can data bind to.
  /// <para>
  /// See http://www.galasoft.ch/mvvm
  /// </para>
  /// </summary>
  public class LicenseActivationViewModel : ViewModelBase
  {
    /// <summary>
    /// Initializes a new instance of the LicenseActivationViewModel class.
    /// </summary>
    public LicenseActivationViewModel()
    {
      ActivateLicenseCommand = new RelayCommand(ActivateLicense);
    }

    private string _userName;
    private string _licenseKey;
    public RelayCommand ActivateLicenseCommand { get; private set; }


    private void ActivateLicense()
    {
      Logic.Licensing lic = new Logic.Licensing();
      if (string.IsNullOrEmpty(this.UserName) || !lic.CheckLicenseKey(this.LicenseKey))
      {
        System.Environment.Exit(0);
      }
      Logic.AppSettings.AddUpdateAppSettings("UserName", this.UserName);
      Logic.AppSettings.AddUpdateAppSettings("LicKey", this.LicenseKey);
      
      NotifyWindowToClose();
    }

    private void NotifyWindowToClose()
    {
      Messenger.Default.Send<NotificationMessage>(new NotificationMessage(this, "CloseWindowsBoundToMe"));
    }


    public string UserName
    {
      get
      {
        return _userName;
      }
      set
      {
        _userName = value;
        RaisePropertyChanged();
      }
    }

    public string LicenseKey
    {
      get
      {
        return _licenseKey;
      }
      set
      {
        _licenseKey = value;
        RaisePropertyChanged();
      }
    }

  }
}