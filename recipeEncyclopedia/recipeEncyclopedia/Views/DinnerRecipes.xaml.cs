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

            //load recipe hubs not working, getbycategory throwing null 

            // Assuming Category ID 3 = Dinner
            //_recipes = _recipeService.GetByCategory(3);

            // DinnerRecipeList.ItemsSource = _recipes;

            //hardcoded recipes for now below
            _recipes = new List<Recipe>
        {
            new Recipe
            {
                Name = "Spaghetti Bolognese",
                Ingredients = new List<string> { "spaghetti", "ground beef", "tomato sauce", "onion", "garlic" },
                Ingredient = "spaghetti",
                Allergen = "none",
                MeasurementAmount = 2,
                Serving = 4,
                MeasurementType = "cups",
                Instructions = "Boil spaghetti. Cook beef. Mix with sauce. Combine and serve.",
                Keywords = "italian, pasta",
                Categories = new List<int> { 3 },
                TotalTime = 45
            },
            new Recipe
            {
                Name = "Supreme Pizza",
                Ingredients = new List<string> { "cheese", "pepperoni", "tomato", "dough", "onion", "pepper", "sausage" },
                Ingredient = "dough",
                Allergen = "dairy",
                MeasurementAmount = .5,
                Serving = 4,
                MeasurementType = "pound",
                Instructions = "Shape the dough into flying saucer shape, spread pizza sauce evenly, add layer of mozzerella, top with the veggies and meats",
                Keywords = "Italian, Comfort, Dinner",
                Categories = new List<int> { 3 },
                TotalTime = 30
            },
            new Recipe
            {
                Name = "Mac And Cheese",
                Ingredients = new List<string> { "cheese", "Elbow Macaroni", "Milk", "Flour", "pasta"},
                Ingredient = "cheese",
                Allergen = "dairy",
                MeasurementAmount = 2.0,
                Serving = 4,
                MeasurementType = "cups",
                Instructions = "cook the pasta, make the roux, add milk and cheese and stir into pot",
                Keywords = "Italian, Comfort, Dinner",
                Categories = new List<int> { 3 },
                TotalTime = 20
            },
        };
            DinnerRecipeList.ItemsSource = _recipes;

    }
   
        private void DinnerRecipeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedRecipe = DinnerRecipeList.SelectedItem as Recipe;
            if (selectedRecipe != null)
            {
                string details = $"{selectedRecipe.Name} has the ingredients: {string.Join(',', selectedRecipe.Ingredients)}" +
                    $" \n \n use {selectedRecipe.MeasurementAmount} {selectedRecipe.MeasurementType} of {selectedRecipe.Ingredient}. The recipe serves {selectedRecipe.Serving} and takes {selectedRecipe.TotalTime} Minutes. " +
                    $"\n \n Here are the instructions: {selectedRecipe.Instructions}";

                DinnerDetailsText.Text = details;
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
