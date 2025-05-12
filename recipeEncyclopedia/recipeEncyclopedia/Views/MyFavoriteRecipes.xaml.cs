using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using recipeEncyclopedia.Models;
using recipeEncyclopedia.Data;

namespace recipeEncyclopedia.Views
{
    public partial class MyFavoriteRecipes : Window
    {
        private readonly UserRecipeService _userRecipeService = new UserRecipeService();
        private readonly RecipeService _recipeService = new RecipeService();

        private List<Recipe> _favoriteRecipes = new List<Recipe>();

        public MyFavoriteRecipes()
        {
            InitializeComponent();
            LoadFavorites();
        }

        private void LoadFavorites()
        {
            var user = AppSession.CurrentUser;

            if (user == null)
            {
                MessageBox.Show("You must be logged in to view favorites.");
                return;
            }

            var userRecipes = _userRecipeService.GetByUserId(user.Id);
            var recipeIds = userRecipes.Select(ur => ur.RecipeId).ToList();

            _favoriteRecipes = recipeIds
                .Select(id => _recipeService.GetById(id))
                .Where(r => r != null)
                .ToList();

            //informationBox.ItemsSource = _favoriteRecipes;
            if (_favoriteRecipes.Count == 0)
            {
                //MessageBox.Show("No recipes found in favorites.");
            }
            else
            {
                MessageBox.Show("Loaded " + _favoriteRecipes.Count + " favorite recipes.");
                informationBox.ItemsSource = _favoriteRecipes;
            }

        }

        private void Favorites_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Optional: display recipe details elsewhere
        }

        private void DeleteFavorite_Click(object sender, RoutedEventArgs e)
        {
            var selectedRecipe = informationBox.SelectedItem as Recipe;
            var user = AppSession.CurrentUser;

            if (selectedRecipe == null || user == null)
            {
                MessageBox.Show("Please select a recipe to delete.");
                return;
            }

            var userRecipe = _userRecipeService
                .GetByUserId(user.Id)
                .FirstOrDefault(ur => ur.RecipeId == selectedRecipe.Id);

            if (userRecipe != null)
            {
                _userRecipeService.Delete(userRecipe.Id);
                MessageBox.Show($"Removed '{selectedRecipe.Name}' from favorites.");
                LoadFavorites();
            }
        }

        private void EditFavorite_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Edit functionality not implemented yet.");
        }

        private void Login_Click(object sender, RoutedEventArgs e)
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
    }
}
