using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using recipeEncyclopedia.Models;
using MongoDB.Driver;
using System.Collections.Generic;

namespace recipeEncyclopedia.Data
{
    public class UserRecipeService
    {
        private readonly IMongoCollection<UserRecipe> _collection;

        public UserRecipeService()
        {
            var context = new MongoContext();
            _collection = context.UserRecipes;
        }

        public void Add(UserRecipe ur) => _collection.InsertOne(ur);

        public List<UserRecipe> GetAll() => _collection.Find(_ => true).ToList();

        public UserRecipe GetById(string id) =>
            _collection.Find(ur => ur.Id == id).FirstOrDefault();

        public List<UserRecipe> GetByUserId(string userId) =>
            _collection.Find(ur => ur.UserId == userId).ToList();

        public List<UserRecipe> GetByRecipeId(string recipeId) =>
            _collection.Find(ur => ur.RecipeId == recipeId).ToList();

        public void Delete(string id) =>
            _collection.DeleteOne(ur => ur.Id == id);
    }
}

