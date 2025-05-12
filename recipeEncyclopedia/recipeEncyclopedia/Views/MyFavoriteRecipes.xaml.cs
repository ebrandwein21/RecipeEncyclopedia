using recipeEncyclopedia.Data;
using recipeEncyclopedia.Models.recipeEncyclopedia.Models;
using recipeEncyclopedia.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for IngredientWindow.xaml
    /// </summary>
    public partial class MyFavoriteRecipes : Window
    {

        public ObservableCollection<recipeEncyclopedia.Models.Recipe> favoriteRecipe { get; set; } = new ObservableCollection<recipeEncyclopedia.Models.Recipe>();
        public MyFavoriteRecipes()
        {
            InitializeComponent();
            LoadFavorites();
            DataContext = this;
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

        private void Favorites_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void DeleteFavorite_Click(object sender, RoutedEventArgs e)
        {

            if (informationBox.SelectedItem is recipeEncyclopedia.Models.Recipe selectedRecipeIngredient)
            {
               ViewModel.RecipeViewModel.favoriteRecipe.Remove(selectedRecipeIngredient);
            }
        }



        private void EditFavorite_Click(object sender, RoutedEventArgs e)
        {
            var selectedRecipe = informationBox.SelectedItem as Recipe;

            if (selectedRecipe == null)
            {
                MessageBox.Show("Please select a recipe to edit.");
                return;
            }

            var editWindow = new EditFavoriteRecipe(selectedRecipe);
            editWindow.Show();
            this.Close(); // Or use .Hide() if you want to return here later
        }


        private void AddToShoppingList_Click(object sender, RoutedEventArgs e)
        {
            var selectedRecipe = informationBox.SelectedItem as recipeEncyclopedia.Models.Recipe;
            var user = AppSession.CurrentUser;

            if (selectedRecipe == null)
            {
                MessageBox.Show("Please select a recipe first.");
                return;
            }

            if (user == null)
            {
                MessageBox.Show("You must be logged in to use the shopping list.");
                return;
            }

            var shoppingService = new ShoppingListService();
            var userLists = shoppingService.GetByUser(user.Id);
            var currentList = userLists.FirstOrDefault();

            if (currentList == null)
            {
                currentList = new ShoppingListModel
                {
                    UserId = user.Id,
                    Items = new List<Ingredient>()
                };
            }

            foreach (var ingredient in selectedRecipe.Ingredients)
            {
                bool alreadyInList = currentList.Items.Any(i =>
                    i.Name == ingredient.Name &&
                    i.MeasurementType == ingredient.MeasurementType &&
                    Math.Abs(i.Amount - ingredient.Amount) < 0.001);

                if (!alreadyInList)
                    currentList.Items.Add(ingredient);
            }

            if (string.IsNullOrEmpty(currentList.Id))
                shoppingService.Add(currentList);
            else
                shoppingService.Update(currentList.Id, currentList);

            MessageBox.Show($"{selectedRecipe.Name} ingredients added to your shopping list.");
        }

        private void LoadFavorites()
        {
            var user = AppSession.CurrentUser;
            if (user == null)
            {
                MessageBox.Show("You must be logged in to view favorites.");
                return;
            }
            
            var recipeService = new RecipeService();
            var userRecipeService = new UserRecipeService();

            var favoriteLinks = userRecipeService.GetByUserId(user.Id);
            var recipeIds = favoriteLinks.Select(f => f.RecipeId).ToList();

            var recipes = recipeService.GetByIds(recipeIds);

            ViewModel.RecipeViewModel.favoriteRecipe.Clear();
            foreach (var recipe in recipes)
            {
                ViewModel.RecipeViewModel.favoriteRecipe.Add(recipe);
            }

            informationBox.ItemsSource = ViewModel.RecipeViewModel.favoriteRecipe;
        }


    }
}
