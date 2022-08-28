using eQuantic.Linq.Filter;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace eQuantic.Core.Mvc.Binders.Filtering;

/// <summary>
/// The filter model binder class
/// </summary>
/// <seealso cref="IModelBinder"/>
public class FilteringModelBinder : IModelBinder
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
        var model = new List<IFiltering>();

        foreach (var value in values)
        {
            if (string.IsNullOrEmpty(value))
            {
                continue;
            }

            model.AddRange(FilteringParser.Parse(value));
        }

        if (bindingContext.ModelType.IsArray)
        {
            bindingContext.Result = ModelBindingResult.Success(model.ToArray());
        }
        else if (FilteringParser.IsValidListType(bindingContext.ModelType))
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