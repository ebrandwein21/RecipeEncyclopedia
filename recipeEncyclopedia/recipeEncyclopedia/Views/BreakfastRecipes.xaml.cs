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
    /// Interaction logic for BreakfastRecipes.xaml
    /// </summary>
    /// 
    public partial class BreakfastRecipes : Window
    {
        private readonly MongoContext _context = new MongoContext();
        private readonly RecipeService _recipeService = new RecipeService();
        private readonly UserRecipeService _userRecipeService = new UserRecipeService();

        private List<Recipe> _recipes = new List<Recipe>();
        public BreakfastRecipes()
        {
            InitializeComponent();
            LoadBreakfastRecipes();
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

        private void LoadBreakfastRecipes()
        {
            // Assuming Category ID 1 = Breakfast
            // _recipes = _recipeService.GetByCategory(1);

            //hardcoded recipes for now below
            _recipes = new List<Recipe>
            {
                new Recipe
                {
                
                    Name = "Pancakes",
                    Ingredients = new List<string> { "flour", "milk", "eggs", "sugar", "baking powder" },
                    Ingredient = "flour",
                    MeasurementType = "cups",
                    MeasurementAmount = 1.0,
                    Serving = 2,
                    Allergen = "dairy",
                    Instructions = "Mix all ingredients. Pour batter on griddle. Flip when bubbles form.",
                    Keywords = "pancake, breakfast, sweet",
                    Categories = new List<int> { 1 },
                    TotalTime = 20
                },
                new Recipe
                {
                    Name = "French Toast",
                    Ingredients = new List<string> { "Bread", "Eggs", "Milk", "Cinnamon", "Vanilla"},
                    Ingredient = "Eggs",
                    Allergen = "dairy",
                    MeasurementAmount = .5,
                    Serving = 3,
                    MeasurementType = "dozen",
                    Instructions = "Cut thick pieces of bread, Crack eggs into flour and whisk, add cinnamon and vanilla and pour on bread, bake at 425f for 25 minutes.",
                    Keywords = "Breakfast, Sweet",
                    Categories = new List<int> { 3 },
                    TotalTime = 30
                },

                new Recipe
                {
                    Name = "Omellette",
                    Ingredients = new List<string> { "eggs", "tomato", "onion", "jalapeno", "cheese", "ham"},
                    Ingredient = "eggs",
                    Allergen = "dairy",
                    MeasurementAmount = .25,
                    Serving = 1,
                    MeasurementType = "dozen",
                    Instructions = "crack egg on pan, cut up veggies/ham into small chunks and pour into eggs with cheese.",
                    Keywords = "healthy, breakfast, quick",
                    Categories = new List<int> { 3 },
                    TotalTime = 10
                }
            };
            BreakfastRecipeList.ItemsSource = _recipes;
        }
    

        private void BreakfastRecipeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        var selectedRecipe = BreakfastRecipeList.SelectedItem as Recipe;
        if (selectedRecipe != null)
        {
            string details = $"{selectedRecipe.Name} has the ingredients: {string.Join(',', selectedRecipe.Ingredients)}" +
                $" \n \n use {selectedRecipe.MeasurementAmount} {selectedRecipe.MeasurementType} of {selectedRecipe.Ingredient}. The recipe serves {selectedRecipe.Serving} and takes {selectedRecipe.TotalTime} Minutes. " +
                $"\n \n Here are the instructions: {selectedRecipe.Instructions}";

            BreakfastDetailsText.Text = details;
        }
    }

        private void AddToFavorites_Click(object sender, RoutedEventArgs e)
        {
            var selectedRecipe = BreakfastRecipeList.SelectedItem as Recipe;
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
            favorites.Show();
            this.Close();
        }
    }
}
