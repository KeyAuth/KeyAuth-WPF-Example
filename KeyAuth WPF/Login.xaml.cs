using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KeyAuth;

namespace KeyAuth_WPF_Example
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        /// 
        public static api KeyAuthApp = new api(
            name: "", // Application Name
            ownerid: "", // Owner ID
            secret: "", // Application Secret
            version: "" // Application Version /*
           //path: @"Your_Path_Here" // (OPTIONAL) see tutorial here https://www.youtube.com/watch?v=I9rxt821gMk&t=1s
        );

        // VIEW https://keyauth.win/docs to see all of the functions that you can run.

        public Login()
        {
            InitializeComponent();
        }

        private async void Login_Loaded(object sender, RoutedEventArgs e)
        {
            await KeyAuthApp.init();
            MessageBox.Show(KeyAuthApp.response.message + $"\n\n It took {api.responseTime}ms to Initialize.", "KeyAuth Response");
        } 

        private async void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            await KeyAuthApp.login(usernameField.Text, passwordField.Text);
            if (KeyAuthApp.response.success)
            {
                Main main = new Main();
                main.Show();
                this.Close();

                KeyAuthApp.log(usernameField.Text + " Logged In"); // this will send a log to the logs channel https://keyauth.win/app/?page=logs OR to your Discord server if you enabled Discord logs.
            }
            else
            {
                MessageBox.Show(KeyAuthApp.response.message, "KeyAuth Response");
            }
        }

        private async void registerBtn_Click(object sender, RoutedEventArgs e)
        {
            string? email = emailField.Text;
            if (!email.Contains("@"))
            {
                email = null;
            }

            await KeyAuthApp.register(usernameField.Text, passwordField.Text, email);
            if (KeyAuthApp.response.success)
            {
                Main main = new Main();
                main.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show(KeyAuthApp.response.message, "KeyAuth Response");
            }
        }

        private async void upgradeBtn_Click(object sender, RoutedEventArgs e)
        {
            await KeyAuthApp.upgrade(usernameField.Text, licenseField.Text);
            MessageBox.Show(KeyAuthApp.response.message, "KeyAuth Response");
        }

        private async void licenseBtn_Click(object sender, RoutedEventArgs e)
        {
            await KeyAuthApp.license(licenseField.Text);
            if (KeyAuthApp.response.success)
            {
                Main main = new Main();
                main.Show();
                this.Close();

                KeyAuthApp.log(licenseField.Text + " Logged In");
            }
            else
            {
                MessageBox.Show(KeyAuthApp.response.message, "KeyAuth Response");
            }
        }

        private async void forgotPasswordBtn_Click(object sender, RoutedEventArgs e)
        {
            await KeyAuthApp.forgot(usernameField.Text, emailField.Text);
            MessageBox.Show(KeyAuthApp.response.message);
        }
    }
}