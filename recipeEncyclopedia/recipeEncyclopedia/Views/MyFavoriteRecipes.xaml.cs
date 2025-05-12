using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace recipeEncyclopedia.Views
{
    /// <summary>
    /// Interaction logic for IngredientWindow.xaml
    /// </summary>
    public partial class MyFavoriteRecipes : Window
    {

        public ObservableCollection<recipeEncyclopedia.Models.Recipe> favoriteRecipe { get; set; } = new ObservableCollection<recipeEncyclopedia.Models.Recipe>();
        public MyFavoriteRecipes()
        {
            InitializeComponent();
            informationBox.ItemsSource = ViewModel.RecipeViewModel.favoriteRecipe;

        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            login.Show();

        }
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            MainWindow home = new MainWindow();
            home.Show();
            this.Close();
        }

        private void Favorites_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void DeleteFavorite_Click(object sender, RoutedEventArgs e)
        {
            //delete selected
        }

        private void EditFavorite_Click(object sender, RoutedEventArgs e)
        {
            //edit selected
        }
    }
 }
