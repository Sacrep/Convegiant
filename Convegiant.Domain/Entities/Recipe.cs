using System.Text.Json.Serialization;

namespace Convegiant.Domain.Entities;
public class Recipe
{
	[JsonConstructor]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	private Recipe()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	{
	}

	public Recipe(Records.CreateRecipeDTO dto)
	{
		Title = dto.Title;
		Ingredients = dto.Ingredients;
		Instructions = dto.Instructions;
		DefaultServings = dto.DefaultServings;
		DurationInMinutes = dto.DurationInMinutes;
	}

	public string? Id { get; set; }

	public string Title { get; set; }

	public string? TitlePicture { get; set; }

	public IEnumerable<Records.IngredientWithAmount> Ingredients { get; set; }

	public IEnumerable<string> Instructions { get; set; }

	public int DefaultServings { get; set; }

	public int DurationInMinutes { get; set; }

	public RecipeListItem ToListItem()
	{
		return new RecipeListItem
		{
			Id = Id,
			Title = Title,
			TitlePicture = TitlePicture,
			DurationInMinutes = DurationInMinutes,
			IngredientCount = Ingredients.Count(),
			IsVegetarian = !Ingredients.Any(ic => ic.Ingredient.Type == IngredientCategory.Meat || ic.Ingredient.Type == IngredientCategory.Fish),
			IsVegan = !Ingredients.Any(ic => ic.Ingredient.Type == IngredientCategory.Meat || ic.Ingredient.Type == IngredientCategory.Fish || ic.Ingredient.Type == IngredientCategory.Dairy || ic.Ingredient.Type == IngredientCategory.Eggs)
		};
	}
}
