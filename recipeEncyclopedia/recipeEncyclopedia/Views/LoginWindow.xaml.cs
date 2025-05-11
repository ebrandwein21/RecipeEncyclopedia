using System.Linq;
using System.Windows;
using recipeEncyclopedia.Models;
using recipeEncyclopedia.Data;
using MongoDB.Driver;

namespace recipeEncyclopedia
{
    public partial class LoginWindow : Window
    {
        private readonly MongoContext _context = new MongoContext();

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameBox.Text;
            var password = PasswordBox.Password;

            var user = _context.Users
                .Find(u => u.Username == username && u.Password == password)
                .FirstOrDefault();

            if (user != null)
            {
                // Successful login
                AppSession.CurrentUser = user; // Save logged-in user globally
                MainWindow mainWindow = new MainWindow(); 
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameBox.Text;
            var password = PasswordBox.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both username and password.");
                return;
            }

            var existingUser = _context.Users
                .Find(u => u.Username == username)
                .FirstOrDefault();

            if (existingUser != null)
            {
                MessageBox.Show("Username already exists.");
                return;
            }

            var newUser = new User
            {
                Username = username,
                Password = password
            };

            _context.Users.InsertOne(newUser);

            MessageBox.Show("Registration successful! You can now log in.");
        }
    }
}
