using MongoDB.Driver;
using recipeEncyclopedia.Models;
using System.Collections.Generic;
using System.Linq;

namespace recipeEncyclopedia.Data
{
    public class RecipeService
    {
        private readonly IMongoCollection<Recipe> _collection;

        public RecipeService()
        {
            var context = new MongoContext();
            _collection = context.Recipes;
        }

        public List<Recipe> GetByCategory(int categoryId)
        {
            return _collection.Find(r => r.Categories.Contains(categoryId)).ToList();
        }

        public List<Recipe> GetAll()
        {
            return _collection.Find(_ => true).ToList();
        }

        public Recipe GetById(string id)
        {
            return _collection.Find(r => r.Id == id).FirstOrDefault();
        }
    }
}
