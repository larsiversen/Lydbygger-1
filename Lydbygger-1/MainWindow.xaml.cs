﻿using System.Windows;
using Lydbygger_1.ViewModel;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;
using System;


namespace Lydbygger_1
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    MainViewModel _viewModel;

    /// <summary>
    /// Initializes a new instance of the MainWindow class.
    /// </summary>
    public MainWindow()
    {
      InitializeComponent();
      Closing += (s, e) => ViewModelLocator.Cleanup();
      CheckLicense();
      Loaded += OnLydbyggerLoaded;
    }

    private void CheckLicense()
    {
      string name = Logic.AppSettings.ReadSetting("UserName");
      string licenseKey = Logic.AppSettings.ReadSetting("LicKey");
      var lic = new Logic.Licensing();
      if (name == "Error" || string.IsNullOrEmpty(name) || licenseKey == "Error" || string.IsNullOrEmpty(licenseKey) || !lic.CheckLicenseKey(licenseKey))
      {
        //// Bring up the License activation screen
        var licenseView = new LicenseActivationView();
        licenseView.ShowDialog();
      }

    }

    private void OnLydbyggerLoaded(object sender, RoutedEventArgs routedEventArgs)
    {
      _viewModel = DataContext as MainViewModel;
    }


    private void Image_PreviewMouseDown(object sender, MouseButtonEventArgs e)
    {
      Image s = (Image)sender;

      mediaElement1.Source = new Uri("pack://siteoforigin:,,,/../Sounds\\" + s.Tag + ".mp3");
      mediaElement1.Play();

      DragDrop.DoDragDrop((DependencyObject)sender, ((Image)sender).Source, DragDropEffects.Copy);





    }

    private void Canvas_Drop(object sender, DragEventArgs e)
    {
      _viewModel.TextVisibility = Visibility.Collapsed;

      foreach (var format in e.Data.GetFormats())
      {
        ImageSource imageSource = e.Data.GetData(format) as ImageSource;
        if (imageSource != null)
        {
          Image img = new Image();
          Point p = e.GetPosition(sender as IInputElement);
          Canvas cv = sender as Canvas;



          img.Height = 90;
          img.Source = imageSource;
          ((Canvas)sender).Children.Add(img);
          Canvas.SetTop(img, p.Y);
          Canvas.SetLeft(img, p.X);
        }
      }
    }


    private Image draggedImage;
    private Point mousePosition;

    private void CanvasMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      var image = e.Source as Image;

      if (image != null && canvas.CaptureMouse())
      {
        mousePosition = e.GetPosition(canvas);
        draggedImage = image;
        Panel.SetZIndex(draggedImage, 1); // in case of multiple images
      }
    }

    private void CanvasMouseRightButtonDown(object sender, MouseButtonEventArgs e)
    {
      try
      {
        ((Canvas)sender).Children.Remove((Image)e.Source);
        if (((Canvas)sender).Children.Count == 0)
        {
          var dc = (MainViewModel)DataContext;
          dc.TextVisibility = Visibility.Visible;
        }
      }
      catch (Exception)
      {
        // swallow the exception
      }
      
    }

    private void CanvasMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
      if (draggedImage != null)
      {
        canvas.ReleaseMouseCapture();
        Panel.SetZIndex(draggedImage, 0);
        draggedImage = null;
      }
    }

    private void CanvasMouseMove(object sender, MouseEventArgs e)
    {
      if (draggedImage != null)
      {
        var position = e.GetPosition(canvas);
        var offset = position - mousePosition;
        mousePosition = position;
        Canvas.SetLeft(draggedImage, Canvas.GetLeft(draggedImage) + offset.X);
        Canvas.SetTop(draggedImage, Canvas.GetTop(draggedImage) + offset.Y);
      }
    }

    private void CanvasDragLeave(object sender, DragEventArgs e)
    {

    }


  }
}