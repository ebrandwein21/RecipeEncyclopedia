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

        private List<Recipe> _recipes;
        public BreakfastRecipes()
        {
            InitializeComponent();
            LoadBreakfastRecipes();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
            _recipes = _recipeService.GetByCategory(1);

            BreakfastRecipeList.ItemsSource = _recipes;
        }

        private void BreakfastRecipeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedRecipe = BreakfastRecipeList.SelectedItem as Recipe;
            if (selectedRecipe != null)
            {
                string details = $"{selectedRecipe.Name}\n\n" +
                        $"Total Time: {selectedRecipe.TotalTime} minutes\n" +
                        $"Servings: {selectedRecipe.Serving}\n\n" +
                        $"Ingredients:\n - {string.Join("\n - ", selectedRecipe.Ingredients)}\n\n" +
                        $"Instructions:\n{selectedRecipe.Instructions}\n\n" +
                        $"Keywords: {selectedRecipe.Keywords}\n\n";

                BreakfastDetailsText.Text = details;
            }
        }

        private void AddToFavorites_Click(object sender, RoutedEventArgs e)
        {
            var selectedRecipe = BreakfastRecipeList.SelectedItem as Recipe;
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
        }
    }
}
