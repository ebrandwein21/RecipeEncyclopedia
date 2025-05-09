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
    /// Interaction logic for RecipeMenu.xaml
    /// </summary>
    public partial class RecipeMenu : Window
    {
        public RecipeMenu()
        {
            InitializeComponent();
        }

        private void BreakfastRecipe_Click(object sender, RoutedEventArgs e)
        {

            BreakfastRecipes breakfast = new BreakfastRecipes();
            breakfast.Show();
            this.Close();
        }

        private void DinnerRecipe_Click(object sender, RoutedEventArgs e)
        {
            DinnerRecipes dinner = new DinnerRecipes();
            dinner.Show();
            this.Close();
        }


        private void LunchRecipe_Click(object sender, RoutedEventArgs e)
        {
            LunchRecipes lunch = new LunchRecipes();
            lunch.Show();
            this.Close();
        }

        private void BakingRecipe_Click(object sender, RoutedEventArgs e)
        {
            BakingRecipes bakedGoods = new BakingRecipes();
            bakedGoods.Show();
            this.Close();
        }
    }
}
