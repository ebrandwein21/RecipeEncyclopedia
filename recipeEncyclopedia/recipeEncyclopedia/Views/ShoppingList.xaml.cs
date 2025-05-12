using recipeEncyclopedia.Data;
using recipeEncyclopedia.Models;
using System.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using recipeEncyclopedia.Models.recipeEncyclopedia.Models;

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


    }
}
