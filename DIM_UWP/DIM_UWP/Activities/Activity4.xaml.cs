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
    public sealed partial class Activity4 : Page
    {
        List<CompositeTransform> Images { get; set; }
        public Activity4()
        {
            this.InitializeComponent();
            Images.AddRange(new List<CompositeTransform>{ image_Transform, image_Transform2, image_Transform3, image_Transform4});
            foreach (var imageT in Images)
            {
                imageT.ScaleX = image_Transform.ScaleY = 1;
                imageT.Rotation = 0;
            }
            
        }


        private void Image_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            var source = (FrameworkElement)e.OriginalSource;
            image_Transform.ScaleX *= e.Delta.Scale;
            image_Transform.ScaleY *= e.Delta.Scale;
            image_Transform.Rotation += e.Delta.Rotation;

            image_Transform.CenterX = image_Transform.CenterY = source.Width / 2;

            image_Transform.TranslateX += e.Delta.Translation.X;
            image_Transform.TranslateY += e.Delta.Translation.Y;
        }

        private void Image_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            image.Opacity = 0.4;
        }

        private void Image_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            image.Opacity = 1;
        }

        private void Image2_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            image2.Opacity = 1;
        }

        private void Image2_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {

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

        }

        private void Image4_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            image4.Opacity = 0.4;
        }


    }
}
