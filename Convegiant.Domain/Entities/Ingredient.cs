namespace Convegiant.Domain.Entities;

public class Ingredient
{
	public required string Name { get; init; }

	public IngredientCategory Type { get; set; }

	public bool IsStaple { get; set; }

	public string? Icon { get; set; }
}

public enum IngredientCategory
{
	Vegetable,
	Fruit,
	Herb,
	Dairy,
	Eggs,
	Meat,
	Fish,
	Carbs,
	Conserve,
	Seasoning,
	Misc
}
