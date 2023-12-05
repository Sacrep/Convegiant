using Convegiant.Domain.Entities;

namespace Convegiant.Domain;
public static class Records
{
	public record CreateRecipeDTO(string Title, IngredientWithAmount[] Ingredients, Instruction[] Instructions, int DefaultServings, int DurationInMinutes);

	public record IngredientWithAmount(Ingredient Ingredient, decimal Quantity, string? Unit);

	public record Instruction(int stepNr, string instruction);
}
