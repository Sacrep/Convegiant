using Convegiant.Domain;
using Convegiant.Domain.Entities;
using Convegiant.Infrastructure;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationInsightsTelemetry(builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]);
builder.Services.AddAuthorization();

builder.Services.AddSingleton<IRecipeRepository, Convegiant.Infrastructure.InMemory.RecipeRepository>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();

var recipeRepository = app.Services.GetRequiredService<IRecipeRepository>();

app.MapGet("/api/recipes", () => recipeRepository.GetAllRecipes());

app.MapGet("/api/recipes/{recipeId}", Results<Ok<Recipe>, NotFound> (Guid recipeId) =>
	recipeRepository.GetRecipeByID(recipeId)
	is Recipe recipe
		? TypedResults.Ok(recipe)
		: TypedResults.NotFound());

app.MapPost("/api/recipes", (Records.CreateRecipeDTO dto) =>
{
	var recipe = recipeRepository.Insert(dto);
	return TypedResults.Created($"/api/recipes/{recipe.Id}", recipe);
});

app.MapPatch("/api/recipes/{recipeId}", Results<Created<Recipe>, NotFound> (Guid recipeId, Records.CreateRecipeDTO dto) =>
{
	if (recipeRepository.GetRecipeByID(recipeId) == null)
	{
		return TypedResults.NotFound();
	}
	var updatedRecipe = recipeRepository.Update(recipeId, dto);
	return TypedResults.Created($"/api/recipes/{recipeId}", updatedRecipe);
});


app.Run();
