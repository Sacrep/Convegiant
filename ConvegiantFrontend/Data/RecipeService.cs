using Convegiant.Api.Spec;

namespace ConvegiantFrontend.Data;
public class RecipeService
{
	private readonly ConvegiantApiClient _convegiantApiClient;
	private readonly HttpClient _httpClient;

	public RecipeService(string apiBaseAddress)
	{
		_httpClient = new HttpClient();
		_convegiantApiClient = new ConvegiantApiClient(apiBaseAddress, _httpClient);
	}

	public async Task<ICollection<RecipeListItem>> GetRecipes(int page = 1)
	{
		if (page < 1) throw new ArgumentException("Page should be a positive number", nameof(page));

		var pageSize = 15;
		var skip = (page - 1) * pageSize;
		return await _convegiantApiClient.RecipesAllAsync(skip, pageSize).ConfigureAwait(false);
	}

	public Task<Recipe> GetRecipeDetails(string recipeId)
	{
		return _convegiantApiClient.RecipesGETAsync(recipeId);
	}

	public async Task<Recipe> PostRecipeDetails(CreateRecipeDTO dto)
	{
		var createdRecipe = await _convegiantApiClient.RecipesPOSTAsync(dto);
		return createdRecipe;
	}
}
