using MongoDB.Driver;
using recipeEncyclopedia.Models;
using recipeEncyclopedia.Models.recipeEncyclopedia.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace recipeEncyclopedia.ViewModel
{
    class RecipeViewModel
    {

      
            public static ObservableCollection<Recipe> favoriteRecipe { get; set; } = new ObservableCollection<Recipe>();
        
    }
}
        
