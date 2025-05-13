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
using System.IO.Packaging;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Diagnostics;

namespace recipeEncyclopedia.Views
{
    /// <summary>
    /// Interaction logic for IngredientWindow.xaml
    /// </summary>
    /// 

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

        private void ExportToPDF_Click(object sender, RoutedEventArgs e)
        {
            //https://stackoverflow.com/questions/12391244/pdfsharp-getting-started
            var user = AppSession.CurrentUser;

            var selectedRecipe = informationBox.SelectedItem as Recipe;

            if (selectedRecipe != null)
            {
                PdfSharp.Pdf.PdfDocument recipePDF = new PdfSharp.Pdf.PdfDocument();

                PdfPage recipePage = recipePDF.AddPage();

                 XGraphics graph = XGraphics.FromPdfPage(recipePage);

            
                recipePDF.Info.Title = $"{selectedRecipe.Name} recipe PDF";

                string recipeFileName = $"{user.Username}_{selectedRecipe.Name}.pdf";

                XFont font = new XFont("Verdana", 20);
                graph.DrawString(selectedRecipe.Name, font, XBrushes.Black, new XRect(0, 0, recipePage.Width.Point, recipePage.Height.Point), XStringFormats.Center);

                int y = 100;
                foreach (var ingredient in selectedRecipe.Ingredients)
                {

                    string recipeText = $"{ingredient.Name} takes {ingredient.Amount} {ingredient.MeasurementType}, be careful this ingredient may contain {ingredient.Allergen}";

                    XFont font2 = new XFont("Verdana", 10);
                    graph.DrawString(recipeText, font2, XBrushes.Black, new XRect(0, y, recipePage.Width.Point, recipePage.Height.Point), XStringFormats.Center);

                    y += 20;
                }
                XFont font3 = new XFont("Verdana", 10);
                graph.DrawString(selectedRecipe.Instructions, font3, XBrushes.Black, new XRect(0, 250, recipePage.Width.Point, recipePage.Height.Point), XStringFormats.Center);
                recipePDF.Save(recipeFileName);

                Process.Start("Explorer.exe", recipeFileName); //https://stackoverflow.com/questions/1746079/how-can-i-open-windows-explorer-to-a-certain-directory-from-within-a-wpf-app
            }
            else
            {
                MessageBox.Show("select an entry before exporting to pdf");
            }
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            AppSession.CurrentUser = null;
            LoginWindow login = new LoginWindow();
            login.Show();
            this.Close();
        }
    }
}
