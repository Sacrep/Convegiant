﻿using Convegiant.Api.Spec;
using System.ComponentModel.DataAnnotations;

namespace ConvegiantFrontend.Data;

public class RecipeModel
{
	[Required]
	public string Title { get; set; }

	public string Description { get; set; }

	public int DefaultServings { get; set; }

	public int DurationInMinutes { get; set; }

	public List<InstructionModel> Instructions { get; set; } = [];

	public List<IngredientWithAmount> Ingredients { get; set; } = [];
}
