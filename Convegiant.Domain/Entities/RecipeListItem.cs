namespace Convegiant.Domain.Entities;

public class RecipeListItem
{
	public string? Id { get; init; }
	public string? Title { get; init; }
	public string? TitlePicture { get; init; }
	public int DurationInMinutes { get; init; }
	public int IngredientCount { get; init; }
	public bool IsVegetarian { get; init; }
	public bool IsVegan { get; init; }
}
