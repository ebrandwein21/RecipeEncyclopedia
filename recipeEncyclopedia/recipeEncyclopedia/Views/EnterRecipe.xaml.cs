﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using recipeEncyclopedia.Data;
using recipeEncyclopedia.Models;
using recipeEncyclopedia.Models.recipeEncyclopedia.Models;

namespace recipeEncyclopedia.Views
{
    public partial class EnterRecipe : Window
    {
        private List<Ingredient> currentIngredients = new List<Ingredient>();


        public EnterRecipe()
        {
            InitializeComponent();
        }

        private void SubmitRecipe_Click(object sender, RoutedEventArgs e)
        {

            // Validate inputs
            if (string.IsNullOrWhiteSpace(EnterRecipeName.Text) ||
                string.IsNullOrWhiteSpace(DurationBox.Text) ||
                string.IsNullOrWhiteSpace(ServingTextBox.Text) ||
                string.IsNullOrWhiteSpace(KeywordBox.Text) ||
                string.IsNullOrWhiteSpace(InstructionBox.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            if (!int.TryParse(DurationBox.Text, out int duration))
            {
                MessageBox.Show("Duration must be a valid number.");
                return;
            }

            if (!int.TryParse(ServingTextBox.Text, out int serving))
            {
                MessageBox.Show("Serving size must be a valid number.");
                return;
            }

            if (currentIngredients.Count == 0)
            {
                MessageBox.Show("Please add at least one ingredient.");
                return;
            }

            // Gather selected categories
            var selectedCategories = new List<int>();

            foreach (var selectedItem in CategorySelection.SelectedItems)
            {
                int index = CategorySelection.Items.IndexOf(selectedItem);
                if (index >= 0)
                    selectedCategories.Add(index+1);
            }

            // Construct recipe object
            var newRecipe = new Recipe
            {
                Name = EnterRecipeName.Text,
                TotalTime = duration,
                Serving = serving,
                Keywords = KeywordBox.Text,
                Instructions = InstructionBox.Text,
                Ingredients = new List<Ingredient>(currentIngredients),
                Categories = selectedCategories
            };

            // Add to database
            var recipeService = new RecipeService();
            recipeService.Add(newRecipe);

            MessageBox.Show("Recipe submitted successfully!");

            // Clear inputs
            EnterRecipeName.Clear();
            DurationBox.Clear();
            ServingTextBox.Clear();
            KeywordBox.Clear();
            InstructionBox.Clear();
            IngredientBox.Clear();
            AmountBox.Clear();
            MeasurementTypeBox.Clear();
            AllergenBox.Clear();
            currentIngredients.Clear();
            IngredientPreviewList.ItemsSource = null;

            foreach (ListBoxItem item in CategorySelection.Items)
            {
                item.IsSelected = false;
            }
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            MainWindow home = new MainWindow();
            home.Show();
            this.Close();
        }

        private void AddIngredient_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(IngredientBox.Text) ||
                string.IsNullOrWhiteSpace(AmountBox.Text) ||
                string.IsNullOrWhiteSpace(MeasurementTypeBox.Text) ||
                string.IsNullOrWhiteSpace(AllergenBox.Text))
            {
                MessageBox.Show("Please fill in all ingredient fields.");
                return;
            }

            if (!double.TryParse(AmountBox.Text, out double amount))
            {
                MessageBox.Show("Amount must be a number.");
                return;
            }

            var ingredient = new Ingredient
            {
                Name = IngredientBox.Text,
                Amount = amount,
                MeasurementType = MeasurementTypeBox.Text,
                Allergen = AllergenBox.Text
            };

            currentIngredients.Add(ingredient);

            // Update preview
            IngredientPreviewList.ItemsSource = null;
            IngredientPreviewList.ItemsSource = currentIngredients
                .Select(i => $"{i.Name} - {i.Amount} {i.MeasurementType} (Allergen: {i.Allergen})")
                .ToList();

            // Clear inputs
            IngredientBox.Clear();
            AmountBox.Clear();
            MeasurementTypeBox.Clear();
            AllergenBox.Clear();
        }
    }
}