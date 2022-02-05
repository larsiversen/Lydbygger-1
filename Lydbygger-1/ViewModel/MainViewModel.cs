using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Lydbygger_1.Model;
using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Printing;
using System.Windows.Markup;
using System.IO;
using System.Xml;
using System.Linq;


namespace Lydbygger_1.ViewModel
{
  /// <summary>
  /// This class contains properties that the main View can data bind to.
  /// <para>
  /// See http://www.galasoft.ch/mvvm
  /// </para>
  /// </summary>
  public class MainViewModel : ViewModelBase
  {
    private readonly IDataService _dataService;
    private string _canvasText = string.Empty;

    public RelayCommand<FrameworkElement> PrintCommand { get; private set; }
    public RelayCommand<FrameworkElement> AutoLayoutCommand { get; private set; }
    public RelayCommand<FrameworkElement> ClearCommand { get; private set; }

    private Visibility _TextVisible;
    

    public ObservableCollection<MySourceImage> SourceImages
    {
      get;
      private set;
    }

    public Visibility TextVisibility
    {
      get
      {
        return _TextVisible;
      }
      set
      {
        _TextVisible = value;
        RaisePropertyChanged();
      }
    }

    /// <summary>
    /// The <see cref="WelcomeTitle" /> property's name.
    /// </summary>
    public const string WelcomeTitlePropertyName = "WelcomeTitle";

    private string _welcomeTitle = string.Empty;

    /// <summary>
    /// Gets the WelcomeTitle property.
    /// Changes to that property's value raise the PropertyChanged event. 
    /// </summary>
    public string WelcomeTitle
    {
      get
      {
        return _welcomeTitle;
      }

      set
      {
        if (_welcomeTitle == value)
        {
          return;
        }

        _welcomeTitle = value;
        RaisePropertyChanged(WelcomeTitlePropertyName);
      }

    }

    /// <summary>
    /// Initializes a new instance of the MainViewModel class.
    /// </summary>
    public MainViewModel(IDataService dataService)
    {
      _dataService = dataService;
      _dataService.GetData(
          (item, error) =>
          {
            if (error != null)
            {
              // Report error here
              return;
            }

            WelcomeTitle = item.Title;
          });
      SourceImages = new ObservableCollection<MySourceImage>();


      var imagefiles = System.IO.Directory.GetFiles("Images");

      foreach (string f in imagefiles)
      {
        SourceImages.Add(new MySourceImage() { Image = f, Name = System.IO.Path.GetFileNameWithoutExtension(f) });
      }
      PrintCommand = new RelayCommand<FrameworkElement>(PrintPage);
      AutoLayoutCommand = new RelayCommand<FrameworkElement>(AutoLayout);
      ClearCommand = new RelayCommand<FrameworkElement>(ClearCanvas);
      TextVisibility = Visibility.Visible;



    }

    ////public override void Cleanup()
    ////{
    ////    // Clean up if needed

    ////    base.Cleanup();
    ////}

    private void AutoLayout(FrameworkElement ele)
    {

      var horisontal = 50.0;
      double vertical = 10.0;

      Canvas canvas = (Canvas)ele;


      var canvasChildrenOrg = canvas.Children.Cast<UIElement>().ToArray();


      var numberOfPics = canvas.Children.Count;

      UIElement pictureToWorkOn;

      for (var i = 0; i <= 6; i++)
      {
        var k = 0;
        var activePictureNo = 0;
        foreach (UIElement picture in canvasChildrenOrg)
        {
          pictureToWorkOn = picture;
          if (i != 0)
          {
            var xaml = System.Windows.Markup.XamlWriter.Save(picture);
            var deepCopy = System.Windows.Markup.XamlReader.Parse(xaml) as UIElement;
            canvas.Children.Add(deepCopy);
            pictureToWorkOn = canvas.Children[activePictureNo];
          }

          Canvas.SetTop(pictureToWorkOn, vertical);
          Canvas.SetLeft(pictureToWorkOn, horisontal + k);

          k += 100;
          activePictureNo++;
        }
        vertical += 95;
      }
    }

    private void ClearCanvas(FrameworkElement ele)
    {
      ((Canvas)ele).Children.Clear();
      TextVisibility = Visibility.Visible;

    }

    private void PrintPage(FrameworkElement ele)
    {
      PrintDialog dlg = new PrintDialog();

      //// dlg.PrintVisual(ele, "User Control Printing.");

      Visual v = ele;
      Print(v);
    }

    private void Print(Visual v)
    {

      System.Windows.FrameworkElement e = v as System.Windows.FrameworkElement;
      if (e == null)
        return;


      PrintDialog pd = new PrintDialog();
      if (pd.ShowDialog() == true)
      {
        //store original scale
        Transform originalTranform = e.LayoutTransform;
        Size OldSize = new Size(e.ActualWidth, e.ActualHeight);


        //get selected printer capabilities
        System.Printing.PrintCapabilities capabilities = pd.PrintQueue.GetPrintCapabilities(pd.PrintTicket);

        //get scale of the print wrt to screen of WPF visual
        double scale = Math.Min(capabilities.PageImageableArea.ExtentWidth / e.ActualWidth, capabilities.PageImageableArea.ExtentHeight /
                       e.ActualHeight);

        //Transform the Visual to scale
        var s = new ScaleTransform(scale, scale);
        s.CenterX = 0.5;
        s.CenterY = 0.5;

        e.LayoutTransform = s;

        //get the size of the printer page
        System.Windows.Size sz = new System.Windows.Size(capabilities.PageImageableArea.ExtentWidth, capabilities.PageImageableArea.ExtentHeight);

        //update the layout of the visual to the printer page size.
        e.Measure(sz);
        e.Arrange(new System.Windows.Rect(new System.Windows.Point(capabilities.PageImageableArea.OriginWidth, capabilities.PageImageableArea.OriginHeight), sz));

        //now print the visual to printer to fit on the one page.
        pd.PrintVisual(v, "My Print");

        //apply the original transform.
        e.LayoutTransform = originalTranform;

        e.Measure(OldSize);
        e.Arrange(new Rect(new Point(OldSize.Width, 0), OldSize));


      }
    }

  }



  public class MySourceImage : INotifyPropertyChanged
  {
    private string _image;
    private string _name;
    public string Image
    {
      get { return _image; }
      set { _image = value; NotifyPropertyChanged("Image"); }
    }

    public string Name
    {
      get { return _name; }
      set { _name = value; NotifyPropertyChanged("Name"); }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    public void NotifyPropertyChanged(string property)
    {
      if (PropertyChanged != null)
      {
        PropertyChanged(this, new PropertyChangedEventArgs(property));
      }
    }
  }
}