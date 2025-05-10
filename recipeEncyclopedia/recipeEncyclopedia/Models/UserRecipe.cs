using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
<<<<<<< Updated upstream
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace recipeEncyclopedia.Models
{
    public class UserRecipe
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string RecipeId { get; set; }
=======

namespace recipeEncyclopedia.Models
{
    internal class UserRecipe
    {
>>>>>>> Stashed changes
    }
}
