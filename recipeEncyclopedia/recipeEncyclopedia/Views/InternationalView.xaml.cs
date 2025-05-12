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
    public partial class InternationalView : Window
    {
        private readonly MongoContext _context = new MongoContext();
        private readonly RecipeService _recipeService = new RecipeService();
        private readonly UserRecipeService _userRecipeService = new UserRecipeService();

        private List<Recipe> _recipes = new List<Recipe>();

        public InternationalView()
        {
            InitializeComponent();
            LoadInternationalRecipes();
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

        private void LoadInternationalRecipes()
        {

            //load recipe hubs not working, getbycategory throwing null 

            // Assuming Category ID 3 = Dinner
            _recipes = _recipeService.GetByCategory(6);

            /*_recipes = new List<Recipe>
            {
                new Recipe
                {

                    Name = "Fried Rice",
                    Ingredients = new List<string> { "Rice", "Soy Sauce", "Teriyaki", "Chicken", "Peas", "carrots"},
                    Ingredient = "Rice",
                    MeasurementType = "cups",
                    MeasurementAmount = 1.5,
                    Serving = 3,
                    Allergen = "none",
                    Instructions = "cook veggies, chicken and sauces in a wok at 400f, cook rice in pot or 2 cups of water.",
                    Keywords = "asian, dinner",
                    Categories = new List<int> { 1 },
                    TotalTime = 25
                },
                new Recipe
                {
                Name = "Tacos",
                Ingredients = new List<string> { "cheese", "Chicken", "tomato", "onion", "avocado", "lettuce", "jalapeno", "cilantro", "soft shells" },
                Ingredient = "Chicken",
                Allergen = "dairy",
                MeasurementAmount = 200,
                Serving = 2,
                MeasurementType = "grams",
                Instructions = "Put Chicken In Crock Pot, prep salsa by cutting up veggies, put chicken in soft shells.",
                Keywords = "Hispanic, Long, dinner",
                Categories = new List<int> { 3 },
                TotalTime = 180
                },

                new Recipe
                {
                    Name = "falaffel Gyro Sandwich",
                    Ingredients = new List<string> { "Gyro", "Tzaiki", "onion", "tortilla", "fallafel"},
                    Ingredient = "Gyro",
                    Allergen = "none",
                    MeasurementAmount = .5,
                    Serving = 1,
                    MeasurementType = "pounds",
                    Instructions = "cook gyro on grill or stovetop, put cooked fallafel in tortilla wrap with falafel, onion and tzaiki sauce.",
                    Keywords = "medditeranian, dinner, quick",
                    Categories = new List<int> { 3 },
                    TotalTime = 10
                },
                new Recipe
                {
                    Name = "Chicken Curry ",
                    Ingredients = new List<string> { "Chicken", "Coconut Milk", "Curry Powder", "Chickpeas"},
                    Ingredient = "Chicken",
                    Allergen = "Coconut",
                    MeasurementAmount = 1.0,
                    Serving = 3,
                    MeasurementType = "pounds",
                    Instructions = "cook chicken with curry powder and chickpeas, let simmer on stovetop, adding coconut milk.",
                    Keywords = "south asian, dinner, spicy",
                    Categories = new List<int> { 3 },
                    TotalTime = 30
                }
            };*/
            InternationalRecipeList.ItemsSource = _recipes;
        }
    

        private void InternationalRecipeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        var selectedRecipe = InternationalRecipeList.SelectedItem as Recipe;
        if (selectedRecipe != null)
        {
            string details = $"{selectedRecipe.Name} has the ingredients: {string.Join(',', selectedRecipe.Ingredients)}" +
                $" \n \n use {selectedRecipe.MeasurementAmount} {selectedRecipe.MeasurementType} of {selectedRecipe.Ingredient}. The recipe serves {selectedRecipe.Serving} and takes {selectedRecipe.TotalTime} Minutes. " +
                $"\n \n Here are the instructions: {selectedRecipe.Instructions}";

            InternationalDetailsText.Text = details;
        }
    }
        

        private void AddToFavorites_Click(object sender, RoutedEventArgs e)
        {
            var selectedRecipe = InternationalRecipeList.SelectedItem as Recipe;
            var user = AppSession.CurrentUser;

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

            // prevent duplicates
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
        }

    }
}
