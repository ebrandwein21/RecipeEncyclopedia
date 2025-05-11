using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace recipeEncyclopedia.Models
{
    public class Recipe
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }

        public List<string> Ingredients { get; set; }
        public String Ingredient { get; set; }
        public int Allergen { get; set; }
        public int Measurement { get; set; }
        public int Serving { get; set; }

        public int MeasurementAmount { get; set; }
        public string Instructions { get; set; }
        public String Keywords { get; set; }

        public List<int> Categories { get; set; }
        public int TotalTime { get; set; }

        public String IngredienToString
        {
            get
            {
                return $"Ingredient {Ingredient}, Measurement {Measurement}, Serving Size {Serving}";
            }
        }

        public String InstructionToString
        {
            get
            {
                return $"{Instructions}";
            }
        }

        public String InformationToString
        {
            get
            {
                return $"Recipe {Name} will take {TotalTime} Minutes Long, It will Serve {Serving}, it is tagged as {Keywords} and is a {Categories} meal ";
            }
        }
    }
   
}