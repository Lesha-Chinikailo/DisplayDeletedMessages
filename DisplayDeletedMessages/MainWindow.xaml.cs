using DisplayDeletedMessages.Models;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.IO;

namespace DisplayDeletedMessages
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BotAdministratorContext context = BotAdministratorContext.getInstance();
        private Deletedmessage? _deletedmessage = null;
        private int _countOfMessages;
        private string _addToPathInFront = "../../../../";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void dateGridListMessages_Loaded(object sender, RoutedEventArgs e)
        {


            //DataGridButtonColumn dataGridViewButtonColumn = new DataGridButtonColumn();
            //dataGridViewButtonColumn.Text = "посмотреть";
            //dataGridViewButtonColumn.HeaderText = "View";
            //dataGridViewButtonColumn.Name = "button";
            //dataGridViewButtonColumn.UseColumnTextForButtonValue = true;
            //dateGridListMessages.Columns.Add(dataGridViewButtonColumn);
        }

        private void dateGridListMessages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dateGridListMessages.SelectedIndex < _countOfMessages)
                _deletedmessage = (Deletedmessage)dateGridListMessages.SelectedItem;
            else
                _deletedmessage = null;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<Deletedmessage> list = context.Deletedmessages.ToList();
            _countOfMessages = list.Count;
            dateGridListMessages.ItemsSource = list;
            //dateGridListMessages.Columns.Add(new DataGridTemplateColumn());
            Console.WriteLine();
        }

        private void BtnShowContentInDataGridClicked(object sender, RoutedEventArgs e)
        {
            if (_deletedmessage == null)
                return;
            User user = context.Users.Where(u => u.Id == _deletedmessage.UserId).Single();
            if(_deletedmessage.TypeMessage == "TEXT")
            {
                MessageBox.Show(_deletedmessage.MessageOrPath, 
                    $"{(user.Name != null ? user.Name : user.Username)} прислал сообщение", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.None);

            }
            if (_deletedmessage.TypeMessage == "PHOTO")
            {
                string pathToPhoto = _addToPathInFront + _deletedmessage.MessageOrPath;
                pathToPhoto = Path.GetFullPath(pathToPhoto);
                var startInfo = new ProcessStartInfo
                {
                    FileName = pathToPhoto,
                    UseShellExecute = true // Важно для открытия файлов с ассоциациями
                };

                try
                {
                    Process.Start(startInfo);
                    //ThreadStart ths = new ThreadStart(() => Process.Start(startInfo));
                    //Thread th = new Thread(ths);
                    //th.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            if (_deletedmessage.TypeMessage == "VIDEO")
            {
                string pathToVideo = _addToPathInFront + _deletedmessage.MessageOrPath;
                pathToVideo = Path.GetFullPath(pathToVideo);
                var startInfo = new ProcessStartInfo
                {
                    FileName = pathToVideo,
                    UseShellExecute = true // Важно для открытия файлов с ассоциациями
                };

                try
                {
                    Process.Start(startInfo);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

    }
}