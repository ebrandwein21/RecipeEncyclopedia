﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using recipeEncyclopedia.Data;
using recipeEncyclopedia.Models;

namespace recipeEncyclopedia.Views
{
    public partial class EnterRecipe : Window
    {
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
                string.IsNullOrWhiteSpace(IngredientBox.Text) ||
                string.IsNullOrWhiteSpace(AmountBox.Text) ||
                string.IsNullOrWhiteSpace(MeasurementTypeBox.Text) ||
                string.IsNullOrWhiteSpace(AllergenBox.Text) ||
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

            if (!double.TryParse(AmountBox.Text, out double amount))
            {
                MessageBox.Show("Amount must be a valid number.");
                return;
            }

            // Build ingredients list as string
            string ingredientText = $"Ingredient: {IngredientBox.Text}, Amount: {amount} {MeasurementTypeBox.Text}, Allergen: {AllergenBox.Text}";
            var ingredientsList = new List<string> { ingredientText };

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
                Ingredients = ingredientsList,
                Allergen = AllergenBox.Text,
                MeasurementAmount = amount,
                MeasurementType = MeasurementTypeBox.Text,
                Instructions = InstructionBox.Text,
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
            IngredientBox.Clear();
            AmountBox.Clear();
            MeasurementTypeBox.Clear();
            AllergenBox.Clear();
            InstructionBox.Clear();

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

    }
}