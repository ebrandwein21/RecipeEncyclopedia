using recipeEncyclopedia.Data;
using recipeEncyclopedia.Models;
using System.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using recipeEncyclopedia.Models.recipeEncyclopedia.Models;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Diagnostics;

namespace recipeEncyclopedia.Views
{
    public partial class ShoppingList : Window
    {
        private readonly ShoppingListService _service = new ShoppingListService();
        private List<Ingredient> _currentIngredients = new List<Ingredient>();

        public ShoppingList()
        {
            InitializeComponent();
            LoadShoppingList();
        }

        private void LoadShoppingList()
        {
            var user = AppSession.CurrentUser;
            if (user == null) return;

            var lists = _service.GetByUser(user.Id);
            _currentIngredients = lists
                .SelectMany(l => l.Items)
                .DistinctBy(i => $"{i.Name}|{i.Amount}|{i.MeasurementType}|{i.Allergen}")
                .ToList();

            var displayItems = _currentIngredients
                .Select(i => $"{i.Amount} {i.MeasurementType} {i.Name} (Allergen: {i.Allergen})")
                .ToList();

            ShoppingListBox.ItemsSource = displayItems;

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

        private void RemoveSelectedItem_Click(object sender, RoutedEventArgs e)
        {
            var selectedText = ShoppingListBox.SelectedItem as string;
            var user = AppSession.CurrentUser;

            if (string.IsNullOrEmpty(selectedText))
            {
                MessageBox.Show("Please select an item to remove.");
                return;
            }

            if (user == null)
            {
                MessageBox.Show("You must be logged in.");
                return;
            }

            // Find the matching Ingredient based on the display string
            var ingredientToRemove = _currentIngredients.FirstOrDefault(i =>
                $"{i.Amount} {i.MeasurementType} {i.Name} (Allergen: {i.Allergen})" == selectedText);

            if (ingredientToRemove == null)
            {
                MessageBox.Show("Item not found in list.");
                return;
            }

            _service.RemoveIngredientFromList(user.Id, ingredientToRemove);
            LoadShoppingList();
            MessageBox.Show($"{ingredientToRemove.Name} removed from your shopping list.");
        }

        private void ClearShoppingList_Click(object sender, RoutedEventArgs e)
        {
            var user = AppSession.CurrentUser;
            if (user == null)
            {
                MessageBox.Show("You must be logged in to clear the shopping list.");
                return;
            }

            var service = new ShoppingListService();
            var userLists = service.GetByUser(user.Id);

            foreach (var list in userLists)
            {
                service.Delete(list.Id);
            }

            ShoppingListBox.ItemsSource = null;
            _currentIngredients.Clear();
            MessageBox.Show("Your shopping list has been cleared.");
        }

        private void ExportToPDF_Click(object sender, RoutedEventArgs e)
        {
            var user = AppSession.CurrentUser;

            IEnumerable<string> ShoppingList = ShoppingListBox.SelectedItems.Cast<string>().ToList();


            if (ShoppingList != null)
            {
                PdfSharp.Pdf.PdfDocument recipePDF = new PdfSharp.Pdf.PdfDocument();

                PdfPage recipePage = recipePDF.AddPage();

                XGraphics graph = XGraphics.FromPdfPage(recipePage);


                recipePDF.Info.Title = $"{ShoppingList} recipe PDF";

                string recipeFileName = $"{user.Username}_selectedItems.pdf";

                int y = 25;

                foreach (var ingredient in ShoppingList)
                {


                    var ingredientToPrint = _currentIngredients.FirstOrDefault(i =>
                       $"{i.Amount} {i.MeasurementType} {i.Name} (Allergen: {i.Allergen})" == ingredient);

                    XFont font2 = new XFont("Verdana", 20);
                    string shoppingListOutput = $"{ingredientToPrint.Amount} {ingredientToPrint.MeasurementType} {ingredientToPrint.Name} (Allergen: {ingredientToPrint.Allergen})";

                    graph.DrawString(shoppingListOutput, font2, XBrushes.Black, new XRect(0, y, recipePage.Width.Point, recipePage.Height.Point), XStringFormats.Center);

                    y += 20;
                }
              
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
