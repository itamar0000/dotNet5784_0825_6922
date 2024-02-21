﻿using PL.Task;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for GetIdWindow.xaml
    /// </summary>
    public partial class GetIdWindow : Window
    {

        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public int Id
        {
            get { return (int)GetValue(IdProperty); }
            set { SetValue(IdProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Id.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IdProperty =
            DependencyProperty.Register("Id", typeof(int), typeof(GetIdWindow), new PropertyMetadata(null));


        public GetIdWindow()
        {
            InitializeComponent();
        }

        private void Btn_EnterId(object sender, RoutedEventArgs e)
        {
            try
            {
                s_bl.Engineer.Read(Id);
                this.Close();
                new TaskEngineerView(Id).Show();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }
    }
}
