using eQuantic.Linq.Sorter;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace eQuantic.Core.Mvc.Binders.Sorting;

/// <summary>

/// The sort model binder class

/// </summary>

/// <seealso cref="IModelBinder"/>

public class SortingModelBinder : IModelBinder
{
    /// <summary>
    /// Binds the model using the specified binding context
    /// </summary>
    /// <param name="bindingContext">The binding context</param>
    /// <exception cref="ArgumentNullException"></exception>
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
        {
            throw new ArgumentNullException(nameof(bindingContext));
        }

        var modelName = bindingContext.ModelName;
        var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

        if (valueProviderResult == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }

        bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

        var values = valueProviderResult.Values;
        var model = new List<ISorting>();

        foreach (var value in values)
        {
            if (string.IsNullOrEmpty(value))
            {
                continue;
            }

            model.AddRange(SortingParser.Parse(value));
        }

        if (bindingContext.ModelType.IsArray)
        {
            bindingContext.Result = ModelBindingResult.Success(model.ToArray());
        }
        else if (SortingParser.IsValidListType(bindingContext.ModelType))
        {
            bindingContext.Result = ModelBindingResult.Success(model);
        }
        else
        {
            bindingContext.Result = ModelBindingResult.Success(model.FirstOrDefault());
        }

        return Task.CompletedTask;
    }
}