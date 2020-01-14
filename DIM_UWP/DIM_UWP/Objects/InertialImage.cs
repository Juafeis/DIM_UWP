using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIM_UWP.Objects
{
    public class InertialImage
    {
        private Image image = new Image();
        public bool XInertia { get; set; }
        public bool YInertia { get; set; }

        public InertialImage()
        {
            image.Source = new BitmapImage(new Uri("ms-appx:///Assets/ball.png"));
            image.ManipulationMode = ManipulationModes.All;
            image.Width = image.Height = 50;
            CompositeTransform compositeTransform = new CompositeTransform();
            compositeTransform.ScaleX = 1;
            compositeTransform.ScaleY = 1;
            compositeTransform.Rotation = 0;
            image.RenderTransform = compositeTransform;
        }

        public Image GetImage()
        {
            return this.image;
        }
        public double GetPosX()
        {
            var pos = (CompositeTransform)image.RenderTransform;
            return pos.TranslateX;
        }
        public double GetPosY()
        {
            var pos = (CompositeTransform)image.RenderTransform;
            return pos.TranslateY;
        }

    }
}
