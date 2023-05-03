using Convegiant.Domain.Entities;
using Convegiant.Domain;

namespace Convegiant.Infrastructure;

public interface IRecipeRepository
{
    IEnumerable<Recipe> GetAllRecipes();
    Recipe? GetRecipeByID(Guid recipeId);
    Recipe Insert(Records.CreateRecipeDTO recipe);
    void Delete(Guid recipeId);
    Recipe Update(Guid recipeId, Records.CreateRecipeDTO recipe);
    void Save();
}