using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
<<<<<<< Updated upstream
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace recipeEncyclopedia.Models
{
    public class ShoppingListModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserId { get; set; }
        public List<string> Items { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
=======

namespace recipeEncyclopedia.Models.cs
{
    class ShoppingListModel
    {
>>>>>>> Stashed changes
    }
}
