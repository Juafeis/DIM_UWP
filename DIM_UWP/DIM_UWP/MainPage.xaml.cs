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
using System.Diagnostics;
using DIM_UWP.Activities;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DIM_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var index = args.InvokedItem as NavigationViewItem;
            if (index != null) ChangeListViewItem(index.Name);
        }

        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var index = args.SelectedItem as NavigationViewItem;
            if (index != null) ChangeListViewItem(index.Name);
        }

        private void ChangeListViewItem(string index)
        {
            var indexInt = Convert.ToByte(index.Replace("e", ""));
            Debug.WriteLine(indexInt.ToString());
            switch (indexInt)
            {
                case 0:
                    ContentFrame.Navigate(typeof(Activity1), 0);
                    break;
                case 1:
                    ContentFrame.Navigate(typeof(Activity2), 1);
                    break;
                case 2:
                    ContentFrame.Navigate(typeof(Activity3), 2);
                    break;
                case 3:
                    ContentFrame.Navigate(typeof(Activity4), 2);
                    break;
            }
        }
    }
}
