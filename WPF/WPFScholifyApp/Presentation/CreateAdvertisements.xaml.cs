// <copyright file="CreateAdvertisements.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WPFScholifyApp.Presentation
{
    using System.Windows;
    using WPFScholifyApp.BLL;
    using WPFScholifyApp.DAL.DBClasses;

    /// <summary>
    /// Interaction logic for CreateAdvertisements.xaml.
    /// </summary>
    public partial class CreateAdvertisements : Window
    {
        private AdminService adminService;
        private AdvertisementService advertisementService;
        private WindowService windowService;
        private MainWindow mainWindow;

        public int ClassId { get; set; }

        public CreateAdvertisements(
            AdminService adminService,
            AdvertisementService advertisementService,
            WindowService windowService,
            MainWindow mainWindow)
        {
            this.adminService = adminService;
            this.advertisementService = advertisementService;
            this.windowService = windowService;
            this.mainWindow = mainWindow;
            this.InitializeComponent();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            string name = this.Name1.Text;
            string description = this.Description.Text;
            var advertisement = new Advertisement
            {
                Id = this.adminService.GetNewAdvertisementId(),
                Name = name,
                Description = description,
                ClassId = this.ClassId,
            };

            this.advertisementService.AddAdvertisement(advertisement);
            this.Hide();
            this.windowService.Show<TeacherWindow>(window =>
            {
                window.DeleteFromTeacherPanel();
                window.ShowAllClasses();
                window.ShowAllAdvertisementsForClassId(this.ClassId);
                window.UpdateTeacherPanel();
            });
        }
    }
}
