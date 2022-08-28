using eQuantic.Linq.Sorter;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace eQuantic.Core.Mvc.Binders.Sorting;

/// <summary>
/// The sort model binder provider class
/// </summary>
/// <seealso cref="IModelBinderProvider"/>
public class SortingModelBinderProvider : IModelBinderProvider
{
    /// <summary>
    /// Gets the binder using the specified context
    /// </summary>
    /// <param name="context">The context</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns>The model binder</returns>
    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        var modelType = context.Metadata.ModelType;
        modelType = modelType.IsArray ? modelType.GetElementType() : modelType;

        if (SortingParser.IsValidListType(modelType))
        {
            modelType = modelType.GetGenericArguments().FirstOrDefault();
        }

        if (typeof(ISorting).IsAssignableFrom(modelType))
        {
            return new BinderTypeModelBinder(typeof(SortingModelBinder));
        }
        return null;
    }
}