// <copyright file="ParentWindow.xaml.cs" company="PlaceholderCompany">
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
    /// Interaction logic for ParentWindow.xaml.
    /// </summary>
    public partial class ParentWindow : Window
    {
        private bool infoDisplayed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParentWindow"/> class.
        /// </summary>
        public ParentWindow()
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
            User parent = userService.GetInfoByNameSurname(name, surname);

            if (parent != null)
            {
                TextBlock studentInfo = new TextBlock
                {
                    Text = $"Ім'я:\t\t {parent.FirstName}\n\nПрізвище:\t {parent.LastName}\n\nПо батькові:\t {parent.MiddleName}\n\nСтать:\t\t {parent.Gender}" +
                    $"\n\nДата народження: {parent.Birthday:dd.MM.yyyy}\n\nАдреса:\t\t {parent.Address}\n\nТелефон:\t {parent.PhoneNumber}",
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

        private void ChatButton_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}