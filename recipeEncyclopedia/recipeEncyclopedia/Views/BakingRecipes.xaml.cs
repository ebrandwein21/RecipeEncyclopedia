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
using recipeEncyclopedia.Models.recipeEncyclopedia.Models;


namespace recipeEncyclopedia.Views
{
    /// <summary>
    /// Interaction logic for BakingRecipes.xaml
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
            _recipes = _recipeService.GetByCategory(5);

            BakingRecipeList.ItemsSource = _recipes;

           
        }


        private void BakingRecipeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedRecipe = BakingRecipeList.SelectedItem as Recipe;
            if (selectedRecipe != null)
            {
                if (selectedRecipe.Ingredients != null && selectedRecipe.Ingredients.Any())
                {
                    string ingredientList = string.Join(",\n", selectedRecipe.Ingredients.Select(i =>
                        $"{i.Amount} {i.MeasurementType} {i.Name} (Allergen: {i.Allergen})"));

                    string details = $"{selectedRecipe.Name} has the ingredients:\n\n{ingredientList}" +
                        $"\n\nThis recipe serves {selectedRecipe.Serving} and takes {selectedRecipe.TotalTime} minutes." +
                        $"\n\nInstructions:\n{selectedRecipe.Instructions}";

                    BakingDetailsText.Text = details;
                }
                else
                {
                    BakingDetailsText.Text = $"{selectedRecipe.Name} has no ingredients listed.\n\nInstructions:\n{selectedRecipe.Instructions}";
                }
            }
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
