namespace recipeEncyclopedia.Models
{ 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    namespace recipeEncyclopedia.Models
    {
        public class Ingredient
        {
            public string Name { get; set; }
            public double Amount { get; set; }
            public string MeasurementType { get; set; }
            public string Allergen { get; set; }
        }
    }

}

