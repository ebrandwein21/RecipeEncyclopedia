using MongoDB.Driver;
using recipeEncyclopedia.Models;
using System;
using System.Collections.Generic;

namespace recipeEncyclopedia.Data
{
    public class RecipeSeed
    {
        public static void SeedRecipes()
        {
            var context = new MongoContext();
            var collection = context.Recipes;

            if (collection.CountDocuments(_ => true) > 0)
            {
                Console.WriteLine("Recipes already exist in the database.");
                return;
            }

            var recipes = new List<Recipe>
            {
                new Recipe
                {
                    Name = "Spaghetti Bolognese",
                    Ingredients = new List<string> { "spaghetti", "ground beef", "tomato sauce", "onion", "garlic" },
                    Ingredient = "spaghetti",
                    Allergen = 1,
                    Measurement = 2,
                    Serving = 4,
                    MeasurementAmount = 500,
                    Instructions = "Boil spaghetti. Cook beef. Mix with sauce. Combine and serve.",
                    Keywords = "italian, pasta",
                    Categories = new List<int> { 3 },
                    TotalTime = 45
                },
                new Recipe
                {
                    Name = "Pancakes",
                    Ingredients = new List<string> { "flour", "milk", "eggs", "sugar", "baking powder" },
                    Ingredient = "flour",
                    Allergen = 2,
                    Measurement = 1,
                    Serving = 2,
                    MeasurementAmount = 200,
                    Instructions = "Mix all ingredients. Pour batter on griddle. Flip when bubbles form.",
                    Keywords = "pancake, breakfast, sweet",
                    Categories = new List<int> { 1 },
                    TotalTime = 20
                },
                new Recipe
                {
                    Name = "Chicken Salad",
                    Ingredients = new List<string> { "chicken breast", "lettuce", "tomato", "cucumber", "olive oil" },
                    Ingredient = "chicken",
                    Allergen = 0,
                    Measurement = 1,
                    Serving = 2,
                    MeasurementAmount = 250,
                    Instructions = "Grill chicken. Chop veggies. Toss everything with olive oil.",
                    Keywords = "chicken, salad, healthy",
                    Categories = new List<int> { 2 },
                    TotalTime = 25
                },
                // Add more recipes here...
            };

            collection.InsertMany(recipes);
            Console.WriteLine("Seeded recipes to database.");
        }
    }
}
