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
    public partial class BakingRecipes : Window
    {
        private readonly MongoContext _context = new MongoContext();
        private readonly RecipeService _recipeService = new RecipeService();
        private readonly UserRecipeService _userRecipeService = new UserRecipeService();

        private List<Recipe> _recipes = new List<Recipe>();

        public BakingRecipes()
        {
            InitializeComponent();
            LoadBakingRecipes();
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

        private void LoadBakingRecipes()
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
                    Name = "Chocolate Chip Cookies",
                    Ingredients = new List<string> { "Flour", "Chocolate Chips", "Sugar", "Eggs" },
                    Ingredient = "Flour",
                    Allergen = "Dairy",
                    MeasurementAmount = 3.0,
                    Serving = 6,
                    MeasurementType = "cups",
                    Instructions = "Crack eggs into flour and whisk, add sugar, drop some chocolate chips in after stirring and bake at 425f for 15 minutes.",
                    Keywords = "dessert, comfort",
                    Categories = new List<int> { 3 },
                    TotalTime = 25
                },
                new Recipe
                {
                    Name = "Cupcakes",
                    Ingredients = new List<string> {  "Flour", "Chocolate", "Sugar", "Eggs", "Vanilla", "Frosting" },
                    Ingredient = "Flour",
                    Allergen = "dairy",
                    MeasurementAmount = 2.0,
                    Serving = 8,
                    MeasurementType = "cups",
                    Instructions = "Crack eggs into flour and whisk, add sugar, Chocolate and vanilla, bake at 425f for 25 minutes, add layer of frosting after baking.",
                    Keywords = "dessert, comfort",
                    Categories = new List<int> { 3 },
                    TotalTime = 60
                },

                new Recipe
                {
                    Name = "Banana Bread",
                    Ingredients = new List<string> { "bananas", "Flour", "Sugar", "Chocolate Chips", "Eggs"},
                    Ingredient = "Banana",
                    Allergen = "dairy",
                    MeasurementAmount = .5,
                    Serving = 5,
                    MeasurementType = "pounds",
                    Instructions = "Crack eggs into flour and whisk, add bananas and sugar, add chocolate chips, bake at 425f for 25 minutes.",
                    Keywords = "dessert, fruit",
                    Categories = new List<int> { 3 },
                    TotalTime = 35
                }
            };
            BakingRecipeList.ItemsSource = _recipes;
        }


            private void BakingRecipeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                var selectedRecipe = BakingRecipeList.SelectedItem as Recipe;
                if (selectedRecipe != null)
                {
                    string details = $"{selectedRecipe.Name} has the ingredients: {string.Join(',', selectedRecipe.Ingredients)}" +
                        $" \n \n use {selectedRecipe.MeasurementAmount} {selectedRecipe.MeasurementType} of {selectedRecipe.Ingredient}. The recipe serves {selectedRecipe.Serving} and takes {selectedRecipe.TotalTime} Minutes. " +
                        $"\n \n Here are the instructions: {selectedRecipe.Instructions}";

                    BakingDetailsText.Text = details;
                }
            }
        




        private void AddToFavorites_Click(object sender, RoutedEventArgs e)
        {
            var selectedRecipe = BakingRecipeList.SelectedItem as Recipe;
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
