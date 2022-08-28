using eQuantic.Linq.Filter;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace eQuantic.Core.Mvc.Binders.Filtering;

/// <summary>
/// The filter model binder provider class
/// </summary>
/// <seealso cref="IModelBinderProvider"/>
public class FilteringModelBinderProvider : IModelBinderProvider
{
    /// <summary>
    /// Gets the binder using the specified context
    /// </summary>
    /// <param name="context">The context</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns>The model binder</returns>
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        var modelType = context.Metadata.ModelType;
        modelType = modelType.IsArray ? modelType.GetElementType()! : modelType;

        if (FilteringParser.IsValidListType(modelType))
        {
            modelType = modelType.GetGenericArguments().FirstOrDefault();
        }

        return typeof(IFiltering).IsAssignableFrom(modelType) ? 
            new BinderTypeModelBinder(typeof(FilteringModelBinder)) : 
            null;
    }
}