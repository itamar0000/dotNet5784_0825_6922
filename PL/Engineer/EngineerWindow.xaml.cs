using BO;
using Microsoft.Win32;
using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing.IndexedProperties;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EngineerWindow.xaml
    /// </summary>
    public partial class EngineerWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public BO.Engineer engineer
        {
            get { return (BO.Engineer)GetValue(EngineerProperty); }
            set { SetValue(EngineerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        int id;
        public static readonly DependencyProperty EngineerProperty =
           DependencyProperty.Register("engineer", typeof(BO.Engineer), typeof(EngineerWindow), new PropertyMetadata(null));


        public EngineerWindow(int Id = 0)
        {
             BitmapImage imageSource = new BitmapImage(new Uri("C:\\Users\\olete\\source\\repos\\dotNet5784_0825_6922\\PL\\Images\\defaultImageOfEngineer.jpg"));
            Image = imageSource;
            InitializeComponent();
            id = Id;
            if (Id == 0)
            {
                engineer = new BO.Engineer();
            }
            else
            {
                try
                {
                    engineer = s_bl.Engineer.Read(Id)!;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }


        private void Add_UpdateEngineer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (id == 0)
                {
                    s_bl.Engineer.Create(engineer);
                    MessageBox.Show("Engineer added successfully", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    s_bl.Engineer.Update(engineer);
                    MessageBox.Show("Engineer updated successfully", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void Txt_Input(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!Char.IsDigit(c))
                {
                    e.Handled = true; // Mark the event as handled, preventing the character from being entered
                    return;
                }
            }
        }


        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageSourceProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(ImageSource), typeof(EngineerWindow), new PropertyMetadata(null));



        private void Button_Photo(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            if (openFileDialog.ShowDialog() == true)
                {
                    try
                    {
                        // Get the selected file path
                        string imagePath = openFileDialog.FileName;

                        // Store the image path in your class or wherever you need it
                        engineer.ImagePath = imagePath;

                        // Load the image into an Image control or any other appropriate control
                        BitmapImage imageSource = new BitmapImage(new Uri(imagePath));
                        Image = imageSource;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error loading image: " + ex.Message);
                    }
                }
        }

    }
}

