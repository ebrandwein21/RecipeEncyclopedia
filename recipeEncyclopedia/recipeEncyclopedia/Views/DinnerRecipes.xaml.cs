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
    public partial class DinnerRecipes : Window
    {
        private readonly MongoContext _context = new MongoContext();
        private readonly RecipeService _recipeService = new RecipeService();
        private readonly UserRecipeService _userRecipeService = new UserRecipeService();
        private List<Recipe> _recipes = new List<Recipe>();

        public DinnerRecipes()
        {
            InitializeComponent();
            LoadDinnerRecipes();
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

        private void LoadDinnerRecipes()
        {
            _recipes = _recipeService.GetByCategory(3);

             DinnerRecipeList.ItemsSource = _recipes;
        }
   
        private void DinnerRecipeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedRecipe = DinnerRecipeList.SelectedItem as Recipe;
            if (selectedRecipe != null)
            {
                if (selectedRecipe.Ingredients != null && selectedRecipe.Ingredients.Any())
                {
                    string ingredientList = string.Join(",\n", selectedRecipe.Ingredients.Select(i =>
                        $"{i.Amount} {i.MeasurementType} {i.Name} (Allergen: {i.Allergen})"));

                    string details = $"{selectedRecipe.Name} has the ingredients:\n\n{ingredientList}" +
                        $"\n\nThis recipe serves {selectedRecipe.Serving} and takes {selectedRecipe.TotalTime} minutes." +
                        $"\n\nInstructions:\n{selectedRecipe.Instructions}";

                    DinnerRecipeDetailsText.Text = details;
                }
                else
                {
                    DinnerRecipeDetailsText.Text = $"{selectedRecipe.Name} has no ingredients listed.\n\nInstructions:\n{selectedRecipe.Instructions}";
                }
            }
        }

        private void AddToFavorites_Click(object sender, RoutedEventArgs e)
        {
            var selectedRecipe = DinnerRecipeList.SelectedItem as Recipe;
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

            var newFavorite = new UserRecipe
            {
                UserId = user.Id,
                RecipeId = selectedRecipe.Id
            };

            ViewModel.RecipeViewModel.favoriteRecipe.Add(selectedRecipe);

            _userRecipeService.Add(newFavorite);
            MessageBox.Show($"'{selectedRecipe.Name}' added to your favorites.");
            favorites.Show();
            this.Close();
        }

        private void AllRecipes_Click(object sender, RoutedEventArgs e)
        {
            AllRecipesView allRecipes = new AllRecipesView();
            allRecipes.Show();
        }

        private void FavoriteRecipe_Click(object sender, RoutedEventArgs e)
        {
            MyFavoriteRecipes favoriteRecipes = new MyFavoriteRecipes();
            favoriteRecipes.Show();
        }

        private void Categories_Click(object sender, RoutedEventArgs e)
        {
            RecipeMenu menu = new RecipeMenu();
            menu.Show();
            this.Close();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {

            AppSession.CurrentUser = null;
            LoginWindow login = new LoginWindow();
            login.Show();
            this.Close();
        }

        private void EnterRecipe_Click(object sender, RoutedEventArgs e)
        {
            EnterRecipe enter = new EnterRecipe();
            enter.Show();
        }
    }
}
