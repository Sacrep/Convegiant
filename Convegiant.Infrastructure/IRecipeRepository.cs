using Convegiant.Domain;
using Convegiant.Domain.Entities;

namespace Convegiant.Infrastructure;

public interface IRecipeRepository
{
	IEnumerable<RecipeListItem> GetRecipeList(int skip = 0, int take = 15);
	Recipe? GetRecipeByID(string recipeId);
	Recipe Insert(Records.CreateRecipeDTO dto);
	void Delete(string recipeId);
	Recipe Update(string recipeId, Records.CreateRecipeDTO dto);
}