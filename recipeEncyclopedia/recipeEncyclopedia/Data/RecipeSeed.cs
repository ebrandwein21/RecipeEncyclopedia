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
                    Allergen = "none",
                    MeasurementAmount = 2,
                    Serving = 4,
                    MeasurementType = "cups",
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
                    MeasurementType = "cups",
                    MeasurementAmount = 1,
                    Serving = 2,
                    Allergen = "dairy",
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
                    Allergen = "none",
                    MeasurementAmount = 200,
                    Serving = 4,
                    MeasurementType = "grams",
                    Instructions = "Grill chicken. Chop veggies. Toss everything with olive oil.",
                    Keywords = "chicken, salad, healthy",
                    Categories = new List<int> { 2 },
                    TotalTime = 25
                },

                new Recipe
                {
                    Name = "Chocolate Chip Cookies",
                    Ingredients = new List<string> { "Flour", "Chocolate Chips", "Sugar", "Eggs" },
                    Ingredient = "Flour",
                    Allergen = "Dairy",
                    MeasurementAmount = 3.0,
                    Serving = 6,
                    MeasurementType = "cups",
                    Instructions = "Crack eggs into flour and whisk, add sugar, drop some chocolate chips in after stirring and bake at 425f for 15 minutes.",
                    Keywords = "dessert, comfort",
                    Categories = new List<int> { 3 },
                    TotalTime = 25
                },
                new Recipe
                {
                    Name = "Cupcakes",
                    Ingredients = new List<string> {  "Flour", "Chocolate", "Sugar", "Eggs", "Vanilla", "Frosting" },
                    Ingredient = "Flour",
                    Allergen = "dairy",
                    MeasurementAmount = 2.0,
                    Serving = 8,
                    MeasurementType = "cups",
                    Instructions = "Crack eggs into flour and whisk, add sugar, Chocolate and vanilla, bake at 425f for 25 minutes, add layer of frosting after baking.",
                    Keywords = "dessert, comfort",
                    Categories = new List<int> { 3 },
                    TotalTime = 60
                },

                new Recipe
                {
                    Name = "Banana Bread",
                    Ingredients = new List<string> { "bananas", "Flour", "Sugar", "Chocolate Chips", "Eggs"},
                    Ingredient = "Banana",
                    Allergen = "dairy",
                    MeasurementAmount = .5,
                    Serving = 5,
                    MeasurementType = "pounds",
                    Instructions = "Crack eggs into flour and whisk, add bananas and sugar, add chocolate chips, bake at 425f for 25 minutes.",
                    Keywords = "dessert, fruit",
                    Categories = new List<int> { 3 },
                    TotalTime = 35
                },

                new Recipe
                {

                    Name = "Pancakes",
                    Ingredients = new List<string> { "flour", "milk", "eggs", "sugar", "baking powder" },
                    Ingredient = "flour",
                    MeasurementType = "cups",
                    MeasurementAmount = 1.0,
                    Serving = 2,
                    Allergen = "dairy",
                    Instructions = "Mix all ingredients. Pour batter on griddle. Flip when bubbles form.",
                    Keywords = "pancake, breakfast, sweet",
                    Categories = new List<int> { 1 },
                    TotalTime = 20
                },
                new Recipe
                {
                    Name = "French Toast",
                    Ingredients = new List<string> { "Bread", "Eggs", "Milk", "Cinnamon", "Vanilla"},
                    Ingredient = "Eggs",
                    Allergen = "dairy",
                    MeasurementAmount = .5,
                    Serving = 3,
                    MeasurementType = "dozen",
                    Instructions = "Cut thick pieces of bread, Crack eggs into flour and whisk, add cinnamon and vanilla and pour on bread, bake at 425f for 25 minutes.",
                    Keywords = "Breakfast, Sweet",
                    Categories = new List<int> { 3 },
                    TotalTime = 30
                },

                new Recipe
                {
                    Name = "Omellette",
                    Ingredients = new List<string> { "eggs", "tomato", "onion", "jalapeno", "cheese", "ham"},
                    Ingredient = "eggs",
                    Allergen = "dairy",
                    MeasurementAmount = .25,
                    Serving = 1,
                    MeasurementType = "dozen",
                    Instructions = "crack egg on pan, cut up veggies/ham into small chunks and pour into eggs with cheese.",
                    Keywords = "healthy, breakfast, quick",
                    Categories = new List<int> { 3 },
                    TotalTime = 10
                },

            new Recipe
            {
                Name = "CheeseBurger",
                Ingredients = new List<string> { "cheese", "ground beef", "tomato", "onion", "pickle", "lettuce" },
                Ingredient = "ground beef",
                Allergen = "dairy",
                MeasurementAmount = 1,
                Serving = 2,
                MeasurementType = "pound",
                Instructions = "Smush Ground Beef into patties, Cut up vegetables, Cook On Grill, flipping patties halfway through.",
                Keywords = "American, Fast",
                Categories = new List<int> { 3 },
                TotalTime = 15
            },

                new Recipe
                {

                    Name = "Fried Rice",
                    Ingredients = new List<string> { "Rice", "Soy Sauce", "Teriyaki", "Chicken", "Peas", "carrots"},
                    Ingredient = "Rice",
                    MeasurementType = "cups",
                    MeasurementAmount = 1.5,
                    Serving = 3,
                    Allergen = "none",
                    Instructions = "cook veggies, chicken and sauces in a wok at 400f, cook rice in pot or 2 cups of water.",
                    Keywords = "asian, dinner",
                    Categories = new List<int> { 1 },
                    TotalTime = 25
                },
                new Recipe
                {
                Name = "Tacos",
                Ingredients = new List<string> { "cheese", "Chicken", "tomato", "onion", "avocado", "lettuce", "jalapeno", "cilantro", "soft shells" },
                Ingredient = "Chicken",
                Allergen = "dairy",
                MeasurementAmount = 200,
                Serving = 2,
                MeasurementType = "grams",
                Instructions = "Put Chicken In Crock Pot, prep salsa by cutting up veggies, put chicken in soft shells.",
                Keywords = "Hispanic, Long, dinner",
                Categories = new List<int> { 3 },
                TotalTime = 180
                },

                new Recipe
                {
                    Name = "falaffel Gyro Sandwich",
                    Ingredients = new List<string> { "Gyro", "Tzaiki", "onion", "tortilla", "fallafel"},
                    Ingredient = "Gyro",
                    Allergen = "none",
                    MeasurementAmount = .5,
                    Serving = 1,
                    MeasurementType = "pounds",
                    Instructions = "cook gyro on grill or stovetop, put cooked fallafel in tortilla wrap with falafel, onion and tzaiki sauce.",
                    Keywords = "medditeranian, dinner, quick",
                    Categories = new List<int> { 3 },
                    TotalTime = 10
                },
                new Recipe
                {
                    Name = "Chicken Curry ",
                    Ingredients = new List<string> { "Chicken", "Coconut Milk", "Curry Powder", "Chickpeas"},
                    Ingredient = "Chicken",
                    Allergen = "Coconut",
                    MeasurementAmount = 1.0,
                    Serving = 3,
                    MeasurementType = "pounds",
                    Instructions = "cook chicken with curry powder and chickpeas, let simmer on stovetop, adding coconut milk.",
                    Keywords = "south asian, dinner, spicy",
                    Categories = new List<int> { 3 },
                    TotalTime = 30
                }
            };



            collection.InsertMany(recipes);
            Console.WriteLine("Seeded recipes to database.");
        }
    }
}
