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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DIM_UWP.Activities
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Entregable : Page
    {
        private double xGrid;
        private double yGrid;
        private bool xInertial;
        private bool yInertial;

        public Entregable()
        {
            this.InitializeComponent();
            image_Transform.ScaleX = image_Transform.ScaleY = 1;
            image_Transform.Rotation = 0;

        }

        private void Image_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            var element = (FrameworkElement)e.OriginalSource;
            if(element != null)
            {
                if (e.IsInertial)
                {
                    if (Math.Abs(image_Transform.TranslateY) + image.ActualHeight >= yGrid)
                    {
                        yInertial = yInertial ? yInertial = false : yInertial = true;
                    }
                    if (Math.Abs(image_Transform.TranslateX) + image.ActualWidth >= xGrid)
                    {
                        xInertial = xInertial ? xInertial = false : xInertial = true;
                    }
                }
            }
            if (!xInertial && yInertial)
            {
                image_Transform.TranslateX += e.Delta.Translation.X;
                image_Transform.TranslateY -= e.Delta.Translation.Y;
            }
            else if (xInertial && !yInertial)
            {
                image_Transform.TranslateX -= e.Delta.Translation.X;
                image_Transform.TranslateY += e.Delta.Translation.Y;
            }
            else if (xInertial && yInertial)
            {
                image_Transform.TranslateX -= e.Delta.Translation.X;
                image_Transform.TranslateY -= e.Delta.Translation.Y;
            }
            else
            {
                image_Transform.TranslateX += e.Delta.Translation.X;
                image_Transform.TranslateY += e.Delta.Translation.Y;
            }
        }

        private void Image_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            xGrid = grid.ActualWidth;
            yGrid = grid.ActualHeight;
            
        }

        private void Image_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            xInertial = false;
            yInertial = false;
        }

        private void Image_ManipulationInertiaStarting(object sender, ManipulationInertiaStartingRoutedEventArgs e)
        {
            var element = e.OriginalSource as FrameworkElement;
            if (element == null) return;
            e.TranslationBehavior.DesiredDeceleration = 20.0 * 96.0 / (1000.0 * 1000.0);
        }
    }
}
