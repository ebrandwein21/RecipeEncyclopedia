using recipeEncyclopedia.Data;
using recipeEncyclopedia.Models.recipeEncyclopedia.Models;
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
    /// Interaction logic for EditFavoriteRecipe.xaml
    /// </summary>
    public partial class EditFavoriteRecipe : Window
    {
        private Recipe _originalRecipe;
        private List<Ingredient> currentIngredients = new List<Ingredient>();

        private int selectedIngredientIndex = -1;


        public EditFavoriteRecipe(Recipe recipeToEdit)
        {
            InitializeComponent();
            _originalRecipe = recipeToEdit;
            LoadRecipeData();
        }

        private void LoadRecipeData()
        {
            EditRecipeName.Text = _originalRecipe.Name;
            EditDurationBox.Text = _originalRecipe.TotalTime.ToString();
            EditServingTextBox.Text = _originalRecipe.Serving.ToString();
            EditKeywordBox.Text = _originalRecipe.Keywords;
            EditInstructionBox.Text = _originalRecipe.Instructions;

            currentIngredients = _originalRecipe.Ingredients ?? new List<Ingredient>();
            EditIngredientPreviewList.ItemsSource = currentIngredients.Select(i =>
                $"{i.Name} - {i.Amount} {i.MeasurementType} (Allergen: {i.Allergen})").ToList();

            foreach (int index in _originalRecipe.Categories)
            {
                if (index - 1 >= 0 && index - 1 < EditCategorySelection.Items.Count)
                    ((ListBoxItem)EditCategorySelection.Items[index - 1]).IsSelected = true;
            }
        }

        private void AddIngredient_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(EditAmountBox.Text, out double amount))
            {
                MessageBox.Show("Invalid ingredient amount.");
                return;
            }

            var newIngredient = new Ingredient
            {
                Name = EditIngredientBox.Text,
                Amount = amount,
                MeasurementType = EditMeasurementTypeBox.Text,
                Allergen = EditAllergenBox.Text
            };

            if (selectedIngredientIndex >= 0)
            {
                // Update existing ingredient
                currentIngredients[selectedIngredientIndex] = newIngredient;
                selectedIngredientIndex = -1;
            }
            else
            {
                // Add new ingredient
                currentIngredients.Add(newIngredient);
            }

            RefreshIngredientList();
            ClearIngredientFields();
        }

        private void UpdateRecipe_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(EditDurationBox.Text, out int duration) ||
                !int.TryParse(EditServingTextBox.Text, out int serving))
            {
                MessageBox.Show("Invalid time or serving value.");
                return;
            }

            var user = AppSession.CurrentUser;
            if (user == null)
            {
                MessageBox.Show("You must be logged in to save edits.");
                return;
            }

            // Create a copy with modified name
            var newRecipe = new Recipe
            {
                Name = $"{user.Username}'s {EditRecipeName.Text}",
                TotalTime = duration,
                Serving = serving,
                Keywords = EditKeywordBox.Text,
                Instructions = EditInstructionBox.Text,
                Ingredients = currentIngredients,
                Categories = EditCategorySelection.SelectedItems.Cast<ListBoxItem>()
                            .Select(i => EditCategorySelection.Items.IndexOf(i) + 1).ToList()
            };

            var recipeService = new RecipeService();
            recipeService.Add(newRecipe); // Save the new version

            // Replace the favorite reference
            var favoriteService = new UserRecipeService();
            favoriteService.ReplaceFavorite(user.Id, _originalRecipe.Id, newRecipe.Id);

            MessageBox.Show("Your personalized version has been saved and favorited.");
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void EditIngredientPreviewList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EditIngredientPreviewList.SelectedIndex >= 0 && EditIngredientPreviewList.SelectedIndex < currentIngredients.Count)
            {
                var selected = currentIngredients[EditIngredientPreviewList.SelectedIndex];
                EditIngredientBox.Text = selected.Name;
                EditAmountBox.Text = selected.Amount.ToString();
                EditMeasurementTypeBox.Text = selected.MeasurementType;
                EditAllergenBox.Text = selected.Allergen;

                selectedIngredientIndex = EditIngredientPreviewList.SelectedIndex;
            }
        }

        private void ClearIngredientFields()
        {
            EditIngredientBox.Clear();
            EditAmountBox.Clear();
            EditMeasurementTypeBox.Clear();
            EditAllergenBox.Clear();
            EditIngredientPreviewList.UnselectAll();
        }

        private void RefreshIngredientList()
        {
            EditIngredientPreviewList.ItemsSource = null;
            EditIngredientPreviewList.ItemsSource = currentIngredients.Select(i =>
                $"{i.Name} - {i.Amount} {i.MeasurementType} (Allergen: {i.Allergen})").ToList();
        }

        private void RemoveIngredient_Click(object sender, RoutedEventArgs e)
        {
            if (EditIngredientPreviewList.SelectedIndex >= 0)
            {
                currentIngredients.RemoveAt(EditIngredientPreviewList.SelectedIndex);
                RefreshIngredientList();
                ClearIngredientFields();
            }
        }
    }
}
