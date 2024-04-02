using BO; // Import Business Object namespace
using Microsoft.Win32; // Import Win32 registry manipulation functionality
using Microsoft.Xaml.Behaviors; // Import XAML behaviors
using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EngineerWindow.xaml
    /// </summary>
    public partial class EngineerWindow : Window, INotifyPropertyChanged
    {
        // Static reference to Business Logic layer
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        // Event handler for property changes
        public event PropertyChangedEventHandler? PropertyChanged;

        // Dependency property for Engineer object
        public BO.Engineer Engineer
        {
            get { return (BO.Engineer)GetValue(EngineerProperty); }
            set { SetValue(EngineerProperty, value); }
        }

        // Dependency property definition
        public static readonly DependencyProperty EngineerProperty =
            DependencyProperty.Register("Engineer", typeof(BO.Engineer), typeof(EngineerWindow), new PropertyMetadata(null));

        int id; // ID of the engineer

        // Constructor for EngineerWindow
        public EngineerWindow(int Id = 0)
        {
            InitializeComponent();
            id = Id;

            // Initialize Engineer object based on ID
            if (Id == 0)
            {
                Engineer = new BO.Engineer();
            }
            else
            {
                try
                {
                    Engineer = s_bl.Engineer.Read(Id)!;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // Event handler for Add/Update Engineer button click
        private void Add_UpdateEngineer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Add or update engineer based on ID
                if (id == 0)
                {
                    s_bl.Engineer.Create(Engineer);
                    MessageBox.Show("Engineer added successfully", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    s_bl.Engineer.Update(Engineer);
                    MessageBox.Show("Engineer updated successfully", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                Close(); // Close the window after operation completion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Event handler for text input validation (accepts only digits)
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

        // Event handler for selecting a photo
        private void Button_Photo(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    string imagePath = openFileDialog.FileName;
                    ConvertPhotoToEncodedText(imagePath); // Convert selected photo to base64 encoded text
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error selecting image: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // Convert selected photo to base64 encoded text
        private void ConvertPhotoToEncodedText(string imagePath)
        {
            try
            {
                byte[] imageData = File.ReadAllBytes(imagePath);
                string encodedImageText = Convert.ToBase64String(imageData);
                Engineer.ImagePath = encodedImageText; // Set the encoded image path to Engineer object
                MessageBox.Show("Image selected successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error converting photo to encoded text: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
