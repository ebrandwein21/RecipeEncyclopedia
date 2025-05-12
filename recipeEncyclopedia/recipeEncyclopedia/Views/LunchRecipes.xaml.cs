using recipeEncyclopedia.Data;
using recipeEncyclopedia.Models;
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
    /// Interaction logic for LunchRecipes.xaml
    /// </summary>
    public partial class LunchRecipes : Window
    {
        private readonly MongoContext _context = new MongoContext();
        private readonly RecipeService _recipeService = new RecipeService();
        private readonly UserRecipeService _userRecipeService = new UserRecipeService();

        private List<Recipe> _recipes;
        public LunchRecipes()
        {
            InitializeComponent();
            LoadLunchRecipes();
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

        private void LoadLunchRecipes()
        {

            //load recipe hubs not working, getbycategory throwing null 

            // Assuming Category ID 3 = Dinner
            //_recipes = _recipeService.GetByCategory(3);

            // DinnerRecipeList.ItemsSource = _recipes;

            //hardcoded recipes for now below
            _recipes = new List<Recipe>
        {
            new Recipe
            {
                    Name = "Chicken Salad",
                    Ingredients = new List<string> { "chicken breast", "lettuce", "tomato", "cucumber", "olive oil" },
                    Ingredient = "chicken",
                    Allergen = "none",
                    MeasurementAmount = 200,
                    Serving = 4,
                    MeasurementType = "grams",
                    Instructions = "Grill chicken. Chop veggies. Toss everything with olive oil.",
                    Keywords = "chicken, salad, healthy",
                    Categories = new List<int> { 2 },
                    TotalTime = 25
            },
            new Recipe
            {
                    Name = "CheeseBurger",
                    Ingredients = new List<string> { "cheese", "ground beef", "tomato", "onion", "pickle", "lettuce" },
                    Ingredient = "ground beef",
                    Allergen = "dairy",
                    MeasurementAmount = 1,
                    Serving = 2,
                    MeasurementType = "pound",
                    Instructions = "Smush Ground Beef into patties, Cut up vegetables, Cook On Grill, flipping patties halfway through.",
                    Keywords = "American, Fast",
                    Categories = new List<int> { 3 },
                    TotalTime = 15
            },
        };
            LunchRecipeList.ItemsSource = _recipes;
        }

        private void LunchRecipeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedRecipe = LunchRecipeList.SelectedItem as Recipe;
            if (selectedRecipe != null)
            {
                string details = $"{selectedRecipe.Name} has the ingredients: {string.Join(',', selectedRecipe.Ingredients)}" +
                    $" \n \n use {selectedRecipe.MeasurementAmount} {selectedRecipe.MeasurementType} of {selectedRecipe.Ingredient}. The recipe serves {selectedRecipe.Serving} and takes {selectedRecipe.TotalTime} Minutes. " +
                    $"\n \n Here are the instructions: {selectedRecipe.Instructions}";

                LunchDetailsText.Text = details;
            }
        }

        private void AddToFavorites_Click(object sender, RoutedEventArgs e)
        {
            var selectedRecipe = LunchRecipeList.SelectedItem as Recipe;
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
    }
}
