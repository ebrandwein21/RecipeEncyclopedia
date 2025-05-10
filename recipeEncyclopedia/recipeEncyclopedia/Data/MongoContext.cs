using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
<<<<<<< Updated upstream
using MongoDB.Driver;
using recipeEncyclopedia.Models;

namespace recipeEncyclopedia.Data
{
    public class MongoContext
    {
        private readonly IMongoDatabase _database;

        public MongoContext()
        {
            var connectionString = "mongodb+srv://user:<password>@cookbook.0tkr7zr.mongodb.net/?retryWrites=true&w=majority&appName=CookBook";
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase("CookbookDB");
        }

        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
        public IMongoCollection<Recipe> Recipes => _database.GetCollection<Recipe>("Recipes");
        public IMongoCollection<ShoppingListModel> ShoppingLists => _database.GetCollection<ShoppingListModel>("ShoppingLists");
        public IMongoCollection<UserRecipe> UserRecipes => _database.GetCollection<UserRecipe>("UserRecipes");

=======

<<<<<<<< Updated upstream:recipeEncyclopedia/recipeEncyclopedia/Models/Ingredient.cs
namespace recipeEncyclopedia.Models
========
namespace recipeEncyclopedia.Data
>>>>>>>> Stashed changes:recipeEncyclopedia/recipeEncyclopedia/Data/MongoContext.cs
{
    internal class MongoContext
    {
>>>>>>> Stashed changes
    }
}
