using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
<<<<<<< Updated upstream
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace recipeEncyclopedia.Models
{
    public class Recipe
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Ingredients { get; set; }
        public string Instructions { get; set; }
        public List<string> Tags { get; set; }
        public string Category { get; set; }
        public int TotalTime { get; set; }
=======

namespace recipeEncyclopedia.Models.cs
{
    class Recipe
    {
>>>>>>> Stashed changes
    }
}
