using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace recipeEncyclopedia.Models
{
    public class ShoppingListModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserId { get; set; }
        public List<recipeEncyclopedia.Models.Ingredient> Items { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
