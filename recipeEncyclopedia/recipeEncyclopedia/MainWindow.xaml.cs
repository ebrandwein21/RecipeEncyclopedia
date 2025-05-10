using recipeEncyclopedia.Models;
using recipeEncyclopedia.Views;
using recipeEncyclopedia.Data;
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
using MongoDB.Driver;

namespace recipeEncyclopedia
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MongoContext _context = new MongoContext();
        public MainWindow()
        {
            InitializeComponent();
            //LoadUsers();
        }

        private void ShoppingList_Click(object sender, RoutedEventArgs e)
        {
            ShoppingList shoppingList = new ShoppingList();
            shoppingList.Show();
            this.Close();
        }

        private void IngredientsList_Click(object sender, RoutedEventArgs e)
        {
            IngredientWindow ingredient = new IngredientWindow();
            ingredient.Show();
            this.Close();
        }

        private void Recipe_Click(object sender, RoutedEventArgs e)
        {
            RecipeMenu recipe = new RecipeMenu();
            recipe.Show();
            this.Close();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void EnterRecipe_Click(object sender, RoutedEventArgs e)
        {
            EnterRecipe featuredRecipe = new EnterRecipe();
            featuredRecipe.Show();
            this.Close();
        }
    }
}