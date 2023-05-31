
using Convegiant.Domain;
using Convegiant.Domain.Entities;

namespace Convegiant.Infrastructure.InMemory;

public class InMemoryRecipeRepository : IRecipeRepository
{
	private readonly List<Recipe> _recipes = new();

	public void Delete(string recipeId)
	{
		var recipe = _recipes.FirstOrDefault(x => x.Id == recipeId);
		if (recipe != null)
		{
			_recipes.Remove(recipe);
		}
		else
		{
			Console.WriteLine($"No recipe to delete with ID {recipeId}");
		}

	}

	public Recipe? GetRecipeByID(string recipeId)
	{
		return _recipes.FirstOrDefault(r => r.Id == recipeId);
	}

	public IEnumerable<RecipeListItem> GetRecipeList(int skip = 0, int take = 15)
	{
		return _recipes.Skip(skip).Take(take).Select(recipe => recipe.ToListItem());
	}

	public Recipe Insert(Records.CreateRecipeDTO dto)
	{
		var recipe = new Recipe(dto);

		_recipes.Add(recipe);
		return recipe;
	}

	public Recipe Update(string recipeId, Records.CreateRecipeDTO dto)
	{
		var savedRecipe = _recipes.FirstOrDefault(r => r.Id == recipeId);
		var updatedRecipe = new Recipe(dto)
		{
			Id = recipeId
		};
		savedRecipe = updatedRecipe;

		return updatedRecipe;
	}
}