using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using recipeEncyclopedia.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Collections.Generic;

namespace recipeEncyclopedia.Data
{
    public class ShoppingListService
    {
        private readonly IMongoCollection<ShoppingListModel> _collection;

        public ShoppingListService()
        {
            var context = new MongoContext();
            _collection = context.ShoppingLists;
        }

        // Add a new shopping list
        public void Add(ShoppingListModel list)
        {
            _collection.InsertOne(list);
        }

        // Get all shopping lists
        public List<ShoppingListModel> GetAll()
        {
            return _collection.Find(_ => true).ToList();
        }

        // Get a list by its MongoDB _id
        public ShoppingListModel GetById(string id)
        {
            return _collection.Find(list => list.Id == id).FirstOrDefault();
        }

        // Get all shopping lists for a user
        public List<ShoppingListModel> GetByUser(string userId)
        {
            return _collection.Find(list => list.UserId == userId).ToList();
        }

        // Update an existing list by ID
        public void Update(string id, ShoppingListModel updatedList)
        {
            _collection.ReplaceOne(list => list.Id == id, updatedList);
        }

        // Delete a list by ID
        public void Delete(string id)
        {
            _collection.DeleteOne(list => list.Id == id);
        }

        public void RemoveIngredientFromList(string userId, Models.recipeEncyclopedia.Models.Ingredient ingredientToRemove)
        {
            // Find the first list associated with the user
            var list = _collection.Find(sl => sl.UserId == userId).FirstOrDefault();
            if (list == null) return;

            // Remove the matching ingredient
            list.Items.RemoveAll(i =>
                i.Name == ingredientToRemove.Name);
            //i.MeasurementType == ingredientToRemove.MeasurementType &&
            //Math.Abs(i.Amount - ingredientToRemove.Amount) < 0.001 &&
            //i.Allergen == ingredientToRemove.Allergen);

            // Update the list in the database
            _collection.ReplaceOne(sl => sl.Id == list.Id, list);
        }


    }
}
