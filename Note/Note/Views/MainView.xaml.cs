﻿using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Note.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainView : ContentPage
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void DragGestureRecognizer_DragStarting(Object sender, DragStartingEventArgs e)
        {
            var label = (Label)((Element)sender).Parent;

            e.Data.Properties["Label"] = label;
        }

        private void DropGestureRecognizer_Drop(Object sender, DropEventArgs e)
        {
            var label = (Label)((Element)sender).Parent;
            var dropLabel = (Label)e.Data.Properties["Label"];
            if (label == dropLabel)
                return;

            var sourceContainer = (Grid)dropLabel.Parent;
            var targetContainer = (Grid)label.Parent;
            sourceContainer.Children.Remove(dropLabel);
            targetContainer.Children.Remove(label);
            sourceContainer.Children.Add(label);
            targetContainer.Children.Add(dropLabel);

            e.Handled = true;
        }

        private void DragGestureRecognizer_DragStarting_Collection(System.Object sender, Xamarin.Forms.DragStartingEventArgs e)
        {
        }

        private void DropGestureRecognizer_Drop_Collection(System.Object sender, Xamarin.Forms.DropEventArgs e)
        {
            e.Handled = true;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }
    }
}