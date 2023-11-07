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
using WPFScholifyApp.BLL;
using WPFScholifyApp.DAL.ClassRepository;
using WPFScholifyApp.DAL.DBClasses;

namespace WPFScholifyApp
{
    /// <summary>
    /// Interaction logic for PupilWindow.xaml
    /// </summary>
    public partial class PupilWindow : Window
    {
        private bool infoDisplayed = false;
        public PupilWindow()
        {
            InitializeComponent();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            this.Close();
        }

        private void PrivateInfoButton_Click(object sender, RoutedEventArgs e)
        {
            if (infoDisplayed)
            {
                return; 
            }

            TextBlock titleLabel = new TextBlock
            {
                Text = "Приватна інформація",
                FontSize = 16,
                Foreground = new SolidColorBrush(Colors.DarkBlue),
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(210, 30, 0, 10)
            };
            InfoPanel.Children.Add(titleLabel);

            UserService userService = new UserService(new GenericRepository<User>());
            string name = FirstNameTextBlock.Text;
            string surname = LastNameTextBlock.Text;
            User pupil = userService.GetInfoByNameSurname(name, surname);

            if (pupil != null)
            {
                TextBlock studentInfo = new TextBlock
                {
                    Text = $"Ім'я:\t\t {pupil.FirstName}\n\nПрізвище:\t {pupil.LastName}\n\nПо батькові:\t {pupil.MiddleName}\n\nСтать:\t\t {pupil.Gender}" +
                    $"\n\nДата народження: {pupil.Birthday.ToString("dd.MM.yyyy")}\n\nАдреса:\t\t {pupil.Address}\n\nТелефон:\t {pupil.PhoneNumber}",
                    FontSize = 14,
                    Foreground = new SolidColorBrush(Colors.DarkBlue),
                    Margin = new Thickness(210, 0, 0, 10)
                };
                InfoPanel.Children.Add(studentInfo);
                infoDisplayed = true;
            }
        }

        private void JournalButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ScheduleButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AnnouncementsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ChatButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
