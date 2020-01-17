using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
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
    public sealed partial class Activity4 : Page
    {
        List<CompositeTransform> Images { get; set; }
        public Activity4()
        {
            this.InitializeComponent();
            Images = new List<CompositeTransform>();
            Images.AddRange( new List<CompositeTransform>{ image_Transform, image_Transform2, image_Transform3, image_Transform4});
            foreach (var imageT in Images)
            {
                imageT.ScaleX = imageT.ScaleY = 1;
                imageT.Rotation = 0;
            }
            
        }

        private void ManageManipulation(ManipulationDeltaRoutedEventArgs e)
        {
            var source = (FrameworkElement)e.OriginalSource;
            var index = int.Parse(Regex.Match(source.Name, @"\d+").Value)-1;
            Images[index].ScaleX *= e.Delta.Scale;
            Images[index].ScaleY *= e.Delta.Scale;
            Images[index].Rotation += e.Delta.Rotation;

            Images[index].CenterX = source.Width / 2;
            Images[index].CenterY = source.Height / 2;

            Images[index].TranslateX += e.Delta.Translation.X;
            Images[index].TranslateY += e.Delta.Translation.Y;
        }
        private void Image_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            ManageManipulation(e);
        }

        private void Image_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            image1.Opacity = 0.4;
        }

        private void Image_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            image1.Opacity = 1;
        }

        private void Image2_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            image2.Opacity = 1;
        }

        private void Image2_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            ManageManipulation(e);
        }

        private void Image2_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            image2.Opacity = 0.4;
        }

        private void Image3_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            image3.Opacity = 1;
        }

        private void Image3_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            ManageManipulation(e);
        }

        private void Image3_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            image3.Opacity = 0.4;
        }

        private void Image4_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            image4.Opacity = 1;
        }

        private void Image4_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            ManageManipulation(e);
        }

        private void Image4_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            image4.Opacity = 0.4;
        }


    }
}
