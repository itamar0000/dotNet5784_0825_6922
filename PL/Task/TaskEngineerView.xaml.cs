using PL.Engineer;
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

namespace PL.Task
{
    /// <summary>
    /// Interaction logic for TaskEngineerView.xaml
    /// </summary>
    public partial class TaskEngineerView : Window
    {

        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public IEnumerable<BO.TaskInList> TaskInLists
        {
            get { return (IEnumerable<BO.TaskInList>)GetValue(TaskInListsProperty); }
            set { SetValue(TaskInListsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Engineer.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TaskInListsProperty =
            DependencyProperty.Register("TaskInLists", typeof(IEnumerable<BO.TaskInList>), typeof(TaskEngineerView), new PropertyMetadata(null));


        public TaskEngineerView(int Id)
        {
            try
            {
                IEnumerable<BO.Task?>? temp = s_bl.Task.ReadAll(Item => Item?.Engineer?.Id == Id);
                TaskInLists = temp.Select(item => new BO.TaskInList()
                {
                    Id = item.Id,
                    Alias = item.Alias,
                    Description = item.Description,
                    Status = item.Status
                });
                InitializeComponent();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
