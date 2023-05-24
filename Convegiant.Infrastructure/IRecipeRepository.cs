using Convegiant.Domain;
using Convegiant.Domain.Entities;

namespace Convegiant.Infrastructure;

public interface IRecipeRepository
{
	IEnumerable<Recipe> GetAllRecipes();
	Recipe? GetRecipeByID(string recipeId);
	Recipe Insert(Records.CreateRecipeDTO dto);
	void Delete(string recipeId);
	Recipe Update(string recipeId, Records.CreateRecipeDTO dto);
}