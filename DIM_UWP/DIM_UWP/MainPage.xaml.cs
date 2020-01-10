using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DIM_UWP
{
    public class Action
    {
        public string MouseAction { get; set; }
        public string Description { get; set; }
    }


    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static List<Action> actions;
        public MainPage()
        {
            this.InitializeComponent();
            actions = new List<Action>();
        }

        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {

            
            double x = e.GetPosition(image).X;
            double y = e.GetPosition(image).Y;
            actions.Add(new Action { MouseAction = "Image_Tapped", Description = "Values: x = " + x.ToString() + " and y = " + y.ToString() });
            listView.ItemsSource = actions;
            
            
        }

        private void Image_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            double x = e.GetPosition(image).X;
            double y = e.GetPosition(image).Y;
        }

        private void Image_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            double x = e.GetPosition(image).X;
            double y = e.GetPosition(image).Y;
        }

        private void Image_Holding(object sender, HoldingRoutedEventArgs e)
        {
            double x = e.GetPosition(image).X;
            double y = e.GetPosition(image).Y;
        }
    }
}
