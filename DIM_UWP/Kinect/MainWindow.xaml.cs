﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Kinect;

namespace Kinect
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        KinectSensor miKinect;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            miKinect = KinectSensor.KinectSensors[0];
            miKinect.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
            miKinect.Start();
            miKinect.ColorFrameReady += miKinect_ColorFrameReady;
        }

        WriteableBitmap bitmapEficiente = null;

        void miKinect_ColorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            using (ColorImageFrame framesImagen = e.OpenColorImageFrame())
            {

                if (framesImagen == null)
                    return;

                byte[] datosColor = new byte[framesImagen.PixelDataLength];

                framesImagen.CopyPixelDataTo(datosColor);

                if (grabarFoto)
                {
                    bitmapImagen = BitmapSource.Create(
                        framesImagen.Width, framesImagen.Height, 96, 96, PixelFormats.Bgr32, null,
                        datosColor, framesImagen.Width * framesImagen.BytesPerPixel);
                    grabarFoto = false;
                }

                if (WriteableBitmap.IsChecked == true)
                {
                    if (bitmapEficiente == null)
                    {
                        bitmapEficiente = new WriteableBitmap(
                            framesImagen.Width, framesImagen.Height,
                            96,
                            96,
                            PixelFormats.Bgr32,
                            null
                            );
                    }
                    bitmapEficiente.WritePixels(
                        new Int32Rect(0, 0, framesImagen.Width, framesImagen.Height),
                        datosColor,
                        framesImagen.Width * framesImagen.BytesPerPixel,
                        0
                        );

                    colorStream.Source = bitmapEficiente;
                }
                else
                {
                    colorStream.Source = BitmapSource.Create(
                        framesImagen.Width, framesImagen.Height,
                        96,
                        96,
                        PixelFormats.Bgr32,
                        null,
                        datosColor,
                        framesImagen.Width * framesImagen.BytesPerPixel
                        );
                }
            }
        }

        bool grabarFoto;
        BitmapSource bitmapImagen = null;

        private void tomarFoto_Click(object sender, RoutedEventArgs e)
        {
            grabarFoto = true;

            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "capturaDeKinect";
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "Pictures (.jpg)|*.jpg";

            if (dlg.ShowDialog() == true)
            {
                string nombreArchivo = dlg.FileName;
                using (FileStream stream = new FileStream(nombreArchivo, FileMode.Create))
                {
                    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(bitmapImagen));
                    encoder.Save(stream);
                }
            }
        }

    }
}
