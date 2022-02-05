using System.Windows;
using GalaSoft.MvvmLight.Messaging;

namespace Lydbygger_1
{
  /// <summary>
  /// Description for LicenseActivation.
  /// </summary>
  public partial class LicenseActivationView: Window
  {
    /// <summary>
    /// Initializes a new instance of the LicenseActivation class.
    /// </summary>
    public LicenseActivationView()
    {
      Messenger.Default.Register<NotificationMessage>(this, (nm) =>
            {
              if (nm.Notification == "CloseWindowsBoundToMe")
              {
               if (nm.Sender == this.DataContext)
                this.Close();
               }
            });
    
      InitializeComponent();
    }

    

  }
}