using Convegiant.Domain.Entities;
using Raven.Client.Documents.Indexes;

namespace Convegiant.Infrastructure.RavenDB;

public class Recipe_ListView : AbstractIndexCreationTask<Recipe>
{
	public Recipe_ListView()
	{
		Map = recipes => from recipe in recipes
						 select new RecipeListItem
						 {
							 Id = recipe.Id,
							 Title = recipe.Title,
							 TitlePicture = recipe.TitlePicture,
							 DurationInMinutes = recipe.DurationInMinutes,
							 IngredientCount = recipe.Ingredients.Count(),
							 IsVegetarian = !recipe.Ingredients.Any(ic => ic.Ingredient.Type == IngredientCategory.Meat || ic.Ingredient.Type == IngredientCategory.Fish),
							 IsVegan = !recipe.Ingredients.Any(ic => ic.Ingredient.Type == IngredientCategory.Meat || ic.Ingredient.Type == IngredientCategory.Fish || ic.Ingredient.Type == IngredientCategory.Dairy || ic.Ingredient.Type == IngredientCategory.Eggs)
						 };
	}
}