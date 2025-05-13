using recipeEncyclopedia.Models;
using recipeEncyclopedia.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Diagnostics;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Diagnostics;

namespace recipeEncyclopedia.Views
{
    public partial class AllRecipesView : Window
    {
        private List<Recipe> allRecipes;
        private readonly RecipeService _service = new RecipeService();
        private readonly UserRecipeService _userRecipeService = new UserRecipeService();

        public AllRecipesView()
        {
            InitializeComponent();
            LoadRecipes();
        }

        private void LoadRecipes()
        {
            allRecipes = _service.GetAll(); // Assumes you have a RecipeService with GetAll()
            AllRecipesList.ItemsSource = allRecipes;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            FilterAndDisplayRecipes();
        }

        private void TimeFilterBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterAndDisplayRecipes();
        }


        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            PlaceholderTextBlock.Visibility = string.IsNullOrEmpty(SearchBox.Text)
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            RecipeMenu menu = new RecipeMenu();
            menu.Show();
            this.Close();
        }

        private void AllRecipesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Optional: auto-load or display recipe preview
        }

        private void ViewDetails_Click(object sender, RoutedEventArgs e)
        {
            var selected = AllRecipesList.SelectedItem as Recipe;
            if (selected == null)
            {
                MessageBox.Show("Please select a recipe.");
                return;
            }

            // Show details however you prefer
            MessageBox.Show($"{selected.Name}\n\nIngredients:\n" +
                string.Join("\n", selected.Ingredients.Select(i =>
                    $"{i.Amount} {i.MeasurementType} {i.Name}")) +
                $"\n\nInstructions:\n{selected.Instructions}");
        }

        private void AddToFavorites_Click(object sender, RoutedEventArgs e)
        {
            var selectedRecipe = AllRecipesList.SelectedItem as Recipe;
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
            //favorites.Show();
            //this.Close();
        }

        private void FilterAndDisplayRecipes()
        {
            var keyword = SearchBox.Text?.ToLower() ?? "";
            var filtered = allRecipes.Where(r =>
                string.IsNullOrEmpty(keyword) || (r.Keywords?.ToLower().Contains(keyword) == true)).ToList();

            var selected = (TimeFilterBox.SelectedItem as ComboBoxItem)?.Content?.ToString();

            if (selected != null)
            {
                filtered = selected switch
                {
                    "0–15 minutes" => filtered.Where(r => r.TotalTime <= 15).ToList(),
                    "15–30 minutes" => filtered.Where(r => r.TotalTime > 15 && r.TotalTime <= 30).ToList(),
                    "30–45 minutes" => filtered.Where(r => r.TotalTime > 30 && r.TotalTime <= 45).ToList(),
                    "45+ minutes" => filtered.Where(r => r.TotalTime > 45).ToList(),
                    _ => filtered
                };
            }

            AllRecipesList.ItemsSource = filtered;
        }

        private void ExportToPdf_Click(object sender, RoutedEventArgs e)
        {
            //https://stackoverflow.com/questions/12391244/pdfsharp-getting-started
            var user = AppSession.CurrentUser;

            var selectedRecipe = AllRecipesList.SelectedItem as Recipe;

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
    }
}
