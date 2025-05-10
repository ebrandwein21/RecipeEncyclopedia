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

        public string Ingredient { get; set; }
        public int Allergen { get; set; }
        public int Measurement { get; set; }
        public int Serving { get; set; }

        public int MeasurementAmount { get; set; }
        public string Instructions { get; set; }
        public List<string> Tags { get; set; }

        public string Tag { get; set; }
        public string Category { get; set; }
        public int TotalTime { get; set; }
    }
}