
using Convegiant.Domain;
using Convegiant.Domain.Entities;

namespace Convegiant.Infrastructure.InMemory;

public class RecipeRepository : IRecipeRepository
{
	private readonly List<Recipe> _recipes = new();

	public void Delete(Guid recipeId)
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

	public Recipe? GetRecipeByID(Guid recipeId)
	{
		return _recipes.FirstOrDefault(r => r.Id == recipeId);
	}

	public IEnumerable<Recipe> GetAllRecipes()
	{
		return _recipes;
	}

	public Recipe Insert(Records.CreateRecipeDTO dto)
	{
		var recipe = new Recipe(dto);

		_recipes.Add(recipe);
		return recipe;
	}

	public void Save()
	{
		throw new NotImplementedException();
	}

	public Recipe Update(Guid recipeId, Records.CreateRecipeDTO dto)
	{
		var savedRecipe = _recipes.FirstOrDefault(r => r.Id == recipeId);
		var updatedRecipe = new Recipe(recipeId, dto);
		savedRecipe = updatedRecipe;

		return updatedRecipe;
	}
}