using Convegiant.Domain;
using Convegiant.Domain.Entities;
using Convegiant.Infrastructure;
using Convegiant.Infrastructure.Extensions;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var options = new ApplicationInsightsServiceOptions { ConnectionString = builder.Configuration["ApplicationInsights:InstrumentationKey"] };
builder.Services.AddApplicationInsightsTelemetry(options);

builder.Services.AddAuthorization();

builder.Services.AddRavenDbDatabase(
	builder.Configuration.GetSection("RavenDB:Urls").Get<string[]>() ?? Array.Empty<string>(),
	builder.Configuration["RavenDB:CertificatePath"] ?? "");

builder.Services.AddTransient<IRecipeRepository, Convegiant.Infrastructure.RavenDB.RavenDbRecipeRepository>();

//builder.Services.AddSingleton<IRecipeRepository, Convegiant.Infrastructure.InMemory.InMemoryRecipeRepository>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();

var recipeRepository = app.Services.GetRequiredService<IRecipeRepository>();

app.MapGet("/api/recipes", (int page) =>
{
	var pageSize = 15;
	var skip = (page - 1) * pageSize;
	return recipeRepository.GetRecipeList(skip, pageSize);
});

app.MapGet("/api/recipes/{recipeId}", Results<Ok<Recipe>, NotFound> (string recipeId) =>
	recipeRepository.GetRecipeByID(recipeId)
	is Recipe recipe
		? TypedResults.Ok(recipe)
		: TypedResults.NotFound());

app.MapPost("/api/recipes", (Records.CreateRecipeDTO dto) =>
{
	var recipe = recipeRepository.Insert(dto);
	return TypedResults.Created($"/api/recipes/{recipe.Id}", recipe);
});

app.MapPatch("/api/recipes/{recipeId}", Results<Created<Recipe>, NotFound> (string recipeId, Records.CreateRecipeDTO dto) =>
{
	if (recipeRepository.GetRecipeByID(recipeId) == null)
	{
		return TypedResults.NotFound();
	}
	var updatedRecipe = recipeRepository.Update(recipeId, dto);
	return TypedResults.Created($"/api/recipes/{recipeId}", updatedRecipe);
});


app.Run();
