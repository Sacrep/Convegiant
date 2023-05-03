namespace Convegiant.Domain.Entities;
public class Recipe
{
    public Recipe(Records.CreateRecipeDTO dto)
        : this(Guid.NewGuid(), dto.Title, dto.Ingredients, dto.Instructions, dto.DefaultServings, dto.DurationInMinutes)
    {
    }

    public Recipe(Guid id, Records.CreateRecipeDTO dto)
        : this(id, dto.Title, dto.Ingredients, dto.Instructions, dto.DefaultServings, dto.DurationInMinutes)
    {
    }

    public Recipe(Guid id, string title, IEnumerable<Records.IngredientWithAmount> ingredients, string instructions, int defaultServings, int duration)
    {
        Id = id;
        Title = title;
        Ingredients = ingredients;
        Instructions = instructions;
        DefaultServings = defaultServings;
        DurationInMinutes = duration;
    }

    public Guid Id { get; set; }

    public string Title { get; set; }

    public string? TitlePicture { get; set; }

    public IEnumerable<Records.IngredientWithAmount> Ingredients { get; set; }

    public string Instructions { get; set; }

    public int DefaultServings { get; set; }

    public int DurationInMinutes { get; set; }
}
