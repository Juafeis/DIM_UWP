using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DIM_UWP.Activities
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Activity2 : Page
    {
        ObservableCollection<Action> Actions { get; set; }
        public Activity2()
        {
            this.InitializeComponent();
            Actions = new ObservableCollection<Action>();
            listView.ItemsSource = Actions;
        }
        

        private void AddAction(string methodName, double x, double y)
        {
            var action = new Action
            {
                MouseAction = methodName,
                Description = "Values: x = " + x.ToString() + " and y = " + y.ToString()
            };
            Actions.Add(action);
        }
        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            double x = e.GetPosition(image).X;
            double y = e.GetPosition(image).Y;
            AddAction("Image_Tapped", x, y);
            image.Opacity -= 0.1;
        }

        private void Image_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            double x = e.GetPosition(image).X;
            double y = e.GetPosition(image).Y;
            AddAction("Image_DoubleTapped", x, y);
            image.Opacity = 1;
        }

        private void Image_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            double x = e.GetPosition(image).X;
            double y = e.GetPosition(image).Y;
            AddAction("Image_RightTapped", x, y);
        }

        private void Image_Holding(object sender, HoldingRoutedEventArgs e)
        {
            double x = e.GetPosition(image).X;
            double y = e.GetPosition(image).Y;
            AddAction("Image_Holding", x, y);
            Actions.Clear();
        }
    }
}
