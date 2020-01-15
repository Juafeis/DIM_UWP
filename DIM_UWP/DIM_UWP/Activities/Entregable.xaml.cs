using DIM_UWP.Objects;
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
using Windows.UI.Xaml.Media.Imaging;
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
        private List<Image> elementos;
        private List<InertialImage> imagenes;

        public Entregable()
        {
            this.InitializeComponent();
            elementos = new List<Image>();
            imagenes = new List<InertialImage>();
        }

        private void Image_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            //Execute whatever you want from your client:
            Image imgAux = sender as Image;
            var image_Transform = (CompositeTransform)imgAux.RenderTransform;
            InertialImage imagenActual = imagenes.Find(i => i.GetPosX() == image_Transform.TranslateX && i.GetPosY() == image_Transform.TranslateY);
            var element = (FrameworkElement)e.OriginalSource;
            bool xInertial = imagenActual.XInertia;
            bool yInertial = imagenActual.YInertia;
            if (element != null)
            {
                if (e.IsInertial)
                {
                    if (Math.Abs(image_Transform.TranslateX) + imagenActual.GetImage().ActualWidth >= xGrid && Math.Abs(image_Transform.TranslateY) + imagenActual.GetImage().ActualHeight >= yGrid * 0.5)
                    {
                        xInertial = !xInertial;
                    }
                    if (Math.Abs(image_Transform.TranslateY) + imagenActual.GetImage().ActualHeight >= yGrid)
                    {
                         yInertial = !yInertial;
                    }
                    if (Math.Abs(image_Transform.TranslateX) + imagenActual.GetImage().ActualWidth >= xGrid && Math.Abs(image_Transform.TranslateY) + imagenActual.GetImage().ActualHeight < yGrid * 0.5)
                    {
                        try
                        {
                            Goal(image_Transform.TranslateX);
                            grid.Children.Remove(imagenActual.GetImage());
                        }
                        catch (Exception ex)
                        {
                            
                        }
                        return;
                    }
                    //if(elementos.Count > 0)
                    //{
                    //    foreach (var elemento in elementos)
                    //    {
                    //        if(Math.Abs(image_Transform.TranslateX) + imagenActual.GetImage().ActualWidth >= elemento.X)
                    //        {
                    //            xInertial = xInertial ? xInertial = false : xInertial = true;
                    //        }
                    //        if(Math.Abs(image_Transform.TranslateY) + imagenActual.GetImage().ActualHeight >= elemento.Y)
                    //        {
                    //            yInertial = yInertial ? yInertial = false : yInertial = true;
                    //        }
                    //    }
                    //}
                    
                }
            }
            imagenActual.XInertia = xInertial;
            imagenActual.YInertia = yInertial;
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

        private void Goal(double transformX)
        {
            if (transformX < 0)
            {
                textBlockCountPlayer1.Text = (int.Parse(textBlockCountPlayer1.Text) + 1).ToString();
            }
            else
            {
                textBlockCountPlayer2.Text = (int.Parse(textBlockCountPlayer2.Text) + 1).ToString();
            }        
        }

        private void Image_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            xGrid = internalGrid.ActualWidth/2;
            yGrid = internalGrid.ActualHeight/2;
            
        }

        private void Image_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            Image imgAux = sender as Image;
            var image_Transform = (CompositeTransform)imgAux.RenderTransform;
            InertialImage imagenActual = imagenes.Find(i => i.GetPosX() == image_Transform.TranslateX && i.GetPosY() == image_Transform.TranslateY);
            imagenActual.XInertia = imagenActual.YInertia = false;
        }

        private void Image_ManipulationInertiaStarting(object sender, ManipulationInertiaStartingRoutedEventArgs e)
        {
            var element = e.OriginalSource as FrameworkElement;
            if (element == null) return;
            e.TranslationBehavior.DesiredDeceleration = 20.0 * 96.0 / (1000.0 * 1000.0);
        }



        private void Image_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            //Cursor gets in the path of the image
            Image_ManipulationInertiaStarting(sender, new ManipulationInertiaStartingRoutedEventArgs());
        }

      


        private void Grid_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            //var pointerPosition = Windows.UI.Core.CoreWindow.GetForCurrentThread().PointerPosition;
            var inertialImage = new InertialImage();
            var image = inertialImage.GetImage();
            image.ManipulationStarted += Image_ManipulationStarted;
            image.ManipulationDelta += Image_ManipulationDelta;
            image.ManipulationCompleted += Image_ManipulationCompleted;
            grid.Children.Add(image);
            var compositeTransform = image.RenderTransform as CompositeTransform;
            compositeTransform.TranslateX = e.GetPosition(grid).X - grid.ActualWidth / 2;
            compositeTransform.TranslateY = e.GetPosition(grid).Y - grid.ActualHeight / 2;
            imagenes.Add(inertialImage);
        }



        private void Element_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            var element = sender as Image;
            CompositeTransform source = (CompositeTransform)element.RenderTransform;
            source.TranslateY += e.Delta.Translation.Y;
        }

        private void Element_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            
            
        }

        private void Element_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {

        }




        private void Grid_Holding(object sender, HoldingRoutedEventArgs e)
        {
            //var pointerPosition = Windows.UI.Core.CoreWindow.GetForCurrentThread().PointerPosition;
            var element = new Image();
            element.Source = new BitmapImage(new Uri("ms-appx:///Assets/white.png"));
            element.Width = 20;
            element.Height = 100;
            element.Stretch = Stretch.Fill;
            element.ManipulationMode = ManipulationModes.TranslateY;
            CompositeTransform compositeTransform = new CompositeTransform();
            compositeTransform.TranslateX = e.GetPosition(grid).X - grid.ActualWidth / 2; 
            compositeTransform.TranslateY = e.GetPosition(grid).Y - grid.ActualWidth / 2; 
            element.RenderTransform = compositeTransform;
            element.ManipulationStarted += Element_ManipulationStarted;
            element.ManipulationDelta += Element_ManipulationDelta;
            element.ManipulationCompleted += Element_ManipulationCompleted;
            grid.Children.Add(element);
        }

       
    }
}
