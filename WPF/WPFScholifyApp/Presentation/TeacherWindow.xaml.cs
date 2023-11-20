// <copyright file="TeacherWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WPFScholifyApp.Presentation
{
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

    /// <summary>
    /// Interaction logic for TeacherWindow.xaml.
    /// </summary>
    public partial class TeacherWindow : Window
    {
        private bool infoDisplayed = false;

        public TeacherWindow()
        {
            this.InitializeComponent();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            this.Close();
        }

        private void PrivateInfoButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.infoDisplayed)
            {
                return;
            }

            TextBlock titleLabel = new TextBlock
            {
                Text = "Приватна інформація",
                FontSize = 16,
                Foreground = new SolidColorBrush(Colors.DarkBlue),
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(210, 30, 0, 10),
            };
            this.InfoPanel.Children.Add(titleLabel);

            UserService userService = new UserService(new GenericRepository<User>());
            string name = this.FirstNameTextBlock.Text;
            string surname = this.LastNameTextBlock.Text;
            User teacher = userService.GetInfoByNameSurname(name, surname);

            if (teacher != null)
            {
                TextBlock studentInfo = new TextBlock
                {
                    Text = $"Ім'я:\t\t {teacher.FirstName}\n\nПрізвище:\t {teacher.LastName}\n\nПо батькові:\t {teacher.MiddleName}\n\nСтать:\t\t {teacher.Gender}" +
                    $"\n\nДата народження: {teacher.Birthday:dd.MM.yyyy}\n\nАдреса:\t\t {teacher.Address}\n\nТелефон:\t {teacher.PhoneNumber}",
                    FontSize = 14,
                    Foreground = new SolidColorBrush(Colors.DarkBlue),
                    Margin = new Thickness(210, 0, 0, 10),
                };
                this.InfoPanel.Children.Add(studentInfo);
                this.infoDisplayed = true;
            }
        }

        private void JournalButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void AnnouncementsButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void ScheduleButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void ClassesButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void ChatButton_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
