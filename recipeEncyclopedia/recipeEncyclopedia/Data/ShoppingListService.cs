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
    public class ShoppingListService
    {
        private readonly IMongoCollection<ShoppingListModel> _collection;

        public ShoppingListService()
        {
            var context = new MongoContext();
            _collection = context.ShoppingLists;
        }

        public void Add(ShoppingListModel list) => _collection.InsertOne(list);

        public List<ShoppingListModel> GetAll() => _collection.Find(_ => true).ToList();

        public ShoppingListModel GetById(string id) => _collection.Find(list => list.Id == id).FirstOrDefault();

        public List<ShoppingListModel> GetByUser(string userId) => _collection.Find(list => list.UserId == userId).ToList();

        public void Delete(string id) => _collection.DeleteOne(list => list.Id == id);

        public void AppendItems(string shoppingListId, List<string> newItems)
        {
            var list = _collection.Find(sl => sl.Id == shoppingListId).FirstOrDefault();

            if (list != null)
            {
                list.Items.AddRange(newItems);
                _collection.ReplaceOne(sl => sl.Id == shoppingListId, list);
            }
        }

        public void RemoveItem(string shoppingListId, string itemToRemove)
        {
            var list = _collection.Find(sl => sl.Id == shoppingListId).FirstOrDefault();

            if (list != null)
            {
                // Remove all matching items
                list.Items = list.Items.Where(item => item != itemToRemove).ToList();

                _collection.ReplaceOne(sl => sl.Id == shoppingListId, list);
            }
        }



    }
}

