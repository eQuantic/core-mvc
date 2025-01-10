using System.Globalization;
using eQuantic.Core.Mvc.Binders.Filtering;
using eQuantic.Linq.Filter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Primitives;

namespace eQuantic.Core.Mvc.Tests.Binders.Filtering;

[TestFixture]
public class FilteringModelBinderTests
{
    [Test]
    public async Task FilteringModelBinder_BindModelAsync_successfully()
    {
        var binder = new FilteringModelBinder();
        var valueProvider = new QueryStringValueProvider(
            new BindingSource(Guid.NewGuid().ToString(), "filterBy", false, true),
            new QueryCollection(new Dictionary<string, StringValues>
            {
                { "filterBy", new StringValues("and(startedAt:gte(0001-01-01),startedAt:lte(2023-06-30))") }
            }),
            CultureInfo.CurrentCulture
        );
        
        var metadataProvider = new EmptyModelMetadataProvider();
        var context = new DefaultModelBindingContext
        {
            ModelName = "filterBy",
            ModelState = new ModelStateDictionary(),
            ModelMetadata = metadataProvider.GetMetadataForType(typeof(IFiltering<>)),
            ValueProvider = valueProvider
        };
        await binder.BindModelAsync(context);
        Assert.Multiple(() =>
        {
            Assert.That(context.Result.IsModelSet, Is.True);
            Assert.That(context.Result.Model, Is.Not.Null);
        });
    }
}