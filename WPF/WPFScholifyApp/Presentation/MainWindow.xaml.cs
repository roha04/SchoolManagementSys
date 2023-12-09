// <copyright file="MainWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WPFScholifyApp
{
    using System.ComponentModel;
    using System.Windows;
    using WPFScholifyApp.BLL;
    using WPFScholifyApp.DAL.DBClasses;

    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        public User? AuthenticatedUser { get; set; }

        private UserService userService;
        private WindowService windowService;

        public MainWindow(
            UserService userService,
            WindowService windowService)
        {
            this.userService = userService;
            this.windowService = windowService;

            this.Closing += new CancelEventHandler(this.Window_Closing!);
            this.InitializeComponent();
        }

        public void Window_Closing(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string email = this.EmailTextBox.Text;
            string password = this.PasswordBox.Password;
            User authenticatedUser = this.userService.Authenticate(email, password);
            User authenticatedEmail = this.userService.AuthenticateEmail(email);
            User authenticatedPassword = this.userService.AuthenticatePassword(password);

            if (authenticatedUser != null)
            {
                if (authenticatedUser.Role == "учень")
                {
                    this.windowService.Show<PupilWindow>(window =>
                    {
                        window.InitializeComponent();
                        window.AuthenticatedUser = authenticatedUser;
                        window.FirstNameTextBlock.Text = this.userService.Authenticate(email, password).FirstName;
                        window.LastNameTextBlock.Text = this.userService.Authenticate(email, password).LastName;
                    });
                }
                else if (authenticatedUser.Role == "вчитель")
                {
                    this.windowService.Show<TeacherWindow>(window =>
                    {
                        window.InitializeComponent();
                        window.AuthenticatedUser = authenticatedUser;
                        window.FirstNameTextBlock.Text = this.userService.Authenticate(email, password).FirstName;
                        window.LastNameTextBlock.Text = this.userService.Authenticate(email, password).LastName;
                    });
                }
                else if (authenticatedUser.Role == "батьки")
                {
                    this.windowService.Show<ParentsWindow>(window =>
                    {
                        window.AuthenticatedUser = authenticatedUser;
                        window.FirstNameTextBlock.Text = this.userService.Authenticate(email, password).FirstName;
                        window.LastNameTextBlock.Text = this.userService.Authenticate(email, password).LastName;
                        window.Show();
                    });
                }
                else
                {
                    this.windowService.Show<AdminWindow>(window =>
                    {
                        window.InitializeComponent();
                        window.AuthenticatedUser = authenticatedUser;
                        window.FirstNameTextBlock.Text = this.userService.Authenticate(email, password).FirstName;
                        window.LastNameTextBlock.Text = this.userService.Authenticate(email, password).LastName;
                    });
                }

                this.Hide();
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
    }
}
