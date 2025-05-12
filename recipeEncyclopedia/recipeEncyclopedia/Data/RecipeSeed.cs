using MongoDB.Driver;
using recipeEncyclopedia.Models;
using recipeEncyclopedia.Models.recipeEncyclopedia.Models;
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

            collection.DeleteMany(_ => true);

            var recipes = new List<Recipe>
            {
                new Recipe
                {
                    Name = "Spaghetti Bolognese",
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient { Name = "spaghetti", Amount = 2, MeasurementType = "cups", Allergen = "none" },
                        new Ingredient { Name = "ground beef", Amount = 0.5, MeasurementType = "pounds", Allergen = "none" },
                        new Ingredient { Name = "tomato sauce", Amount = 1, MeasurementType = "cups", Allergen = "none" },
                        new Ingredient { Name = "onion", Amount = 1, MeasurementType = "whole", Allergen = "none" },
                        new Ingredient { Name = "garlic", Amount = 2, MeasurementType = "cloves", Allergen = "none" }
                    },
                    Serving = 4,
                    Instructions = "Boil spaghetti. Cook beef. Mix with sauce. Combine and serve.",
                    Keywords = "italian, pasta",
                    Categories = new List<int> { 3 },
                    TotalTime = 45
                },

                new Recipe
                {
                    Name = "Pancakes",
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient { Name = "flour", Amount = 1, MeasurementType = "cups", Allergen = "none" },
                        new Ingredient { Name = "milk", Amount = 1, MeasurementType = "cups", Allergen = "dairy" },
                        new Ingredient { Name = "eggs", Amount = 2, MeasurementType = "unit", Allergen = "egg" },
                        new Ingredient { Name = "sugar", Amount = 0.25, MeasurementType = "cups", Allergen = "none" },
                        new Ingredient { Name = "baking powder", Amount = 1, MeasurementType = "tsp", Allergen = "none" }
                    },
                    Serving = 2,
                    Instructions = "Mix all ingredients. Pour batter on griddle. Flip when bubbles form.",
                    Keywords = "pancake, breakfast, sweet",
                    Categories = new List<int> { 1 },
                    TotalTime = 20
                },

                new Recipe
                {
                    Name = "Chicken Salad",
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient { Name = "chicken breast", Amount = 200, MeasurementType = "grams", Allergen = "none" },
                        new Ingredient { Name = "lettuce", Amount = 2, MeasurementType = "cups", Allergen = "none" },
                        new Ingredient { Name = "tomato", Amount = 1, MeasurementType = "whole", Allergen = "none" },
                        new Ingredient { Name = "cucumber", Amount = 1, MeasurementType = "whole", Allergen = "none" },
                        new Ingredient { Name = "olive oil", Amount = 2, MeasurementType = "tbsp", Allergen = "none" }
                    },
                    Serving = 4,
                    Instructions = "Grill chicken. Chop veggies. Toss everything with olive oil.",
                    Keywords = "chicken, salad, healthy",
                    Categories = new List<int> { 2 },
                    TotalTime = 25
                },

                new Recipe
                {
                    Name = "Chocolate Chip Cookies",
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient { Name = "flour", Amount = 3, MeasurementType = "cups", Allergen = "none" },
                        new Ingredient { Name = "chocolate chips", Amount = 1, MeasurementType = "cups", Allergen = "dairy" },
                        new Ingredient { Name = "sugar", Amount = 1, MeasurementType = "cups", Allergen = "none" },
                        new Ingredient { Name = "eggs", Amount = 2, MeasurementType = "unit", Allergen = "egg" }
                    },
                    Serving = 6,
                    Instructions = "Crack eggs into flour and whisk, add sugar, stir in chocolate chips. Bake at 425°F for 15 minutes.",
                    Keywords = "dessert, comfort",
                    Categories = new List<int> { 4 },
                    TotalTime = 25
                },

                new Recipe
                {
                    Name = "Cupcakes",
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient { Name = "flour", Amount = 2, MeasurementType = "cups", Allergen = "none" },
                        new Ingredient { Name = "chocolate", Amount = 1, MeasurementType = "cups", Allergen = "dairy" },
                        new Ingredient { Name = "sugar", Amount = 1, MeasurementType = "cups", Allergen = "none" },
                        new Ingredient { Name = "eggs", Amount = 2, MeasurementType = "unit", Allergen = "egg" },
                        new Ingredient { Name = "vanilla", Amount = 1, MeasurementType = "tsp", Allergen = "none" },
                        new Ingredient { Name = "frosting", Amount = 0.5, MeasurementType = "cups", Allergen = "dairy" }
                    },
                    Serving = 8,
                    Instructions = "Mix ingredients. Bake at 425°F for 25 minutes. Add frosting after baking.",
                    Keywords = "dessert, comfort",
                    Categories = new List<int> { 5 },
                    TotalTime = 60
                },

                new Recipe
                {
                    Name = "Banana Bread",
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient { Name = "bananas", Amount = 0.5, MeasurementType = "pounds", Allergen = "none" },
                        new Ingredient { Name = "flour", Amount = 1.5, MeasurementType = "cups", Allergen = "none" },
                        new Ingredient { Name = "sugar", Amount = 1, MeasurementType = "cups", Allergen = "none" },
                        new Ingredient { Name = "chocolate chips", Amount = 0.5, MeasurementType = "cups", Allergen = "dairy" },
                        new Ingredient { Name = "eggs", Amount = 2, MeasurementType = "unit", Allergen = "egg" }
                    },
                    Serving = 5,
                    Instructions = "Mix ingredients and bake at 425°F for 25 minutes.",
                    Keywords = "dessert, fruit",
                    Categories = new List<int> { 5 },
                    TotalTime = 35
                },

                new Recipe
                {
                    Name = "Pancakes",
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient { Name = "flour", Amount = 1, MeasurementType = "cups", Allergen = "none" },
                        new Ingredient { Name = "milk", Amount = 1, MeasurementType = "cups", Allergen = "dairy" },
                        new Ingredient { Name = "eggs", Amount = 2, MeasurementType = "unit", Allergen = "egg" },
                        new Ingredient { Name = "sugar", Amount = 0.25, MeasurementType = "cups", Allergen = "none" },
                        new Ingredient { Name = "baking powder", Amount = 1, MeasurementType = "tsp", Allergen = "none" }
                    },
                    Serving = 2,
                    Instructions = "Mix all ingredients. Pour batter on griddle. Flip when bubbles form.",
                    Keywords = "pancake, breakfast, sweet",
                    Categories = new List<int> { 1 },
                    TotalTime = 20
                },

                new Recipe
                {
                    Name = "French Toast",
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient { Name = "bread", Amount = 3, MeasurementType = "slices", Allergen = "gluten" },
                        new Ingredient { Name = "eggs", Amount = 2, MeasurementType = "unit", Allergen = "egg" },
                        new Ingredient { Name = "milk", Amount = 0.5, MeasurementType = "cups", Allergen = "dairy" },
                        new Ingredient { Name = "cinnamon", Amount = 1, MeasurementType = "tsp", Allergen = "none" },
                        new Ingredient { Name = "vanilla", Amount = 1, MeasurementType = "tsp", Allergen = "none" }
                    },
                    Serving = 3,
                    Instructions = "Cut thick pieces of bread. Whisk eggs, cinnamon, and vanilla. Pour on bread and bake at 425°F for 25 minutes.",
                    Keywords = "breakfast, sweet",
                    Categories = new List<int> { 3 },
                    TotalTime = 30
                },

                new Recipe
                {
                    Name = "Omelette",
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient { Name = "eggs", Amount = 2, MeasurementType = "unit", Allergen = "egg" },
                        new Ingredient { Name = "tomato", Amount = 0.5, MeasurementType = "whole", Allergen = "none" },
                        new Ingredient { Name = "onion", Amount = 0.5, MeasurementType = "whole", Allergen = "none" },
                        new Ingredient { Name = "jalapeno", Amount = 1, MeasurementType = "unit", Allergen = "none" },
                        new Ingredient { Name = "cheese", Amount = 0.25, MeasurementType = "cups", Allergen = "dairy" },
                        new Ingredient { Name = "ham", Amount = 2, MeasurementType = "slices", Allergen = "none" }
                    },
                    Serving = 1,
                    Instructions = "Crack eggs on pan. Add chopped veggies, cheese, and ham into eggs. Cook and fold.",
                    Keywords = "healthy, breakfast, quick",
                    Categories = new List<int> { 3 },
                    TotalTime = 10
                },

                new Recipe
                {
                    Name = "Cheeseburger",
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient { Name = "cheese", Amount = 2, MeasurementType = "slices", Allergen = "dairy" },
                        new Ingredient { Name = "ground beef", Amount = 1, MeasurementType = "pounds", Allergen = "none" },
                        new Ingredient { Name = "tomato", Amount = 1, MeasurementType = "whole", Allergen = "none" },
                        new Ingredient { Name = "onion", Amount = 0.5, MeasurementType = "whole", Allergen = "none" },
                        new Ingredient { Name = "pickle", Amount = 4, MeasurementType = "slices", Allergen = "none" },
                        new Ingredient { Name = "lettuce", Amount = 1, MeasurementType = "leaf", Allergen = "none" }
                    },
                    Serving = 2,
                    Instructions = "Form patties, cook on grill. Add toppings and serve on buns.",
                    Keywords = "american, fast",
                    Categories = new List<int> { 3 },
                    TotalTime = 15
                },

                new Recipe
                {
                    Name = "Fried Rice",
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient { Name = "rice", Amount = 1.5, MeasurementType = "cups", Allergen = "none" },
                        new Ingredient { Name = "soy sauce", Amount = 2, MeasurementType = "tbsp", Allergen = "soy" },
                        new Ingredient { Name = "teriyaki", Amount = 2, MeasurementType = "tbsp", Allergen = "soy" },
                        new Ingredient { Name = "chicken", Amount = 200, MeasurementType = "grams", Allergen = "none" },
                        new Ingredient { Name = "peas", Amount = 0.5, MeasurementType = "cups", Allergen = "none" },
                        new Ingredient { Name = "carrots", Amount = 0.5, MeasurementType = "cups", Allergen = "none" }
                    },
                    Serving = 3,
                    Instructions = "Cook veggies, chicken, sauces in a wok. Cook rice in pot. Combine.",
                    Keywords = "asian, dinner",
                    Categories = new List<int> { 1 },
                    TotalTime = 25
                },

                new Recipe
                {
                    Name = "Tacos",
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient { Name = "cheese", Amount = 0.5, MeasurementType = "cups", Allergen = "dairy" },
                        new Ingredient { Name = "chicken", Amount = 200, MeasurementType = "grams", Allergen = "none" },
                        new Ingredient { Name = "tomato", Amount = 1, MeasurementType = "whole", Allergen = "none" },
                        new Ingredient { Name = "onion", Amount = 0.5, MeasurementType = "whole", Allergen = "none" },
                        new Ingredient { Name = "avocado", Amount = 1, MeasurementType = "whole", Allergen = "none" },
                        new Ingredient { Name = "lettuce", Amount = 1, MeasurementType = "cup", Allergen = "none" },
                        new Ingredient { Name = "jalapeno", Amount = 1, MeasurementType = "unit", Allergen = "none" },
                        new Ingredient { Name = "cilantro", Amount = 1, MeasurementType = "tbsp", Allergen = "none" },
                        new Ingredient { Name = "soft shells", Amount = 2, MeasurementType = "unit", Allergen = "gluten" }
                    },
                    Serving = 2,
                    Instructions = "Slow-cook chicken, prep toppings, serve in soft shells.",
                    Keywords = "hispanic, long, dinner",
                    Categories = new List<int> { 3 },
                    TotalTime = 180
                },

                new Recipe
                {
                    Name = "Falafel Gyro Sandwich",
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient { Name = "gyro", Amount = 0.5, MeasurementType = "pounds", Allergen = "none" },
                        new Ingredient { Name = "tzatziki", Amount = 0.25, MeasurementType = "cups", Allergen = "dairy" },
                        new Ingredient { Name = "onion", Amount = 0.5, MeasurementType = "whole", Allergen = "none" },
                        new Ingredient { Name = "tortilla", Amount = 1, MeasurementType = "unit", Allergen = "gluten" },
                        new Ingredient { Name = "falafel", Amount = 2, MeasurementType = "balls", Allergen = "none" }
                    },
                    Serving = 1,
                    Instructions = "Cook gyro and falafel. Assemble wrap with tzatziki and onion.",
                    Keywords = "mediterranean, dinner, quick",
                    Categories = new List<int> { 3 },
                    TotalTime = 10
                },

                new Recipe
                {
                    Name = "Chicken Curry",
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient { Name = "chicken", Amount = 1, MeasurementType = "pounds", Allergen = "none" },
                        new Ingredient { Name = "coconut milk", Amount = 1, MeasurementType = "cups", Allergen = "coconut" },
                        new Ingredient { Name = "curry powder", Amount = 2, MeasurementType = "tbsp", Allergen = "none" },
                        new Ingredient { Name = "chickpeas", Amount = 0.5, MeasurementType = "cups", Allergen = "none" }
                    },
                    Serving = 3,
                    Instructions = "Simmer chicken and curry powder with chickpeas. Add coconut milk to finish.",
                    Keywords = "south asian, dinner, spicy",
                    Categories = new List<int> { 3 },
                    TotalTime = 30
                },

                new Recipe
                {
                    Name = "Mac and Cheese",
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient { Name = "cheese", Amount = 2, MeasurementType = "cups", Allergen = "dairy" },
                        new Ingredient { Name = "elbow macaroni", Amount = 2, MeasurementType = "cups", Allergen = "gluten" },
                        new Ingredient { Name = "milk", Amount = 1, MeasurementType = "cups", Allergen = "dairy" },
                        new Ingredient { Name = "flour", Amount = 2, MeasurementType = "tbsp", Allergen = "gluten" },
                        new Ingredient { Name = "pasta", Amount = 0.5, MeasurementType = "pounds", Allergen = "gluten" }
                    },
                    Serving = 4,
                    Instructions = "Cook pasta. Make roux, stir in milk and cheese. Combine and serve.",
                    Keywords = "italian, comfort, dinner",
                    Categories = new List<int> { 3 },
                    TotalTime = 20
                },

                new Recipe
                {
                    Name = "Miso Soup",
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient { Name = "miso", Amount = 2, MeasurementType = "tbsp", Allergen = "soy" },
                        new Ingredient { Name = "tofu", Amount = 0.5, MeasurementType = "cups", Allergen = "soy" },
                        new Ingredient { Name = "green onion", Amount = 2, MeasurementType = "stalks", Allergen = "none" },
                        new Ingredient { Name = "seaweed", Amount = 0.25, MeasurementType = "cups", Allergen = "none" },
                        new Ingredient { Name = "kelp", Amount = 0.25, MeasurementType = "cups", Allergen = "none" },
                        new Ingredient { Name = "shiitake mushrooms", Amount = 0.5, MeasurementType = "cups", Allergen = "none" }
                    },
                    Serving = 2,
                    Instructions = "Make dashi broth, add tofu and mushrooms. Mix miso paste in. Garnish with onion.",
                    Keywords = "vegan, asian, dinner",
                    Categories = new List<int> { 1 },
                    TotalTime = 75
                },

                new Recipe
                {
                    Name = "Chickpea Salad",
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient { Name = "chickpeas", Amount = 200, MeasurementType = "grams", Allergen = "none" },
                        new Ingredient { Name = "tahini", Amount = 2, MeasurementType = "tbsp", Allergen = "sesame" },
                        new Ingredient { Name = "mustard", Amount = 1, MeasurementType = "tbsp", Allergen = "none" },
                        new Ingredient { Name = "celery", Amount = 0.5, MeasurementType = "cups", Allergen = "none" },
                        new Ingredient { Name = "red onion", Amount = 0.25, MeasurementType = "cups", Allergen = "none" },
                        new Ingredient { Name = "lemon juice", Amount = 2, MeasurementType = "tbsp", Allergen = "none" }
                    },
                    Serving = 1,
                    Instructions = "Mash chickpeas and mix in all chopped ingredients and sauces.",
                    Keywords = "vegan, lunch, healthy",
                    Categories = new List<int> { 3 },
                    TotalTime = 20
                },

                new Recipe
                {
                    Name = "Supreme Pizza",
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient { Name = "cheese", Amount = 1.5, MeasurementType = "cups", Allergen = "dairy" },
                        new Ingredient { Name = "pepperoni", Amount = 0.5, MeasurementType = "cups", Allergen = "none" },
                        new Ingredient { Name = "tomato", Amount = 1, MeasurementType = "whole", Allergen = "none" },
                        new Ingredient { Name = "dough", Amount = 0.5, MeasurementType = "pounds", Allergen = "gluten" },
                        new Ingredient { Name = "onion", Amount = 0.5, MeasurementType = "whole", Allergen = "none" },
                        new Ingredient { Name = "pepper", Amount = 0.5, MeasurementType = "whole", Allergen = "none" },
                        new Ingredient { Name = "sausage", Amount = 0.5, MeasurementType = "cups", Allergen = "none" }
                    },
                    Serving = 4,
                    Instructions = "Spread sauce on dough, layer with cheese, toppings, and bake.",
                    Keywords = "italian, comfort, dinner",
                    Categories = new List<int> { 3 },
                    TotalTime = 30
                }

            };

            collection.InsertMany(recipes);
            Console.WriteLine("Seeded recipes to database.");
        }
    }
}
