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

            _recipes = new List<Recipe>
            {
                new Recipe
                {

                    Name = "Miso Soup",
                    Ingredients = new List<string> { "Miso", "Tofu", "Green Onion", "Seaweed", "kelp", "shittake mushrooms"},
                    Ingredient = "broth",
                    MeasurementType = "cups",
                    MeasurementAmount = 4,
                    Serving = 2,
                    Allergen = "none",
                    Instructions = "make the dashi broth, add tofu, mushroom and kelp, mix miso into a paste, finish and serve with chopped green onion",
                    Keywords = "vegan, asian, dinner",
                    Categories = new List<int> { 1 },
                    TotalTime = 75
                },
                new Recipe
                {
                Name = "Chickpea Salad",
                Ingredients = new List<string> { "Chickpeas", "Tahini", "Mustard", "Celery", "Red Onion", "Lemon Juice" },
                Ingredient = "ChickPeas",
                Allergen = "dairy",
                MeasurementAmount = 200,
                Serving = 1,
                MeasurementType = "grams",
                Instructions = "Mash Chickpeas with diced celery and red onion, and rest of ingredients.",
                Keywords = "vegan, lunch, healthy",
                Categories = new List<int> { 3 },
                TotalTime = 20
                },
            };
            VeganRecipeList.ItemsSource = _recipes;
        }

        private void VeganRecipeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          
                var selectedRecipe = VeganRecipeList.SelectedItem as Recipe;
                if (selectedRecipe != null)
                {
                    string details = $"{selectedRecipe.Name} has the ingredients: {string.Join(',', selectedRecipe.Ingredients)}" +
                        $" \n \n use {selectedRecipe.MeasurementAmount} {selectedRecipe.MeasurementType} of {selectedRecipe.Ingredient}. The recipe serves {selectedRecipe.Serving} and takes {selectedRecipe.TotalTime} Minutes. " +
                        $"\n \n Here are the instructions: {selectedRecipe.Instructions}";

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
            favorites.Show();
            this.Close();
        }

    }
}
