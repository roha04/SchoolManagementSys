// <copyright file="LookUsers.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WPFScholifyApp.Presentation
{
    using System.Windows;
    using WPFScholifyApp.BLL;
    using WPFScholifyApp.DAL.DBClasses;

    /// <summary>
    /// Interaction logic for LookUsers.xaml.
    /// </summary>
    public partial class LookUsers : Window
    {
        private UserService userService;
        private WindowService windowService;
        private MainWindow mainWindow;

        public bool ShowAllTeachers { get; set; } = false;

        public bool ShowAllClasses { get; set; } = false;

        public User? CurrentUser { get; set; }

        public int CurrentClassId { get; set; }

        public int CurrentPupilId { get; set; }

        public int ParentId { get; set; }

        public LookUsers(
            UserService userService,
            WindowService windowService,
            MainWindow mainWindow)
        {
            this.userService = userService;
            this.windowService = windowService;
            this.mainWindow = mainWindow;
            this.InitializeComponent();
        }

        private void SaveUser(object sender, RoutedEventArgs e)
        {
            this.windowService.Show<AdminWindow>(window =>
            {
                var user = new User
                {
                    Id = this.CurrentUser!.Id,
                    Email = this.Email.Text,
                    Password = this.Password.Text,
                    FirstName = this.FirstName.Text,
                    LastName = this.LastName.Text,
                    MiddleName = this.MiddleName.Text,
                    Gender = this.Gender.Text,
                    Birthday = this.Birthday.SelectedDate!.Value.ToUniversalTime(),
                    Address = this.Adress.Text,
                    PhoneNumber = this.PhoneNumber.Text,
                    Role = this.CurrentUser.Role,
                };

                this.userService.SaveUser(user);

                window.DeleteFromAdminPanels();

                if (this.CurrentUser.Role == "учень")
                {
                    if (this.ShowAllTeachers)
                    {
                        window.ShowAllTeachers();
                    }

                    if (this.ShowAllClasses)
                    {
                        window.ShowAllPupilsForClassId(this.CurrentClassId);
                    }
                }

                if (this.CurrentUser.Role == "батьки")
                {
                    window.ShowAllPuplis();
                    window.ShowParentsForPupilId(this.CurrentPupilId);
                }

                if (this.CurrentUser.Role == "вчитель")
                {
                    window.ShowAllTeachers();
                }

                window.UpdateAdminPanels();
            });

            this.Close();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
