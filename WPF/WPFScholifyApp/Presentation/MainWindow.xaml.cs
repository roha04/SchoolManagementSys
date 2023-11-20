// <copyright file="MainWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WPFScholifyApp
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using WPFScholifyApp.BLL;
    using WPFScholifyApp.DAL.ClassRepository;
    using WPFScholifyApp.DAL.DBClasses;
    using WPFScholifyApp.Presentation;

    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string email = this.EmailTextBox.Text;
            string password = this.PasswordBox.Password;
            var role = ((ComboBoxItem)this.RoleComboBox.SelectedItem).Content;
            if (role != null)
            {
                string selectedRole = role.ToString() !;
                UserService userService = new UserService(new GenericRepository<User>());
                User authenticatedUser = userService.Authenticate(email, password, selectedRole);
                User authenticatedEmail = userService.AuthenticateEmail(email);
                User authenticatedPassword = userService.AuthenticatePassword(password);

                if (authenticatedUser != null)
                {
                    if (selectedRole == "учень")
                    {
                        PupilWindow pupilWindow = new PupilWindow();
                        pupilWindow.FirstNameTextBlock.Text = userService.Authenticate(email, password, selectedRole).FirstName;
                        pupilWindow.LastNameTextBlock.Text = userService.Authenticate(email, password, selectedRole).LastName;
                        pupilWindow.Show();
                    }
                    else if (selectedRole == "батьки")
                    {
                        ParentWindow parentWindow = new ParentWindow();
                        parentWindow.FirstNameTextBlock.Text = userService.Authenticate(email, password, selectedRole).FirstName;
                        parentWindow.LastNameTextBlock.Text = userService.Authenticate(email, password, selectedRole).LastName;
                        parentWindow.Show();
                    }
                    else if (selectedRole == "вчитель")
                    {
                        TeacherWindow teacherWindow = new TeacherWindow();
                        teacherWindow.FirstNameTextBlock.Text = userService.Authenticate(email, password, selectedRole).FirstName;
                        teacherWindow.LastNameTextBlock.Text = userService.Authenticate(email, password, selectedRole).LastName;
                        teacherWindow.Show();
                    }

                    this.Close();
                }
                else
                {
                    string errorMessage = "Помилка автентифікації. Перевірте введені дані.";

                    if (authenticatedEmail == null)
                    {
                        errorMessage = "Неправильно введенна електрона пошта.";
                    }
                    else if (authenticatedPassword == null)
                    {
                        errorMessage = "Неправильно введений пароль.";
                    }
                    else
                    {
                        errorMessage = "Неправильно вибрана роль.";
                    }

                    MessageBox.Show(errorMessage, "Authentication Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                throw new InvalidOperationException("Role is null");
            }
        }
    }
}
