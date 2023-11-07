using System.Windows;
using System.Windows.Controls;
using WPFScholifyApp.BLL;
using WPFScholifyApp.DAL.ClassRepository;
using WPFScholifyApp.DAL.DBClasses;

namespace WPFScholifyApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string password = PasswordBox.Password;
            string selectedRole = ((ComboBoxItem)RoleComboBox.SelectedItem).Content.ToString();

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

    }
}
