using Convegiant.Domain;
using Convegiant.Domain.Entities;
using Raven.Client.Documents;

namespace Convegiant.Infrastructure.RavenDB;

public class RavenDbRecipeRepository : IRecipeRepository
{
	private readonly IDocumentStore _store;

	public RavenDbRecipeRepository(IDocumentStore store)
	{
		_store = store;
	}

	public void Delete(string recipeId)
	{
		using var session = _store.OpenSession();
		session.Delete(recipeId.ToString());
		session.SaveChanges();
	}

	public IEnumerable<RecipeListItem> GetRecipeList(int skip = 0, int take = 15)
	{
		using var session = _store.OpenSession();

		var recipes = session.Query<RecipeListItem, Recipe_ListView>()
			.Skip(skip)
			.Take(take)
			.ToList();

		return recipes;
	}

	public Recipe? GetRecipeByID(string recipeId)
	{
		using var session = _store.OpenSession();

		var recipe = session.Load<Recipe>(recipeId.ToString());

		return recipe;
	}

	public Recipe Insert(Records.CreateRecipeDTO dto)
	{
		var recipe = new Recipe(dto);

		using var session = _store.OpenSession();
		session.Store(recipe);
		session.SaveChanges();

		return recipe;
	}

	public Recipe Update(string recipeId, Records.CreateRecipeDTO dto)
	{
		using var session = _store.OpenSession();

		session.Advanced.Patch<Recipe, string>(recipeId, x => x.Title, dto.Title);
		session.Advanced.Patch<Recipe, IEnumerable<Records.Instruction>>(recipeId, x => x.Instructions, dto.Instructions);
		session.Advanced.Patch<Recipe, IEnumerable<Records.IngredientWithAmount>>(recipeId, x => x.Ingredients, dto.Ingredients);
		session.Advanced.Patch<Recipe, int>(recipeId, x => x.DefaultServings, dto.DefaultServings);
		session.Advanced.Patch<Recipe, int>(recipeId, x => x.DurationInMinutes, dto.DurationInMinutes);

		session.SaveChanges();

		return new Recipe(dto) { Id = recipeId };
	}
}
