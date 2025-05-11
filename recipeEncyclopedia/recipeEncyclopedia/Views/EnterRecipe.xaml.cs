using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using System.Xml.Linq;
using static MongoDB.Bson.Serialization.Serializers.SerializerHelper;

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
            DataContext = this;
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

       
        private void SubmitRecipe_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Button clicked!");

            string ingredient = IngredientBox.Text;
            if(String.IsNullOrEmpty(ingredient))
            {
                MessageBox.Show("hey, lets put an ingredient in");
                return;
            }

            string name = EnterRecipeName.Text;
            if (String.IsNullOrEmpty(name))
            {
                MessageBox.Show("hey, lets put a recipe name in");
                return;
            }

            int serving;
            if(String.IsNullOrEmpty(ServingTextBox.Text))
            {
                MessageBox.Show("hey! Lets enter a serving size");
                return;
            }

            if (Int32.TryParse((string)ServingTextBox.Text, out serving) == false) //https://stackoverflow.com/questions/13335787/wpf-data-binding-exception-handling
            {
                MessageBox.Show("serving is a number of how many to feed");
                return;
            }
            else
            {
                Int32.Parse(ServingTextBox.Text);
            }

            int duration;
            if (String.IsNullOrEmpty(DurationBox.Text))
            {
                MessageBox.Show("hey! Lets enter a duration");
                return;
            }
            if (Int32.TryParse((string)DurationBox.Text, out duration) == false) //https://stackoverflow.com/questions/13335787/wpf-data-binding-exception-handling
            {
                MessageBox.Show("duration is a number in minutes");
                return;
            }
            else
            {
                Int32.Parse(DurationBox.Text);
            }

            string keyword = KeywordBox.Text;
            if (String.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("hey! Lets enter a keyword");
                return;
            }

            string instructions = InstructionBox.Text;
            if (String.IsNullOrEmpty(instructions))
            {
                MessageBox.Show("hey! Lets enter some instructions");
                return;
            }
            int amount;
            if (String.IsNullOrEmpty(AmountBox.Text))
            {
                MessageBox.Show("hey! Lets enter an Amount to measure");
                return;
            }
            if (Int32.TryParse((string)AmountBox.Text, out amount) == false) //https://stackoverflow.com/questions/13335787/wpf-data-binding-exception-handling
            {
                MessageBox.Show("Measurement is a number");
                return;
            }
            else
            {
                Int32.Parse(AmountBox.Text);
            }


            recipeEncyclopedia.Models.Recipe recipe = new recipeEncyclopedia.Models.Recipe();

            recipe.Ingredient = ingredient;
            recipe.Name = name;
            recipe.Serving = serving;
            recipe.TotalTime = duration;
            recipe.Keywords = keyword;
            recipe.Instructions = instructions;
            recipe.MeasurementAmount = amount;


            recipeInput.Add(recipe);

            informationBox.ItemsSource = recipeInput;
            IngredientListBox.ItemsSource = recipeInput;
            InstructionListBox.ItemsSource = recipeInput;

            IngredientBox.Clear();
            EnterRecipeName.Clear();
            ServingTextBox.Clear();
            DurationBox.Clear();
            KeywordBox.Clear();
            AmountBox.Clear();
            InstructionBox.Clear();
        }

        private void IngredientBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (informationBox.SelectedItem is recipeEncyclopedia.Models.Recipe selectedRecipeIngredient)
            {
                IngredientBox.Text = selectedRecipeIngredient.Ingredients.ToString();
                AmountBox.Text = selectedRecipeIngredient.MeasurementAmount.ToString();
                MeasurementChoice.SelectedIndex = selectedRecipeIngredient.Measurement;
            }
        }

        private void Instruction_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (IngredientListBox.SelectedItem is recipeEncyclopedia.Models.Recipe selectedRecipeInstruction)
            {
                InstructionBox.Text = selectedRecipeInstruction.Instructions;
            }
        }
        private void InformationBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (InstructionListBox.SelectedItem is recipeEncyclopedia.Models.Recipe selectedRecipeInfo)
            {
                EnterRecipeName.Text = selectedRecipeInfo.Name;
                ServingTextBox.Text = selectedRecipeInfo.Serving.ToString();
                DurationBox.Text = selectedRecipeInfo.TotalTime.ToString();
                KeywordBox.Text = selectedRecipeInfo.Tags.ToString();
                string infoString = selectedRecipeInfo.InformationToString;



                foreach (int index in selectedRecipeInfo.Categories)
                {
                    if(index >= 0 && index < selectedRecipeInfo.Categories.Count )
                    {
                        CategorySelection.SelectedItems.Add(CategorySelection.Items[index]); //https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.listview.selecteditems?view=windowsdesktop-9.0
                    }
                }

               
               
            }
            
        }

    }
}
