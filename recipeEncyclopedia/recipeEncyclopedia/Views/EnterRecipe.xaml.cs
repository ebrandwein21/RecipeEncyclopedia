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
    /// Interaction logic for FeaturedRecipe.xaml
    /// </summary>
    public partial class EnterRecipe : Window
    {

        public ObservableCollection<recipeEncyclopedia.Models.Recipe> recipeInput { get; set; } = new ObservableCollection<recipeEncyclopedia.Models.Recipe>();
        public EnterRecipe()
        {
            InitializeComponent();
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

        private void SubmitRecipeButton_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show("Button clicked!");

            string ingredient = IngredientBox.Text;
            string name = EnterRecipeName.Text;
            int serving = int.Parse(ServingTextBox.Text);
            int duration = int.Parse(DurationBox.Text);
            string keywords = KeywordBox.Text;
            int amount = int.Parse(AmountBox.Text);
            string instructions = InstructionBox.Text;

            recipeEncyclopedia.Models.Recipe recipe = new recipeEncyclopedia.Models.Recipe();

            recipe.Ingredient = ingredient;
            recipe.Name = name;
            recipe.Serving = serving;
            recipe.TotalTime = duration;
            recipe.Tag = keywords;
            recipe.MeasurementAmount = amount;
            recipe.Instructions = instructions;
            

            recipeInput.Add(recipe);

            RecipeListBox.ItemsSource = recipeInput;

            IngredientBox.Clear();
            EnterRecipeName.Clear();
            ServingTextBox.Clear();
            DurationBox.Clear();
            KeywordBox.Clear();
            AmountBox.Clear();
            InstructionBox.Clear();


        }

        

        public void RecipeBox_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void EnterRecipeName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void RecipeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CategorySelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void InstructionBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
