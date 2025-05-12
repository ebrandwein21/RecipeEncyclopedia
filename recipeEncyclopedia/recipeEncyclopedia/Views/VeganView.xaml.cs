using System;
using recipeEncyclopedia.Models;
using recipeEncyclopedia.Data;
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


namespace recipeEncyclopedia.Views
{
    /// <summary>
    /// Interaction logic for DinnerRecipes.xaml
    /// </summary>
    public partial class VeganView : Window
    {
        private readonly MongoContext _context = new MongoContext();
        private readonly RecipeService _recipeService = new RecipeService();
        private readonly UserRecipeService _userRecipeService = new UserRecipeService();

        private List<Recipe> _recipes = new List<Recipe>();

        public VeganView()
        {
            InitializeComponent();
            LoadVeganRecipes();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
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

        private void LoadVeganRecipes()
        {

            //load recipe hubs not working, getbycategory throwing null 

            // Assuming Category ID 3 = Dinner
            _recipes = _recipeService.GetByCategory(4);

            VeganRecipeList.ItemsSource = _recipes;
        }

        private void VeganRecipeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedRecipe = VeganRecipeList.SelectedItem as Recipe;
            if (selectedRecipe != null)
            {
                string details = $"{selectedRecipe.Name}\n\n" +
                        $"Total Time: {selectedRecipe.TotalTime} minutes\n" +
                        $"Servings: {selectedRecipe.Serving}\n\n" +
                        $"Ingredients:\n - {string.Join("\n - ", selectedRecipe.Ingredients)}\n\n" +
                        $"Instructions:\n{selectedRecipe.Instructions}\n\n" +
                        $"Keywords: {selectedRecipe.Keywords}\n\n";

                    VeganDetailsText.Text = details;
            }
        }

        private void AddToFavorites_Click(object sender, RoutedEventArgs e)
        {
            var selectedRecipe = VeganRecipeList.SelectedItem as Recipe;
            var user = AppSession.CurrentUser;
            MyFavoriteRecipes favorites = new MyFavoriteRecipes();

            if (selectedRecipe == null)
            {
                MessageBox.Show("Please select a recipe first.");
                return;
            }

            if (user == null)
            {
                MessageBox.Show("You must be logged in to add favorites.");
                return;
            }

            // Optional: prevent duplicates
            var existing = _userRecipeService
                .GetByUserId(user.Id)
                .FirstOrDefault(ur => ur.RecipeId == selectedRecipe.Id);

            if (existing != null)
            {
                MessageBox.Show("Recipe is already in your favorites.");
                return;
            }

            var newFavorite = new UserRecipe
            {
                UserId = user.Id,
                RecipeId = selectedRecipe.Id
            };

            _userRecipeService.Add(newFavorite);
            MessageBox.Show($"'{selectedRecipe.Name}' added to your favorites.");
            //favorites.Show();
            //this.Close();
        }

    }
}
