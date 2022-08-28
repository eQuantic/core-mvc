using eQuantic.Core.Mvc.Binders.Filtering;
using eQuantic.Core.Mvc.Binders.Sorting;
using Microsoft.Extensions.DependencyInjection;

namespace eQuantic.Core.Mvc.Extensions;

/// <summary>
/// The mvc builder extensions class
/// </summary>
public static class MvcBuilderExtensions
{
    /// <summary>
    /// Adds the filter model binder using the specified mvc builder
    /// </summary>
    /// <param name="mvcBuilder">The mvc builder</param>
    /// <returns>The mvc builder</returns>
    public static IMvcBuilder AddFilterModelBinder(this IMvcBuilder mvcBuilder)
    {
        mvcBuilder.AddMvcOptions(opt =>
        {
            opt.ModelBinderProviders.Insert(0, new FilteringModelBinderProvider());
        });
        return mvcBuilder;
    }

    /// <summary>
    /// Adds the sort model binder using the specified mvc builder
    /// </summary>
    /// <param name="mvcBuilder">The mvc builder</param>
    /// <returns>The mvc builder</returns>
    public static IMvcBuilder AddSortModelBinder(this IMvcBuilder mvcBuilder)
    {
        mvcBuilder.AddMvcOptions(opt =>
        {
            opt.ModelBinderProviders.Insert(0, new SortingModelBinderProvider());
        });
        return mvcBuilder;
    }
}