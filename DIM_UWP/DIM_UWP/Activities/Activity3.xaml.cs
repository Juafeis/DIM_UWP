using DIM_UWP.Objects;
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
    public sealed partial class Activity3 : Page
    {
        ObservableCollection<ListViewAction> Actions { get; set; }
        public Activity3()
        {
            this.InitializeComponent();
            Actions = new ObservableCollection<ListViewAction>();
            listView.ItemsSource = Actions;
            image_Transform.ScaleX = image_Transform.ScaleY = 1;
            image_Transform.Rotation = 0;
            image.ManipulationMode = ManipulationModes.Scale | ManipulationModes.Rotate;
        }
        private void AddAction(string methodName, double x, double y)
        {
            var action = new ListViewAction
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
        }

        private void Image_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            double x = e.GetPosition(image).X;
            double y = e.GetPosition(image).Y;
            AddAction("Image_DoubleTapped", x, y);
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
        }

        private void Image_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            var source = (FrameworkElement)e.OriginalSource;
            image_Transform.ScaleX *= e.Delta.Scale;
            image_Transform.ScaleY *= e.Delta.Scale;
            image_Transform.Rotation += e.Delta.Rotation;

            image_Transform.CenterX = source.Width / 2;
            image_Transform.CenterY = source.Height / 2;




           AddAction("Manipulation_Delta", image_Transform.ScaleX, image_Transform.ScaleY);
        }

        private void Image_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            image.Opacity = 0.4;
        }

        private void Image_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            image.Opacity = 1;
        }
    }
}
