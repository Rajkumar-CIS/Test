using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Assesment.Models;
using Microsoft.EntityFrameworkCore;

public class UniqueTitleAttribute : ValidationAttribute
{
    private readonly string _errorMessage;
    private readonly Type _context;

    public UniqueTitleAttribute(Type dbContext, string errorMessage = "The title must be unique.")
    {
        _context = dbContext;
        _errorMessage = errorMessage;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return ValidationResult.Success;
        }

        var title = value.ToString();
        var dbContext = (DbContext)validationContext.GetService(_context);

        var itemBeingEdited = validationContext.ObjectInstance;
        var itemIdProperty = itemBeingEdited.GetType().GetProperty("ID");

        if (itemIdProperty == null)
        {
            throw new InvalidOperationException("Id property not found on the object being validated.");
        }

        var itemId = itemIdProperty.GetValue(itemBeingEdited);

        var isTitleUnique = !dbContext.Set<Tasks>().Any(t => t.Title == title && t.ID != (int)itemId);

        if (!isTitleUnique)
        {
            return new ValidationResult(_errorMessage);
        }

        return ValidationResult.Success;
    }
}
